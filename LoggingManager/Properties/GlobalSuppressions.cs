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

[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "LoggingManager.MySerialization.#SerializeToXML(System.Object)", MessageId = "XML", Justification = "XML is the abbreviation.")]
[module: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Scope = "member", Target = "LoggingManager.MySerialization.#SerializeToJSON(System.Object)", MessageId = "JSON", Justification = "JSON is the abbreviation.")]