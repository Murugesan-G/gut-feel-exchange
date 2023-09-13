USE [NTCYSTAGING]
	INSERT INTO LiquorOpeningStock(StockId,LiquorId,OpeningStockBottle,OpeningStockPeg,Date)
	SELECT StockId,LiquorId,CurrentStockBottles,CurrentStockPegs,GETDATE()+1
	FROM LiquorStock
GO