select * from FoodOrderList
select * from FoodOrder
sp_depends LiquorOrder

select * from LiquorOrder

--$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
Alter Table LiquorOrder Drop Column Waiter_Name
Alter Table FoodOrder Add WaiterName VarChar(100)
Update FoodOrder Set WaiterName = ''

Alter Table LiquorOrder Add WaiterName VarChar(100)
Update LiquorOrder Set WaiterName = ''

--$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
Alter proc [dbo].[sp_InsertAllFoodOrder]    
(    
@UserName varchar(32),    
@MembershipNo varchar(32),    
@TotalAmount float,    
@TotalGST float,    
@TableNo int,    
@Status varchar(10),    
@OrderId int output,    
@GrossAmount float,
@WaiterName VarChar(100)
)    
As    
Begin    
    
Begin Transaction    
declare @Date1 Date    
set @Date1=(select Getdate())    
insert into [dbo].[FoodOrder] (GrossAmount,Date,UserName,MembershipNo,TotalAmount,TotalGST,TableNo,Status,WaiterName) values(@GrossAmount,@Date1,@UserName,@MembershipNo,@TotalAmount,@TotalGST,@TableNo,@Status,@WaiterName)    
Select @@Identity    
set @OrderId = @@Identity    
declare @FoodOrderId int = @@Identity    
declare @ServiceType int = '1'    
insert into dbo.TempPayment (OrderAmount,MembershipNo,OrderId,TotalOrderAmount,Tax,ServiceType,BillDate,Status) values (@GrossAmount,@MembershipNo,@FoodOrderId,@TotalAmount,@TotalGST,@ServiceType,@Date1,@Status)    
commit transaction    
end  

--$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

Alter proc [dbo].[spViewDetailedBill]        
  (@MembershipNo varchar(32),        
  @BillDate Date,@Flag int)        
  as begin        
         
  if(@Flag=0)        
  begin        
  --FoodOrder        
   select  FO.Date, FO.MembershipNo, FOL.OrderId, FOL.FoodName,FOL.Quantity,FOL.Price,FOL.GST,FO.UserName from FoodOrderList FOL join         
 FoodOrder FO on Fo.OrderId=FOL.OrderId where FO.MembershipNo=@MembershipNo and FO.Status='UnPaid' and FOL.Status='Completed'        
 and Date=@BillDate        
         
 ----LiquorOrder        
 -- select  LO.Date, LO.MembershipNo, LOL.OrderId, LOL.LiquorName,LOL.Quantity,LOL.Price,LOL.GST,LO.UserName from LiquorOrderList LOL join         
 --LiquorOrder LO on LO.OrderId=LOL.OrderId where LO.MembershipNo=@MembershipNo and LO.Status='UnPaid' and LOL.Status='Completed'        
 --and Date=@BillDate        
  
  
   select  LO.Date, LO.MembershipNo, LOL.OrderId, LOL.LiquorName,LOL.Quantity  
   --,LOL.Price  
   ,LOL.GST,LO.UserName ,  
   case when LOL.Type='Bottle' or LOL.Type='Miscellaneous' then LRC.SellingPricePerBottle  
  when LOL.Type='Peg' then LRC.SellingPricePerPeg end as Price, LO.WaiterName
   from LiquorOrderList LOL join         
 LiquorOrder LO on LO.OrderId=LOL.OrderId join LiquorRateCard LRC on LRC.LiquorId=LOL.LiquorId  
  where LO.MembershipNo=@MembershipNo and LO.Status='UnPaid' and LOL.Status='Completed'        
 and Date=@BillDate     
  
  
        
 --subscription Type        
 --select MembershipType,SubscriptionAmountPaid,PaymentStatus from Member where  MembershipNo=@MembershipNo and PaymentStatus='UnPaid'        
        
--select SubscriptionType as MembershipType,SubscriptionPaid as SubscriptionAmountPaid,Status as PaymentStatus from SubscriptionsCollected where  MembershipNo=@MembershipNo        
-- and Convert(varchar(15),PaidDate,103)=Convert(varchar(15),@BillDate,103)        
         
 select * from(        
Select  MembershipType,pTable.MembershipNo, Payable,case when amtPaid is null then 0 else amtPaid end as AmtPaid, case when amtPaid is null then payable else payable - amtPaid end as 'ToBePaid',        
case when payable-amtPaid<0 then 'Paid' else 'UnPaid' end as 'Status'        
from         
(        
 Select MembershipType,MemberShipStartDate, coreTab.MembershipNo, Sum(payable) as payable        
 From        
 (        
  Select Relcharge.MembershipNo, MemberShipStartDate, MembershipType, memShipStartDate, memShipEndDate,        
    SubscriptionValidity, SubscriptionRate,        
    Case when MembershipType = 'PM Monthly' and getDate() >= memShipEndDate then 12 * SubscriptionRate        
      when MembershipType = 'PM Monthly' and getDate() < memShipEndDate then (DateDiff(MONTH, memShipStartDate, GETDATE()) + 1)  * SubscriptionRate        
    else SubscriptionRate end payable, (DateDiff(MONTH, memShipStartDate, GETDATE()) + 1) as idx        
  From        
  (        
   Select memb.MembershipNo, MemberShipStartDate, MembershipType, memShipStartDate,  DATEADD(DAY,-1, DATEADD(year,1,memShipStartDate)) as memShipEndDate,        
    ROW_NUMBER() over (Partition By memb.MembershipNo, memShipStartDate order by memb.MembershipNo, memShipStartDate, SubscriptionValidity asc) corres,         
    SubscriptionValidity, SubscriptionRate        
   from        
   (        
    Select Member.MembershipNo, Member.MemberShipStartDate, Member.MembershipType, seq, memShipStartDate        
    from Member        
    OUTER APPLY        
     (        
      SELECT ones.n + 10*tens.n as seq,  DATEADD(year,ones.n + 10*tens.n, Member.MemberShipStartDate) as memShipStartDate        
      FROM (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9)) ones(n),        
        (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9)) tens(n)        
      WHERE ones.n + 10*tens.n BETWEEN 0 AND datediff(year,Member.MemberShipStartDate,getdate())        
     ) a         
   ) memb        
   left outer join Subscription on Subscription.SubscriptionType = MembershipType and SubscriptionValidity >= memShipStartDate        
  ) Relcharge        
  left outer join SubscriptionsCollected on SubscriptionsCollected.MembershipNo = Relcharge.MembershipNo        
  Where corres = 1        
 )coreTab        
 Group by MembershipNo,MemberShipStartDate,MembershipType        
)pTable        
left outer join (        
 Select MembershipNo, sum(SubscriptionPaid) as amtPaid        
 from SubscriptionsCollected        
 Where PaidDate <= GETDATE()        
 Group by MembershipNo        
) paidTab on paidTab.MembershipNo = pTable.MembershipNo        
)as a where ToBePaid>0        
and MembershipNo=@MembershipNo and Convert(date,@BillDate,103)=Convert(date,getdate(),103)        
        
        
        
        
 --OtherServices        
 select distinct ST.SerTransId as 'OrderId', S.ServiceName, ST.MembershipNo,ST.PaymentStatus as 'PaidStatus',ST.Charges from ServiceTransactionTable ST join        
 Service S on ST.ServiceCode=S.ServiceCode        
   where PaymentStatus='UnPaid'   and ST.MembershipNo=@MembershipNo and Convert(varchar(12),ST.ExitTime,103)=Convert(varchar(12),        
 @BillDate,103)        
         
 --Room Booking        
   select distinct R.FromDate,Convert(varchar(16),R.FromTime,100) as 'FromTime',      
   R.ToDate,Convert(varchar(16),R.ToTime,100) as 'ToTime', R.BookingId as 'OrderId', 'RoomBooking' as 'ServiceName', R.MemberId as 'MembershipNo',      
   R.PaidStatus,R.Charges from RoomBooking R where  R.PaidStatus='UnPaid'         
   and MemberId=@MembershipNo and  Convert(varchar(12),FromDate,103)=Convert(varchar(12),@BillDate,103)        
        
 end        
        
        
 else if(@Flag=1)        
 begin        
  --FoodOrder        
   select distinct FO.Date, FO.MembershipNo, FOL.OrderId, FOL.FoodName,FOL.Quantity,FOL.Price,FOL.GST from FoodOrderList FOL join         
 FoodOrder FO on Fo.OrderId=FOL.OrderId where FO.MembershipNo=@MembershipNo and FO.Status='Paid' and FOL.Status='Completed'        
 and Date=@BillDate        
         
        
         
 --LiquorOrder        
  select distinct LO.Date, LO.MembershipNo, LOL.OrderId, LOL.LiquorName,LOL.Quantity,LOL.Price,LOL.GST, LO.WaiterName from LiquorOrderList LOL join         
 LiquorOrder LO on LO.OrderId=LOL.OrderId where LO.MembershipNo=@MembershipNo and LO.Status='Paid' and LOL.Status='Completed'        
 and Date=@BillDate        
        
        
        
        
 --subscription Type        
 --select MembershipType,SubscriptionAmountPaid,PaymentStatus from Member where  MembershipNo=@MembershipNo        
 select distinct SubscriptionType as MembershipType,SubscriptionPaid as SubscriptionAmountPaid,Status as PaymentStatus        
  from SubscriptionsCollected where  MembershipNo=@MembershipNo        
 and Convert(varchar(15),PaidDate,103)=Convert(varchar(15),@BillDate,103)         
        
        
  --OtherServices        
        
 select distinct 'RoomBooking' as 'ServiceName', R.MemberId as 'MembershipNo',R.PaidStatus,R.Charges from RoomBooking R where  R.PaidStatus='Paid'         
   and MemberId=@MembershipNo and Convert(varchar(12),FromDate,103)=Convert(varchar(12),@BillDate,103)        
 union all        
 select distinct S.ServiceName, ST.MembershipNo,ST.PaymentStatus as 'PaidStatus',ST.Charges from ServiceTransactionTable ST join        
 Service S on ST.ServiceCode=S.ServiceCode        
   where PaymentStatus='Paid'   and ST.MembershipNo=@MembershipNo and Convert(varchar(12),ST.ExitTime,103)=Convert(varchar(12),        
   @BillDate,103)        
 end        
        
 end 
--$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
--Change If Required
Alter proc [dbo].[spFoodDailyReport]  
as  
begin  
--today's Food report--  
 --select Convert(varchar(12),F.Date,103) as 'Date', FL.FoodName,Count(FL.FoodName)  as 'Count',F.TotalAmount,FL.Quantity,F.MembershipNo  from  
 -- FoodOrderList as FL join FoodOrder as F on FL.OrderId=F.OrderId where Convert(varchar(12),F.Date,103)=  
 -- convert(varchar(12),getdate(),103)  group by   
 -- FL.FoodName,F.MembershipNo,FL.Quantity,F.TotalAmount,F.Date  order by Count(FL.FoodName) desc  
 --   
  select Convert(varchar(12),Date,103) as 'Date',OrderId as 'BillNo',MembershipNo,GrossAmount,TotalGST,TotalAmount from FoodOrder  
  where Convert(varchar(12),Date,103)=  convert(varchar(12),getdate(),103) group by Date,OrderId,MembershipNo,GrossAmount,  
  TotalGST,TotalAmount  
   
 end  
 --$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

 Alter proc [dbo].[sp_InsertAllLiquororder]    
(    
@UserName varchar(32),    
@MembershipNo varchar(32),    
@TotalGST float,    
@Status varchar (32),    
@TableNo int,    
@TotalAmount float,    
@OrderId int output,    
@GrossAmount Float,
@WaiterName VarChar(100)
)    
As    
Begin    
 Begin Try  
  Begin Transaction    
   Declare @MemberAvailability Int = 0  
   Select @MemberAvailability = Count(MemberShipNo) from Member Where MembershipNo = @MembershipNo  
   If @MemberAvailability = 0  
    RAISERROR ('Membership Number Not Available', 16, 1)  
   declare @Date1 Date    
   set @Date1=(select Getdate())    
   Insert into [dbo].[LiquorOrder]  (Date,UserName,MembershipNo,TotalGST,Status,TableNo,TotalAmount,GrossAmount,WaiterName)values(@Date1,@UserName,@MembershipNo,@TotalGST,@Status,@TableNo,@TotalAmount,@GrossAmount,@WaiterName)    
   select @@Identity    
   set @OrderId = @@Identity    
   declare @LiquorOrderId int = @@Identity    
   declare @ServiceType int = '2'    
   insert into dbo.TempPayment (OrderAmount,MembershipNo,OrderId,TotalOrderAmount,Tax,ServiceType,BillDate,Status) values (@GrossAmount,@MembershipNo,@LiquorOrderId,@TotalAmount,@TotalGST,@ServiceType,@Date1,@Status)    
  commit transaction    
 End Try  
 Begin Catch  
  Select Error_Message() as ErrorMessage  
 End Catch  
  
End  

--$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$