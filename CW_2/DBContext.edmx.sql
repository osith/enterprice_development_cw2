
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/21/2020 01:03:17
-- Generated from EDMX file: D:\MSc\Enterprice Development\CW2\CW_2\CW_2\CW_2\DBContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DataBaseFile];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserDataContactData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContactDatas] DROP CONSTRAINT [FK_UserDataContactData];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDataTransactionEnity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionEnities] DROP CONSTRAINT [FK_UserDataTransactionEnity];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactDataTransactionEnity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransactionEnities] DROP CONSTRAINT [FK_ContactDataTransactionEnity];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDataEventEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventEntities] DROP CONSTRAINT [FK_UserDataEventEntity];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserDatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDatas];
GO
IF OBJECT_ID(N'[dbo].[ContactDatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactDatas];
GO
IF OBJECT_ID(N'[dbo].[TransactionEnities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionEnities];
GO
IF OBJECT_ID(N'[dbo].[EventEntities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventEntities];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserDatas'
CREATE TABLE [dbo].[UserDatas] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(10)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NULL,
    [CreatedAt] datetime  NULL
);
GO

-- Creating table 'ContactDatas'
CREATE TABLE [dbo].[ContactDatas] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Contact] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NULL,
    [AccountNo] nvarchar(max)  NULL,
    [ExpectAmt] float  NULL,
    [BankName] nvarchar(max)  NOT NULL,
    [CreatedAt] datetime  NULL,
    [IsDeleted] bit  NULL,
    [UserDataId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'TransactionEnities'
CREATE TABLE [dbo].[TransactionEnities] (
    [Id] uniqueidentifier  NOT NULL,
    [Amount] float  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [IsDeleted] bit  NULL,
    [Recurring] bit  NULL,
    [RecurringType] int  NULL,
    [UserDataId] uniqueidentifier  NOT NULL,
    [ContactDataId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'EventEntities'
CREATE TABLE [dbo].[EventEntities] (
    [Id] uniqueidentifier  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Type] smallint  NOT NULL,
    [EventCat] smallint  NOT NULL,
    [EventAt] nvarchar(max)  NOT NULL,
    [EventOn] nvarchar(max)  NOT NULL,
    [Recurring] bit  NULL,
    [RecurringType] int  NULL,
    [UserDataId] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserDatas'
ALTER TABLE [dbo].[UserDatas]
ADD CONSTRAINT [PK_UserDatas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ContactDatas'
ALTER TABLE [dbo].[ContactDatas]
ADD CONSTRAINT [PK_ContactDatas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TransactionEnities'
ALTER TABLE [dbo].[TransactionEnities]
ADD CONSTRAINT [PK_TransactionEnities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventEntities'
ALTER TABLE [dbo].[EventEntities]
ADD CONSTRAINT [PK_EventEntities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserDataId] in table 'ContactDatas'
ALTER TABLE [dbo].[ContactDatas]
ADD CONSTRAINT [FK_UserDataContactData]
    FOREIGN KEY ([UserDataId])
    REFERENCES [dbo].[UserDatas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDataContactData'
CREATE INDEX [IX_FK_UserDataContactData]
ON [dbo].[ContactDatas]
    ([UserDataId]);
GO

-- Creating foreign key on [UserDataId] in table 'TransactionEnities'
ALTER TABLE [dbo].[TransactionEnities]
ADD CONSTRAINT [FK_UserDataTransactionEnity]
    FOREIGN KEY ([UserDataId])
    REFERENCES [dbo].[UserDatas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDataTransactionEnity'
CREATE INDEX [IX_FK_UserDataTransactionEnity]
ON [dbo].[TransactionEnities]
    ([UserDataId]);
GO

-- Creating foreign key on [ContactDataId] in table 'TransactionEnities'
ALTER TABLE [dbo].[TransactionEnities]
ADD CONSTRAINT [FK_ContactDataTransactionEnity]
    FOREIGN KEY ([ContactDataId])
    REFERENCES [dbo].[ContactDatas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactDataTransactionEnity'
CREATE INDEX [IX_FK_ContactDataTransactionEnity]
ON [dbo].[TransactionEnities]
    ([ContactDataId]);
GO

-- Creating foreign key on [UserDataId] in table 'EventEntities'
ALTER TABLE [dbo].[EventEntities]
ADD CONSTRAINT [FK_UserDataEventEntity]
    FOREIGN KEY ([UserDataId])
    REFERENCES [dbo].[UserDatas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDataEventEntity'
CREATE INDEX [IX_FK_UserDataEventEntity]
ON [dbo].[EventEntities]
    ([UserDataId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------