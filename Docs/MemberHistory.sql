Select * From Member

Alter Table Member Add MemberPhoto NVarChar(Max)
Update Member Set MemberPhoto = ''

Create Table WaiterDetails
(
	Waiter_Id Int Not Null,
	Waiter_Name NVarChar(50)
)

Insert Into WaiterDetails Values (1,'Madhusudhan R M')
Insert Into WaiterDetails Values (2,'Prashanth Kumar B')
Insert Into WaiterDetails Values (3,'Ganesh SC')
Insert Into WaiterDetails Values (4,'Santhosh Kumar')
Insert Into WaiterDetails Values (5,'Manjunatha C')
Insert Into WaiterDetails Values (6,'Varadaraj')
Insert Into WaiterDetails Values (7,'Nagaraja V')
Insert Into WaiterDetails Values (8,'Sreekanth ')
Insert Into WaiterDetails Values (9,'Arun Kumar CP')
Insert Into WaiterDetails Values (10,'Govinda Raj')

CREATE proc [dbo].[spGetWaiterName]    
(     
	@Waiter_Name varchar(50)    
)    
as    
begin    
 select Waiter_Name from WaiterDetails where Waiter_Name like '%'+@Waiter_Name+'%' order by Waiter_Name
end  

ALTER PROCEDURE [dbo].[spInsertMemberDetails] (  
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
	@Salutation VarChar(50),
	@MemberPhoto NVarChar(Max)
     )  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
insert into Member (MemberId,MembershipNo, ClubId,MemberName,DOB,Gender,Address,MobileNo,  
AltMobileNo,EmailId,ProximityCardNo,Guests,GuestCards,AmenitiesInterested,MembershipType,  
MemberSince,MemberShipStartDate,MemberShipStatus,InitialMembershipAmount,MembershipValidity,  
LastSubscriptionPaid,SubscriptionAmountPaid,SpouseName,FathersName,Child1sName,Child2sName,Alive,Qualification,MaritalStatus,  
Profession,DOBOfChild1,DOBOfChild2,DOBOfSpouse,DOBOfFather,Hobbies,Balance,PaymentStatus,MemberType,Salutation,MemberPhoto)
Values(@MemberId,@MembershipNo, @ClubId,@MemberName,@DOB,@Gender,@Address,@MobileNo,  
@AltMobileNo,@EmailId,@ProximityCardNo,@Guests,@GuestCards,@AmenitiesInterested,@MembershipType,  
@MemberSince,@MemberShipStartDate,@MemberShipStatus,@InitialMembershipAmount,@MembershipValidity,  
@LastSubscriptionPaid,@SubscriptionAmountPaid,@SpouseName,@FathersName,@Child1sName,@Child2sName,@Alive,@Qualification,@MaritalStatus,  
@Profession,@DOBOfChild1,@DOBOfChild2,@DOBOfSpouse,@DOBOfFather,@Hobbies,@Balance,@PaymentStatus,@MemberType,@Salutation,@MemberPhoto
)  
end  

ALTER PROCEDURE [dbo].[spUpdateMemberDetails] (    
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
	@Salutation VarChar(50),
	@MemberPhoto NVarChar(Max)
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
DOBOfFather=@DOBOfFather,Hobbies=@Hobbies,Balance=@Balance,PaymentStatus=@PaymentStatus, MemberType = @MemberType, Salutation = @Salutation, MemberPhoto = @MemberPhoto  WHERE MemId=@MemId    
    
--DECLARE @PaidDate as date    
--SET @PaidDate = GetDate()    
    
--Insert into dbo.SubscriptionsCollected values(@MembershipNo,@SubscriptionAmountPaid,@MembershipType,@PaidDate,@PaymentStatus);    
    
END   

ALTER PROCEDURE [dbo].[spViewMember](  
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
    Hobbies,Balance,PaymentStatus,MemberType,Salutation,MemberPhoto FROM Member WHERE MemId=@MemId; 

Create Table Member_History
(
	MemberId NVarChar(50),
	MemberShipNo NVarChar(50),
	HistoryDate SmallDateTime,
	Comments NVarChar(Max),
	HistoryDocPath NVarChar(Max)
)
GO

Create Proc sp_InsertMemberHistory
(
	@MemberId NVarChar(50),
	@MemberShipNo NVarChar(50),
	@HistoryDate SmallDateTime,
	@Comments NVarChar(Max),
	@HistoryDocPath NVarChar(Max)
)
As
Begin
	Insert Into Member_History (MemberId,MemberShipNo,HistoryDate,Comments,HistoryDocPath)
	Values (@MemberId,@MemberShipNo,@HistoryDate,@Comments,@HistoryDocPath)
End
GO

Create Proc sp_UpdateMemberHistory
(
	@MemberId NVarChar(50),
	@MemberShipNo NVarChar(50),
	@HistoryDate SmallDateTime,
	@Comments NVarChar(Max),
	@HistoryDocPath NVarChar(Max)
)
As
Begin
		Update Member_History Set HistoryDate = @HistoryDate, Comments = @Comments, HistoryDocPath = @HistoryDocPath
		Where MemberId = @MemberId and MemberShipNo = @MemberShipNo
End
GO

Create Proc sp_DeleteMemberHistory
(
	@MemberId NVarChar(50),
	@MemberShipNo NVarChar(50)
)
As
Begin
	Delete Member_History Where MemberId = @MemberId and MemberShipNo = @MemberShipNo
End
GO

Create Proc sp_GetMemberHistory
(
	@MemberId NVarChar(50),
	@MemberShipNo NVarChar(50)
)
As
Begin
	Select MemberId,MemberShipNo,HistoryDate,Comments,HistoryDocPath From Member_History
	Where MemberId = @MemberId And MemberShipNo = @MemberShipNo
End
GO
