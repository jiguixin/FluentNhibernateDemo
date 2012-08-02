select * from Categories

select * from dbo.Products order by ProductID desc
 where ProductID ='640'
 
 select * from Categories as c inner join Products as p on c.CategoryID = p.CategoryID
 where c.CategoryID = 24

select * from dbo.CustomerDetail

select * from dbo.Customers

select * from [Order Details]  where OrderID = 56

select * from dbo.Orders order by OrderID desc
 where OrderID = 42

select * from GoldOrders


select * FROM [Order Details]
WHERE       OrderID = 53 /* @p0 */
            AND ProductID = 650 /* @p1 */

