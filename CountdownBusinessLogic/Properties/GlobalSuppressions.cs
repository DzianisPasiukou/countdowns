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

[module: SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Scope = "type", Target = "CountdownBusinessLogic.ICountdownCollection", Justification = "The is the collection.")]
[module: SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Scope = "type", Target = "CountdownBusinessLogic.CountdownCollection", Justification = "The is the collection.")]
[module: SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Scope = "type", Target = "CountdownBusinessLogic.Settings.SettingsCollection", Justification = "The is the collection.")]
[module: SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Scope = "member", Target = "CountdownBusinessLogic.Security.IHashCalculator.#Calculate(System.String,System.Byte[])", Justification = "I'm needed to use default parameters.")]
[module: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", Scope = "member", Target = "CountdownBusinessLogic.CountdownServiceReference.GetDataCompletedEventArgs.#.ctor(System.Object[],System.Exception,System.Boolean,System.Object)", MessageId = "cancelled", Justification = "The is auto generated WCF proxy.")]
[module: SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "CountdownBusinessLogic.CountdownServiceReference.GetDataCompletedEventArgs.#Result", Justification = "The is auto generated.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "CountdownBusinessLogic.Security.Sha256HashCalculator", MessageId = "Sha", Justification = "This word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Scope = "type", Target = "CountdownBusinessLogic.SettingsInfo.SettingsCollection", Justification = "This word is correct.")]