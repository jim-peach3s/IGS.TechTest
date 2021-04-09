IF NOT EXISTS(SELECT * FROM sys.databases WHERE [name] = 'igsmarketplace')
	BEGIN
		CREATE DATABASE [igsmarketplace]
	END
GO
USE [igsmarketplace]
GO
IF NOT EXISTS(SELECT * FROM sys.tables WHERE [name] = 'Products')
	BEGIN
		CREATE TABLE Products(
			ProductCode INT PRIMARY KEY IDENTITY(1,1),
			Name VARCHAR(100) NOT NULL,
			Price MONEY NOT NULL,
		)
	END
GO
IF NOT EXISTS(SELECT 1 FROM [dbo].[Products])
	BEGIN
		SET IDENTITY_INSERT [dbo].[Products] ON;
		INSERT INTO [dbo].[Products] ([ProductCode], [Name], [Price]) 
		VALUES  (1, 'Lavender heart', 9.25),
				(2, 'Personalised cufflinks', 45),
				(3, 'Kids T-shirt', 19.95);
		SET IDENTITY_INSERT [dbo].[Products] OFF;
	END