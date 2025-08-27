create database [api-million]

IF OBJECT_ID('dbo.Owner', 'U') IS NOT NULL
DROP TABLE dbo.Owner;
GO

CREATE TABLE [Owner] (
    [Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Address NVARCHAR(255) NULL,
    Photo VARBINARY(MAX) NULL,
    Birthday DATE NULL,

    [Code] [nvarchar](250) NULL,
	[DateCreation] [datetime2](7) NOT NULL,
	[Status] [nvarchar](250) NULL,
    [CodeClient] [nvarchar](250) NULL,
	[DateLastUpdate] [datetime2](7) NULL
);

IF OBJECT_ID('dbo.Property', 'U') IS NOT NULL
DROP TABLE dbo.Property;
GO
CREATE TABLE Property (
   [Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    Price DECIMAL(10, 2) NULL,
    CodeInternal NVARCHAR(50) UNIQUE NULL,
    Year INT NULL,
    IdOwner INT NOT NULL,

    [Code] [nvarchar](250) NULL,
	[DateCreation] [datetime2](7) NOT NULL,
	[Status] [nvarchar](250) NULL,
    [CodeClient] [nvarchar](250) NULL,
	[DateLastUpdate] [datetime2](7) NULL
    
    CONSTRAINT FK_Property_Owner FOREIGN KEY (IdOwner)
        REFERENCES [Owner] (Id)
);

IF OBJECT_ID('dbo.PropertyTrace', 'U') IS NOT NULL
DROP TABLE dbo.PropertyTrace;
GO
CREATE TABLE PropertyTrace (
    [Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
    IdProperty INT NOT NULL,
    DateSale DATE NULL,
    Name NVARCHAR(255) NOT NULL,
    Value DECIMAL(10, 2) NULL,
    Tax DECIMAL(10, 2) NULL,

    [Code] [nvarchar](250) NULL,
	[DateCreation] [datetime2](7) NOT NULL,
	[Status] [nvarchar](250) NULL,
    [CodeClient] [nvarchar](250) NULL,
	[DateLastUpdate] [datetime2](7) NULL
    
    CONSTRAINT FK_PropertyTrace_Property FOREIGN KEY (IdProperty)
        REFERENCES Property (Id)
);

IF OBJECT_ID('dbo.PropertyImage', 'U') IS NOT NULL
DROP TABLE dbo.PropertyImage;
GO
CREATE TABLE PropertyImage (
    [Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
    IdProperty INT NOT NULL,
    FileContent VARBINARY(MAX) NOT NULL,
    Enabled BIT CONSTRAINT DF_PropertyImage_Enabled DEFAULT 1 NOT NULL,

    [Code] [nvarchar](250) NULL,
	[DateCreation] [datetime2](7) NOT NULL,
	[Status] [nvarchar](250) NULL,
    [CodeClient] [nvarchar](250) NULL,
	[DateLastUpdate] [datetime2](7) NULL
    
    CONSTRAINT FK_PropertyImage_Property FOREIGN KEY (IdProperty)
        REFERENCES Property (Id)
);
