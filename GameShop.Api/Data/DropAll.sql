USE [master];
GO

ALTER DATABASE [GameShopDb] SET single_user WITH ROLLBACK IMMEDIATE;
GO

DROP DATABASE [GameShopDb];
GO