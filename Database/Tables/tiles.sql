CREATE TABLE [dbo].[tiles]
(
	[Id] INT NOT NULL  PRIMARY KEY IDENTITY, 
    [tileName] VARCHAR(50) NOT NULL, 
    [collisionMap] VARCHAR(50) NOT NULL, 
    [image] IMAGE NOT NULL
)
