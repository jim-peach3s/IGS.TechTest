DECLARE @MAX INT;
SELECT @MAX=MAX([ProductCode]) FROM [dbo].[Products];
IF @MAX IS NULL   --check when max is returned as null
  SET @MAX = 0;
DBCC CHECKIDENT ('[dbo].[Products]', RESEED, @max);