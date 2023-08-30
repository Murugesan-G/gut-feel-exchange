select * from FoodOrderList
select * from FoodOrder
sp_depends LiquorOrder

select * from LiquorOrder
Select * From Member

Update Member Set MobileNo = '8494947490', AltMobileNo = '8494947490',EmailId='murugesh1506@gmail.com'

--$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

Alter Table LiquorOrder Add WaiterName VarChar(100)
Update LiquorOrder Set WaiterName = ''

Alter Table Member Add MemberType VarChar(50)
Update Member Set MemberType = ''

Alter Table Member Add Salutation VarChar(50)
Update Member Set Salutation = ''


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

Alter PROCEDURE [dbo].[spInsertMemberDetails] (  
     @MemberId varchar(10),  
     @MembershipNo varchar(32),  
           @ClubId int,  
           @MemberName varchar(32),  
           @DOB datetime,  
           @Gender varchar(12),  
           @Address varchar(256),  
           @MobileNo varchar(18),  
           @AltMobileNo varchar(18),  
           @EmailId varchar(32),  
           @ProximityCardNo varchar(32),  
           @Guests varchar(100),  
           @GuestCards varchar(50),  
           @AmenitiesInterested varchar(128),  
           @MembershipType varchar(18),  
           @MemberSince datetime,  
           @MemberShipStartDate datetime,  
           @MemberShipStatus varchar(12),  
           @InitialMembershipAmount float,  
           @MembershipValidity datetime,  
           @LastSubscriptionPaid float,  
           @SubscriptionAmountPaid float,  
     @SpouseName varchar (32),  
     @FathersName varchar (32),  
     @Child1sName varchar (32),  
     @Child2sName varchar (32),  
     @Alive varchar (32),  
     @Qualification varchar (32),  
     @MaritalStatus varchar (32),  
     @Profession varchar (32),  
     @DOBOfChild1 datetime,  
     @DOBOfChild2 datetime,  
     @DOBOfSpouse datetime,  
     @DOBOfFather datetime,  
     @Hobbies varchar(32),  
     @Balance float,  
     @PaymentStatus varchar(12) ,
	 @MemberType VarChar(50),
	 @Salutation VarChar(50)
     )  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
insert into Member Values(@MemberId,@MembershipNo, @ClubId,@MemberName,@DOB,@Gender,@Address,@MobileNo,  
@AltMobileNo,@EmailId,@ProximityCardNo,@Guests,@GuestCards,@AmenitiesInterested,@MembershipType,  
@MemberSince,@MemberShipStartDate,@MemberShipStatus,@InitialMembershipAmount,@MembershipValidity,  
@LastSubscriptionPaid,@SubscriptionAmountPaid,@SpouseName,@FathersName,@Child1sName,@Child2sName,@Alive,@Qualification,@MaritalStatus,  
@Profession,@DOBOfChild1,@DOBOfChild2,@DOBOfSpouse,@DOBOfFather,@Hobbies,@Balance,@PaymentStatus,@MemberType,@Salutation
)  
end  
  
--$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

Alter PROCEDURE [dbo].[spUpdateMemberDetails] (    
 @MemId int,    
    @MemberId varchar(10),    
    @MembershipNo varchar(10),    
    @ClubId int,    
    @MemberName varchar(32),    
    @DOB datetime,    
    @Gender varchar(12),    
    @Address varchar(256),    
    @MobileNo varchar(18),    
    @AltMobileNo varchar(18),    
    @EmailId varchar(32),    
    @ProximityCardNo varchar(32),    
    @Guests varchar(100),    
    @GuestCards varchar(50),    
    @AmenitiesInterested varchar(128),    
    @MembershipType varchar(18),    
    @MemberSince datetime,    
    @MemberShipStartDate datetime,    
    @MemberShipStatus varchar(12),    
    @InitialMembershipAmount float,    
    @MembershipValidity datetime,    
    @LastSubscriptionPaid float,    
    @SubscriptionAmountPaid float,    
    @SpouseName varchar (32),    
    @FathersName varchar (32),    
    @Child1sName varchar (32),    
    @Child2sName varchar (32),    
    @Alive varchar (32),    
    @Qualification varchar (32),    
    @MaritalStatus varchar (32),    
    @Profession varchar (32),    
    @DOBOfChild1 datetime,    
    @DOBOfChild2 datetime,    
    @DOBOfSpouse datetime,    
    @DOBOfFather datetime,    
    @Hobbies varchar(32),    
    @Balance float,    
    @PaymentStatus varchar(12),
	@MemberType VarChar(50),
	@Salutation VarChar(50)
)    
AS    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
  
 --SET NOCOUNT ON;    
    
  UPDATE Member SET MemberId=@MemberId,MembershipNo=@MembershipNo,ClubId=@ClubId,MemberName=@MemberName,DOB=@DOB,Gender=@Gender,[Address]=@Address,MobileNo=@MobileNo    
  ,AltMobileNo=@AltMobileNo,EmailId=@EmailId,ProximityCardNo=@ProximityCardNo,Guests=@Guests,GuestCards=@GuestCards,    
  AmenitiesInterested=@AmenitiesInterested,MembershipType=@MembershipType,MemberSince=@MemberSince,MemberShipStartDate=@MemberShipStartDate,    
  MemberShipStatus=@MemberShipStatus,InitialMembershipAmount=@InitialMembershipAmount,MembershipValidity=@MembershipValidity,    
  LastSubscriptionPaid=@LastSubscriptionPaid,SubscriptionAmountPaid=@SubscriptionAmountPaid,    
 SpouseName=@SpouseName,FathersName=@FathersName,Child1sName=@Child1sName,Child2sName=@Child2sName,Alive=@Alive,    
Qualification= @Qualification,MaritalStatus=@MaritalStatus,    
Profession=@Profession,DOBOfChild1=@DOBOfChild1,DOBOfChild2=@DOBOfChild2,DOBOfSpouse=@DOBOfSpouse,    
DOBOfFather=@DOBOfFather,Hobbies=@Hobbies,Balance=@Balance,PaymentStatus=@PaymentStatus, MemberType = @MemberType, Salutation = @Salutation  WHERE MemId=@MemId    
    
--DECLARE @PaidDate as date    
--SET @PaidDate = GetDate()    
    
--Insert into dbo.SubscriptionsCollected values(@MembershipNo,@SubscriptionAmountPaid,@MembershipType,@PaidDate,@PaymentStatus);    
    
END    
  
--$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
Alter PROCEDURE [dbo].[spViewMember](  
@MemId int  
)  
  
AS  
SELECT   MemId,  
   MemberId,  
     MembershipNo,  
           ClubId ,  
           MemberName,  
           Convert(varchar(12),DOB,103) as 'DOB',  
           Gender,  
           Address,  
           MobileNo,  
           AltMobileNo,  
           EmailId,  
           ProximityCardNo,  
           Guests,  
           GuestCards,  
           AmenitiesInterested,  
           MembershipType,  
           Convert(varchar(12),MemberSince,103)   as 'MemberSince',  
           Convert(varchar(12),MemberShipStartDate,103)  as 'MemberShipStartDate' ,  
           MemberShipStatus,  
           InitialMembershipAmount ,  
           Convert(varchar(12),MembershipValidity,103)  as 'MembershipValidity',  
           LastSubscriptionPaid ,  
           SubscriptionAmountPaid,SpouseName,FathersName,Child1sName,Child2sName,Alive,Qualification,MaritalStatus,Profession,    
    Convert(varchar(12),DOBOfChild1,103)  as 'DOBOfChild1' ,   
    Convert(varchar(12),DOBOfChild2,103)  as 'DOBOfChild2' ,   
    Convert(varchar(12),DOBOfSpouse,103)  as 'DOBOfSpouse' ,   
    Convert(varchar(12),DOBOfFather,103)  as 'DOBOfFather' ,   
    Hobbies,Balance,PaymentStatus,MemberType,Salutation FROM Member WHERE MemId=@MemId;    
      
--$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$