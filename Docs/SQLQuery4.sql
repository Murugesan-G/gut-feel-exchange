select * from LiquorOrder where Date = '25-Feb-2023' and OrderId in (257784,257785,257816)
select * from LiquorOrderList where OrderId in (257784,257785,257816)
Select * From Member order by MembershipNo

select * from LiquorOrder where Date = '03-Mar-2023'

select * from LiquorOrder where Date = '25-Feb-2023' 
select * from LiquorOrderList where OrderId in (select OrderId from LiquorOrder where Date = '25-Feb-2023' )

Select * From LiquorOrder Where OrderId in (257784,257785)
Select * From LiquorOrderList Where OrderId in (257784,257785)

Delete From LiquorOrderList Where OrderId in (257784,257785)
Delete From LiquorOrder Where OrderId in (257784,257785)

select * from LiquorOrder where OrderId in (Select OrderId From LiquorOrderList where LiquorName ='BREEZER CRANBERRY') and Date = '25-Feb-2023'

select * from LiquorOrder where Date = '24-Feb-2023' 
Update LiquorOrder Set Status = 'UnPaid' where OrderId = 257619 -- 231
select * from LiquorOrder where Status != 'Paid'
--UnPaid	NULL	NULL

