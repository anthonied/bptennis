
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/06/2013 08:42:57
-- Generated from EDMX file: C:\Development\Projects\bptennis\BPTennis.Data\bpTennis.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [bp_tennis];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_session_court_player_court]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[session_court_player] DROP CONSTRAINT [FK_session_court_player_court];
GO
IF OBJECT_ID(N'[dbo].[FK_session_court_player_player]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[session_court_player] DROP CONSTRAINT [FK_session_court_player_player];
GO
IF OBJECT_ID(N'[dbo].[FK_session_court_player_session]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[session_court_player] DROP CONSTRAINT [FK_session_court_player_session];
GO
IF OBJECT_ID(N'[dbo].[FK_session_players_player]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[session_players] DROP CONSTRAINT [FK_session_players_player];
GO
IF OBJECT_ID(N'[dbo].[FK_session_players_session]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[session_players] DROP CONSTRAINT [FK_session_players_session];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[comments];
GO
IF OBJECT_ID(N'[dbo].[court]', 'U') IS NOT NULL
    DROP TABLE [dbo].[court];
GO
IF OBJECT_ID(N'[dbo].[player]', 'U') IS NOT NULL
    DROP TABLE [dbo].[player];
GO
IF OBJECT_ID(N'[dbo].[session]', 'U') IS NOT NULL
    DROP TABLE [dbo].[session];
GO
IF OBJECT_ID(N'[dbo].[session_court_player]', 'U') IS NOT NULL
    DROP TABLE [dbo].[session_court_player];
GO
IF OBJECT_ID(N'[dbo].[session_players]', 'U') IS NOT NULL
    DROP TABLE [dbo].[session_players];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'courts'
CREATE TABLE [dbo].[courts] (
    [Id] int  NOT NULL,
    [name] nchar(10)  NULL
);
GO

-- Creating table 'players'
CREATE TABLE [dbo].[players] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nchar(20)  NULL,
    [surname] nchar(30)  NULL,
    [gender] nchar(10)  NULL,
    [telephone] nchar(15)  NULL,
    [email] nchar(30)  NULL,
    [status] nchar(10)  NULL
);
GO

-- Creating table 'sessions'
CREATE TABLE [dbo].[sessions] (
    [id] int IDENTITY(1,1) NOT NULL,
    [date] datetime  NOT NULL,
    [courts] varchar(50)  NULL
);
GO

-- Creating table 'session_court_player'
CREATE TABLE [dbo].[session_court_player] (
    [id] int  NOT NULL,
    [session_id] int  NOT NULL,
    [court_id] int  NOT NULL,
    [player_id] int  NOT NULL,
    [in_progress] bit  NOT NULL
);
GO

-- Creating table 'session_players'
CREATE TABLE [dbo].[session_players] (
    [id] int IDENTITY(1,1) NOT NULL,
    [session_id] int  NOT NULL,
    [player_id] int  NOT NULL,
    [player_order] int  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'comments1'
CREATE TABLE [dbo].[comments1] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(50)  NOT NULL,
    [comment] varchar(500)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'courts'
ALTER TABLE [dbo].[courts]
ADD CONSTRAINT [PK_courts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [id] in table 'players'
ALTER TABLE [dbo].[players]
ADD CONSTRAINT [PK_players]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'sessions'
ALTER TABLE [dbo].[sessions]
ADD CONSTRAINT [PK_sessions]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'session_court_player'
ALTER TABLE [dbo].[session_court_player]
ADD CONSTRAINT [PK_session_court_player]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'session_players'
ALTER TABLE [dbo].[session_players]
ADD CONSTRAINT [PK_session_players]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [id] in table 'comments1'
ALTER TABLE [dbo].[comments1]
ADD CONSTRAINT [PK_comments1]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [court_id] in table 'session_court_player'
ALTER TABLE [dbo].[session_court_player]
ADD CONSTRAINT [FK_session_court_player_court]
    FOREIGN KEY ([court_id])
    REFERENCES [dbo].[courts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_session_court_player_court'
CREATE INDEX [IX_FK_session_court_player_court]
ON [dbo].[session_court_player]
    ([court_id]);
GO

-- Creating foreign key on [player_id] in table 'session_court_player'
ALTER TABLE [dbo].[session_court_player]
ADD CONSTRAINT [FK_session_court_player_player]
    FOREIGN KEY ([player_id])
    REFERENCES [dbo].[players]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_session_court_player_player'
CREATE INDEX [IX_FK_session_court_player_player]
ON [dbo].[session_court_player]
    ([player_id]);
GO

-- Creating foreign key on [player_id] in table 'session_players'
ALTER TABLE [dbo].[session_players]
ADD CONSTRAINT [FK_session_players_player]
    FOREIGN KEY ([player_id])
    REFERENCES [dbo].[players]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_session_players_player'
CREATE INDEX [IX_FK_session_players_player]
ON [dbo].[session_players]
    ([player_id]);
GO

-- Creating foreign key on [session_id] in table 'session_court_player'
ALTER TABLE [dbo].[session_court_player]
ADD CONSTRAINT [FK_session_court_player_session]
    FOREIGN KEY ([session_id])
    REFERENCES [dbo].[sessions]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_session_court_player_session'
CREATE INDEX [IX_FK_session_court_player_session]
ON [dbo].[session_court_player]
    ([session_id]);
GO

-- Creating foreign key on [session_id] in table 'session_players'
ALTER TABLE [dbo].[session_players]
ADD CONSTRAINT [FK_session_players_session]
    FOREIGN KEY ([session_id])
    REFERENCES [dbo].[sessions]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_session_players_session'
CREATE INDEX [IX_FK_session_players_session]
ON [dbo].[session_players]
    ([session_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------