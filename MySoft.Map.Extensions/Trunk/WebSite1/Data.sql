IF NOT OBJECT_ID('p_Room') IS NULL
BEGIN
	DROP TABLE p_Room
END
GO
CREATE TABLE p_Room
(
	RoomGUID UNIQUEIDENTIFIER,
	RoomInfo NVARCHAR(256),
	BldArea NUMERIC(18,2),
	TnArea NUMERIC(18,2),
	DjArea NVARCHAR(10),
	Price MONEY,
	TnPrice MONEY,
	Total MONEY,
	RawDjArea NVARCHAR(10),
	RawPrice MONEY,
	RawTnPrice MONEY
)
GO
INSERT INTO p_Room(RoomGUID,RoomInfo,BldArea,TnArea,DjArea,Total)
SELECT NEWID(),'A-3-402', 67.42, 52.93, '建筑面积', 368421
UNION ALL
SELECT NEWID(),'A-3-401', 67.42, 52.93, '套内面积', 369487
UNION ALL
SELECT NEWID(),'A-3-302', 76.34, 69.93, '套内面积', 354125
UNION ALL
SELECT NEWID(),'A-3-301', 76.67, 70.45, '套', 353298
UNION ALL
SELECT NEWID(),'A-3-202', 85.45, 64.21, '套', 412325
UNION ALL
SELECT NEWID(),'A-3-201', 85.48, 64.24, '建筑面积', 425698
UNION ALL
SELECT NEWID(),'A-3-102', 55.41, 45.20, '建筑面积', 284598
UNION ALL
SELECT NEWID(),'A-3-101', 55.20, 45.44, '建筑面积', 281892
GO
UPDATE p_Room SET Price = ROUND(Total / BldArea, 2), TnPrice = ROUND(Total / TnArea, 2)
GO
UPDATE p_room SET RawDjArea = DjArea,RawPrice=Price,RawTnPrice=TnPrice
GO
IF NOT OBJECT_ID('usp_GetAllRoom') IS NULL
BEGIN
	DROP PROC usp_GetAllRoom
END
GO
CREATE PROC usp_GetAllRoom
AS
BEGIN
	SELECT * FROM p_Room
END
GO
IF NOT OBJECT_ID('usp_GetSearchRoom') IS NULL
BEGIN
	DROP PROC usp_GetSearchRoom
END
GO
CREATE PROC usp_GetSearchRoom
(
	@RoomInfo NVARCHAR(36),
	@DjArea NVARCHAR(36)
)
AS
BEGIN
	IF @RoomInfo <> '' AND @DjArea <> '' 
	BEGIN
		SET @RoomInfo = '%' + @RoomInfo + '%'
		SELECT * FROM p_Room WHERE RoomInfo LIKE @RoomInfo AND DjArea = @DjArea
		RETURN 
	END
	IF @RoomInfo <> '' 
	BEGIN
		SET @RoomInfo = '%' + @RoomInfo + '%'
		SELECT * FROM p_Room WHERE RoomInfo LIKE @RoomInfo
		RETURN 
	END
	IF @DjArea <> '' 
	BEGIN
		SELECT * FROM p_Room WHERE DjArea = @DjArea
		RETURN 
	END
	SELECT * FROM p_Room
END
GO
IF NOT OBJECT_ID('usp_UpdateRoom') IS NULL
BEGIN
	DROP PROC usp_UpdateRoom
END
GO
CREATE PROC usp_UpdateRoom
(
	@RoomGUID UNIQUEIDENTIFIER,
	@Price MONEY,
	@TnPrice MONEY,
	@Total MONEY,
	@DjArea NVARCHAR(10)
)
AS
BEGIN
	UPDATE p_room SET RawDjArea = DjArea,RawPrice=Price,RawTnPrice=TnPrice WHERE RoomGUID = @RoomGUID
	UPDATE p_Room SET Price = @Price, TnPrice = @TnPrice, Total = @Total, DjArea = @DjArea WHERE RoomGUID = @RoomGUID
END
GO
