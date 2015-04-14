
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/13/2015 17:24:30
-- Generated from EDMX file: D:\Projects\CountdownMvc\CountdownDataBaseLayer\CountdownDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [mycountAEAqTcFWH];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CountdownSettingsDays]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Days] DROP CONSTRAINT [FK_CountdownSettingsDays];
GO
IF OBJECT_ID(N'[dbo].[FK_CountdownSettingsMonthes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Monthes] DROP CONSTRAINT [FK_CountdownSettingsMonthes];
GO
IF OBJECT_ID(N'[dbo].[FK_CountdownSettingsReminder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CountdownSettings] DROP CONSTRAINT [FK_CountdownSettingsReminder];
GO
IF OBJECT_ID(N'[dbo].[FK_CountdownSettingsWeeks]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Weeks] DROP CONSTRAINT [FK_CountdownSettingsWeeks];
GO
IF OBJECT_ID(N'[dbo].[FK_ExercisesFiles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Files] DROP CONSTRAINT [FK_ExercisesFiles];
GO
IF OBJECT_ID(N'[dbo].[FK_ImagesFiles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Images] DROP CONSTRAINT [FK_ImagesFiles];
GO
IF OBJECT_ID(N'[dbo].[FK_ProgressSettingsReminder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProgressSettings] DROP CONSTRAINT [FK_ProgressSettingsReminder];
GO
IF OBJECT_ID(N'[dbo].[FK_ReminderExercises]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Exercises] DROP CONSTRAINT [FK_ReminderExercises];
GO
IF OBJECT_ID(N'[dbo].[FK_ReminderReminderSettings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReminderSettings] DROP CONSTRAINT [FK_ReminderReminderSettings];
GO
IF OBJECT_ID(N'[dbo].[FK_ReminderUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reminders] DROP CONSTRAINT [FK_ReminderUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_SettingsReminderSettings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReminderSettings] DROP CONSTRAINT [FK_SettingsReminderSettings];
GO
IF OBJECT_ID(N'[dbo].[FK_TypeOfReminderReminder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reminders] DROP CONSTRAINT [FK_TypeOfReminderReminder];
GO
IF OBJECT_ID(N'[dbo].[FK_TypeOfReminderSettings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Settings] DROP CONSTRAINT [FK_TypeOfReminderSettings];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CountdownSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CountdownSettings];
GO
IF OBJECT_ID(N'[dbo].[Days]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Days];
GO
IF OBJECT_ID(N'[dbo].[Exercises]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Exercises];
GO
IF OBJECT_ID(N'[dbo].[Files]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Files];
GO
IF OBJECT_ID(N'[dbo].[Images]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Images];
GO
IF OBJECT_ID(N'[dbo].[Monthes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Monthes];
GO
IF OBJECT_ID(N'[dbo].[ProgressSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProgressSettings];
GO
IF OBJECT_ID(N'[dbo].[Reminders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reminders];
GO
IF OBJECT_ID(N'[dbo].[ReminderSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReminderSettings];
GO
IF OBJECT_ID(N'[dbo].[Settings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Settings];
GO
IF OBJECT_ID(N'[dbo].[TypeOfReminders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeOfReminders];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Weeks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Weeks];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Exercises'
CREATE TABLE [dbo].[Exercises] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [ReminderId] int  NULL
);
GO

-- Creating table 'Settings'
CREATE TABLE [dbo].[Settings] (
    [Id] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [TypeOfReminderId] int  NOT NULL,
    [NameProperty] nvarchar(max)  NULL,
    [NameElement] nvarchar(max)  NULL
);
GO

-- Creating table 'ReminderSettings'
CREATE TABLE [dbo].[ReminderSettings] (
    [ReminderId] int  NOT NULL,
    [SettingsId] int  NOT NULL,
    [ValueSetting] nvarchar(max)  NULL
);
GO

-- Creating table 'Reminders'
CREATE TABLE [dbo].[Reminders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [TypeOfReminderId] int  NOT NULL,
    [UserName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'TypeOfReminders'
CREATE TABLE [dbo].[TypeOfReminders] (
    [Id] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProgressSettings'
CREATE TABLE [dbo].[ProgressSettings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Interval] int  NOT NULL,
    [Duration] int  NOT NULL,
    [Start] datetime  NOT NULL,
    [Reminder_Id] int  NOT NULL
);
GO

-- Creating table 'CountdownSettings'
CREATE TABLE [dbo].[CountdownSettings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Duration] int  NOT NULL,
    [Start] datetime  NOT NULL,
    [Reminder_Id] int  NOT NULL
);
GO

-- Creating table 'Files'
CREATE TABLE [dbo].[Files] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [ExercisesId] int  NOT NULL
);
GO

-- Creating table 'Days'
CREATE TABLE [dbo].[Days] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CountdownSettingsId] int  NOT NULL,
    [Number] int  NULL
);
GO

-- Creating table 'Weeks'
CREATE TABLE [dbo].[Weeks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] int  NOT NULL,
    [CountdownSettingsId] int  NOT NULL
);
GO

-- Creating table 'Monthes'
CREATE TABLE [dbo].[Monthes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CountdownSettingsId] int  NOT NULL,
    [Number] int  NOT NULL
);
GO

-- Creating table 'Images'
CREATE TABLE [dbo].[Images] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(50)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [FilesId] int  NOT NULL,
    [Data] varbinary(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Password] nvarchar(max)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Exercises'
ALTER TABLE [dbo].[Exercises]
ADD CONSTRAINT [PK_Exercises]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [PK_Settings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ReminderId], [SettingsId] in table 'ReminderSettings'
ALTER TABLE [dbo].[ReminderSettings]
ADD CONSTRAINT [PK_ReminderSettings]
    PRIMARY KEY CLUSTERED ([ReminderId], [SettingsId] ASC);
GO

-- Creating primary key on [Id] in table 'Reminders'
ALTER TABLE [dbo].[Reminders]
ADD CONSTRAINT [PK_Reminders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TypeOfReminders'
ALTER TABLE [dbo].[TypeOfReminders]
ADD CONSTRAINT [PK_TypeOfReminders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProgressSettings'
ALTER TABLE [dbo].[ProgressSettings]
ADD CONSTRAINT [PK_ProgressSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CountdownSettings'
ALTER TABLE [dbo].[CountdownSettings]
ADD CONSTRAINT [PK_CountdownSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [PK_Files]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Days'
ALTER TABLE [dbo].[Days]
ADD CONSTRAINT [PK_Days]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Weeks'
ALTER TABLE [dbo].[Weeks]
ADD CONSTRAINT [PK_Weeks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Monthes'
ALTER TABLE [dbo].[Monthes]
ADD CONSTRAINT [PK_Monthes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [PK_Images]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Name] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Name] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ReminderId] in table 'ReminderSettings'
ALTER TABLE [dbo].[ReminderSettings]
ADD CONSTRAINT [FK_ReminderReminderSettings]
    FOREIGN KEY ([ReminderId])
    REFERENCES [dbo].[Reminders]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SettingsId] in table 'ReminderSettings'
ALTER TABLE [dbo].[ReminderSettings]
ADD CONSTRAINT [FK_SettingsReminderSettings]
    FOREIGN KEY ([SettingsId])
    REFERENCES [dbo].[Settings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SettingsReminderSettings'
CREATE INDEX [IX_FK_SettingsReminderSettings]
ON [dbo].[ReminderSettings]
    ([SettingsId]);
GO

-- Creating foreign key on [ReminderId] in table 'Exercises'
ALTER TABLE [dbo].[Exercises]
ADD CONSTRAINT [FK_ReminderExercises]
    FOREIGN KEY ([ReminderId])
    REFERENCES [dbo].[Reminders]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReminderExercises'
CREATE INDEX [IX_FK_ReminderExercises]
ON [dbo].[Exercises]
    ([ReminderId]);
GO

-- Creating foreign key on [TypeOfReminderId] in table 'Reminders'
ALTER TABLE [dbo].[Reminders]
ADD CONSTRAINT [FK_TypeOfReminderReminder]
    FOREIGN KEY ([TypeOfReminderId])
    REFERENCES [dbo].[TypeOfReminders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TypeOfReminderReminder'
CREATE INDEX [IX_FK_TypeOfReminderReminder]
ON [dbo].[Reminders]
    ([TypeOfReminderId]);
GO

-- Creating foreign key on [Reminder_Id] in table 'ProgressSettings'
ALTER TABLE [dbo].[ProgressSettings]
ADD CONSTRAINT [FK_ProgressSettingsReminder]
    FOREIGN KEY ([Reminder_Id])
    REFERENCES [dbo].[Reminders]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgressSettingsReminder'
CREATE INDEX [IX_FK_ProgressSettingsReminder]
ON [dbo].[ProgressSettings]
    ([Reminder_Id]);
GO

-- Creating foreign key on [Reminder_Id] in table 'CountdownSettings'
ALTER TABLE [dbo].[CountdownSettings]
ADD CONSTRAINT [FK_CountdownSettingsReminder]
    FOREIGN KEY ([Reminder_Id])
    REFERENCES [dbo].[Reminders]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CountdownSettingsReminder'
CREATE INDEX [IX_FK_CountdownSettingsReminder]
ON [dbo].[CountdownSettings]
    ([Reminder_Id]);
GO

-- Creating foreign key on [ExercisesId] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [FK_ExercisesFiles]
    FOREIGN KEY ([ExercisesId])
    REFERENCES [dbo].[Exercises]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ExercisesFiles'
CREATE INDEX [IX_FK_ExercisesFiles]
ON [dbo].[Files]
    ([ExercisesId]);
GO

-- Creating foreign key on [CountdownSettingsId] in table 'Days'
ALTER TABLE [dbo].[Days]
ADD CONSTRAINT [FK_CountdownSettingsDays]
    FOREIGN KEY ([CountdownSettingsId])
    REFERENCES [dbo].[CountdownSettings]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CountdownSettingsDays'
CREATE INDEX [IX_FK_CountdownSettingsDays]
ON [dbo].[Days]
    ([CountdownSettingsId]);
GO

-- Creating foreign key on [CountdownSettingsId] in table 'Weeks'
ALTER TABLE [dbo].[Weeks]
ADD CONSTRAINT [FK_CountdownSettingsWeeks]
    FOREIGN KEY ([CountdownSettingsId])
    REFERENCES [dbo].[CountdownSettings]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CountdownSettingsWeeks'
CREATE INDEX [IX_FK_CountdownSettingsWeeks]
ON [dbo].[Weeks]
    ([CountdownSettingsId]);
GO

-- Creating foreign key on [CountdownSettingsId] in table 'Monthes'
ALTER TABLE [dbo].[Monthes]
ADD CONSTRAINT [FK_CountdownSettingsMonthes]
    FOREIGN KEY ([CountdownSettingsId])
    REFERENCES [dbo].[CountdownSettings]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CountdownSettingsMonthes'
CREATE INDEX [IX_FK_CountdownSettingsMonthes]
ON [dbo].[Monthes]
    ([CountdownSettingsId]);
GO

-- Creating foreign key on [FilesId] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [FK_ImagesFiles]
    FOREIGN KEY ([FilesId])
    REFERENCES [dbo].[Files]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ImagesFiles'
CREATE INDEX [IX_FK_ImagesFiles]
ON [dbo].[Images]
    ([FilesId]);
GO

-- Creating foreign key on [TypeOfReminderId] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [FK_TypeOfReminderSettings]
    FOREIGN KEY ([TypeOfReminderId])
    REFERENCES [dbo].[TypeOfReminders]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TypeOfReminderSettings'
CREATE INDEX [IX_FK_TypeOfReminderSettings]
ON [dbo].[Settings]
    ([TypeOfReminderId]);
GO

-- Creating foreign key on [UserName] in table 'Reminders'
ALTER TABLE [dbo].[Reminders]
ADD CONSTRAINT [FK_ReminderUsers]
    FOREIGN KEY ([UserName])
    REFERENCES [dbo].[Users]
        ([Name])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReminderUsers'
CREATE INDEX [IX_FK_ReminderUsers]
ON [dbo].[Reminders]
    ([UserName]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------