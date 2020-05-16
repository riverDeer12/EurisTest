
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/15/2020 00:42:44
-- Generated from EDMX file: E:\_projects\EURISTest_vs2015_EN\EURIS.Entities\LocalDbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LocalDbEntities];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProductCatalog_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductCatalog] DROP CONSTRAINT [FK_ProductCatalog_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductCatalog_Catalog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductCatalog] DROP CONSTRAINT [FK_ProductCatalog_Catalog];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[Catalog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Catalog];
GO
IF OBJECT_ID(N'[dbo].[ProductCatalog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductCatalog];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Product'
CREATE TABLE [dbo].[Product] (
    [ProductId] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(10)  NOT NULL,
    [Description] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Catalog'
CREATE TABLE [dbo].[Catalog] (
    [CatalogId] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProductCatalog'
CREATE TABLE [dbo].[ProductCatalog] (
    [Product_ProductId] int  NOT NULL,
    [Catalog_CatalogId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ProductId] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [PK_Product]
    PRIMARY KEY CLUSTERED ([ProductId] ASC);
GO

-- Creating primary key on [CatalogId] in table 'Catalog'
ALTER TABLE [dbo].[Catalog]
ADD CONSTRAINT [PK_Catalog]
    PRIMARY KEY CLUSTERED ([CatalogId] ASC);
GO

-- Creating primary key on [Product_ProductId], [Catalog_CatalogId] in table 'ProductCatalog'
ALTER TABLE [dbo].[ProductCatalog]
ADD CONSTRAINT [PK_ProductCatalog]
    PRIMARY KEY CLUSTERED ([Product_ProductId], [Catalog_CatalogId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Product_ProductId] in table 'ProductCatalog'
ALTER TABLE [dbo].[ProductCatalog]
ADD CONSTRAINT [FK_ProductCatalog_Product]
    FOREIGN KEY ([Product_ProductId])
    REFERENCES [dbo].[Product]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Catalog_CatalogId] in table 'ProductCatalog'
ALTER TABLE [dbo].[ProductCatalog]
ADD CONSTRAINT [FK_ProductCatalog_Catalog]
    FOREIGN KEY ([Catalog_CatalogId])
    REFERENCES [dbo].[Catalog]
        ([CatalogId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCatalog_Catalog'
CREATE INDEX [IX_FK_ProductCatalog_Catalog]
ON [dbo].[ProductCatalog]
    ([Catalog_CatalogId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------