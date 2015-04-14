SET QUOTED_IDENTIFIER OFF;
GO
USE [Countdown];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

DELETE FROM Reminders
DELETE FROM Settings;
DELETE FROM TypeOfReminders;

INSERT INTO TypeOfReminders ([Id], [Name], [Description])
VALUES (1, 'Countdown', 'Countdown');

INSERT INTO TypeOfReminders ([Id], [Name], [Description])
VALUES (2, 'Progress', 'Progress');

INSERT INTO Settings ([Id], [Name], [Description], [TypeOfReminderId], [NameProperty], [NameElement])
VALUES (1, 'Font for name', 'Font for name', 1, 'font-family', 'reminderName');

INSERT INTO Settings ([Id], [Name], [Description], [TypeOfReminderId], [NameProperty], [NameElement])
VALUES (2, 'Font for name', 'Font for name', 2, 'font-family', 'reminderName');

INSERT INTO Settings ([Id], [Name], [Description], [TypeOfReminderId], [NameProperty], [NameElement])
VALUES (3, 'Color of progress interval', 'Color of progress interval', 2, 'background','progressInterval');

INSERT INTO Settings ([Id], [Name], [Description], [TypeOfReminderId], [NameProperty], [NameElement])
VALUES (4, 'Color of progress duration', 'Color of progress duration', 2, 'background','progressDuration');

INSERT INTO Settings ([Id], [Name], [Description], [TypeOfReminderId], [NameProperty], [NameElement])
VALUES (5, 'Font for countdown', 'Font for countdown', 1, 'font-family','countdownValue');

INSERT INTO Settings ([Id], [Name], [Description], [TypeOfReminderId], [NameProperty], [NameElement])
VALUES (6, 'Color for countdown', 'Color for countdown', 1, 'color','countdownValue');

INSERT INTO Settings ([Id], [Name], [Description], [TypeOfReminderId], [NameProperty], [NameElement])
VALUES (7, 'Background color for countdown', 'Background color for countdown', 1, 'background','countdownValue');

INSERT INTO Settings ([Id], [Name], [Description], [TypeOfReminderId], [NameProperty], [NameElement])
VALUES (8, 'Tile color', 'Tile color', 1, 'background','tile');

INSERT INTO Settings ([Id], [Name], [Description], [TypeOfReminderId], [NameProperty], [NameElement])
VALUES (9, 'Tile color', 'Tile color', 2, 'background','tile');