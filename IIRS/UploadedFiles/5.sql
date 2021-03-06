USE [BuildersAlliances]
GO
/****** Object:  StoredProcedure [dbo].[GetItems]    Script Date: 8/7/2016 12:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[GetItems] --0,10,"asc"
(
@offset int ,
@limit int,
@order varchar(10)=null,
@sort varchar(100)=null,
@ItemSKU nvarchar(100)=null,
@Manufacturer varchar(100)=null,
@ItemName varchar(100)=null,
@ItemColor varchar(100)=null
--@Size int=0
)
as
Begin
;WITH CTE AS (
  select Items.*,Manufacturer.ManufacturerName, Colors.ColorName,DoorStyle.StyleName
   from Items inner join Manufacturer on Items.ManufacturerId=Manufacturer.ManufacturerId left join
  Colors on Colors.ColorId=Items.ColorId left join
  DoorStyle on DoorStyle.DoorId=Items.DoorStyleId
where ItemSKU like case when @ItemSKU is null then ItemSKU else '%'+@ItemSKU+'%' end
and  ManufacturerName like case when @Manufacturer is null then ManufacturerName else '%'+@Manufacturer+'%' end
and  ItemName like case when @ItemName is null then ItemName else '%'+@ItemName+'%' end
and  ISNULL(Colors.ColorName,'') like case when @ItemColor is null then ISNULL(Colors.ColorName,'') else '%'+@ItemColor+'%' end
and Items.IsDeleted=0 
--and  Size = case when @Size=0 then Size else @Size end
)

SELECT 
    CTE.*,
    tCountOrders.CountOrders AS TotalRows
FROM CTE
    CROSS JOIN (SELECT Count(*) AS CountOrders FROM CTE) AS tCountOrders

order by	case when @sort='ManufacturerName' and @order ='asc' then ManufacturerName end ASC,
		case when @sort='ManufacturerName' and @order ='desc'then ManufacturerName end DESC,
		case when @sort='ItemSKU' and @order ='asc' then ItemSKU end ASC,
		case when @sort='ItemSKU' and @order ='desc'then ItemSKU end DESC,
		case when @sort='ItemName' and @order ='asc'then ItemName end ASC,
		case when @sort='ItemName' and @order ='desc'then ItemName end DESC,
		case when @sort='ItemId' and @order ='asc'then ItemId end DESC

OFFSET @offset ROWS
FETCH NEXT @limit ROWS ONLY;

End