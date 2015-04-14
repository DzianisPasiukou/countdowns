//// -----------------------------------------------------------------------
//// <copyright file="GlobalSuppressions.cs" company="One Call Medical, Inc.">
//// Copyright (c) One Call Medical, Inc. All rights reserved.
//// </copyright>
//// -----------------------------------------------------------------------

//// This file is used by Code Analysis to maintain SuppressMessage 
//// attributes that are applied to this project.
//// Project-level suppressions either have no target or are given 
//// a specific target and scoped to a namespace, type, member, etc.
////
//// To add a suppression to this file, right-click the message in the 
//// Error List, point to "Suppress Message(s)", and click 
//// "In Project Suppression File".
//// You do not need to add suppressions to this file manually.

#region Usings

using System.Diagnostics.CodeAnalysis;

#endregion
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.SmallTransfer.ReminderPartDto", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Scope = "member", Target = "Transfer.ImageDto.#Type", Justification = "Type property can not be modified.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.SettingsDto", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.FilesDto", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.ImageDto", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.SettingsDto", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.FilesDto", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.WeeksDto", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.DaysDto", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.CountdownSettingsDto", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.ReminderDto", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.ReminderSettingsDto", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.ProgressSettingsDto", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.MonthsDto", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.ExercisesDto", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.TypeOfReminderDto", MessageId = "Dto", Justification = "Word 'Dto' is correct.")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Transfer.ReminderDto.#ReminderSettings", Justification = "The transfer objects must contain getters and setters.")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Transfer.ReminderDto.#Exercises", Justification = "The transfer objects must contain getters and setters.")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Transfer.CountdownSettingsDto.#Days", Justification = "The transfer objects must contain getters and setters.")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Transfer.CountdownSettingsDto.#Months", Justification = "The transfer objects must contain getters and setters.")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Transfer.CountdownSettingsDto.#Weeks", Justification = "The transfer objects must contain getters and setters.")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Transfer.FilesDto.#Images", Justification = "The transfer objects must contain getters and setters.")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Transfer.ExercisesDto.#Files", Justification = "The transfer objects must contain getters and setters.")]