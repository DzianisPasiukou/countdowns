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

[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Mvc", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "namespace", Target = "CountdownMvc.App_Start", MessageId = "Mvc", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "CountdownMvc.MvcApplication", MessageId = "Mvc", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "namespace", Target = "CountdownMvc.Models.UserIdentity", MessageId = "Mvc", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "namespace", Target = "CountdownMvc.Controllers", MessageId = "Mvc", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "namespace", Target = "CountdownMvc.App_Start", Justification = "The underscores is needed.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.WeeksDto", MessageId = "Dto", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.DaysDto", MessageId = "Dto", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "type", Target = "Transfer.CountdownSettingsDto", MessageId = "Dto", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "CountdownMvc.Startup.#Configuration(Owin.IAppBuilder)", Justification = "The is auto generated.")]
[module: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "CountdownMvc.MvcApplication.#Application_Start()", Justification = "The is auto generated.")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "namespace", Target = "CountdownMvc.Models", MessageId = "Mvc", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", Scope = "member", Target = "CountdownMvc.Controllers.AccountController.#Login(CountdownMvc.Models.LoginViewModel,System.String)", MessageId = "Login", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", Scope = "member", Target = "CountdownMvc.Controllers.AccountController.#ExternalLogin(System.String,System.String)", MessageId = "Login", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", Scope = "member", Target = "CountdownMvc.Controllers.AccountController+ManageMessageId.#RemoveLoginSuccess", MessageId = "Login", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", Scope = "type", Target = "CountdownMvc.Models.LoginViewModel", MessageId = "Login", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", Scope = "member", Target = "CountdownMvc.Controllers.AccountController.#Login(System.String)", MessageId = "Login", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", Scope = "member", Target = "CountdownMvc.Controllers.AccountController.#ExternalLoginFailure()", MessageId = "Login", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", Scope = "member", Target = "CountdownMvc.Controllers.AccountController.#Disassociate(System.String,System.String)", MessageId = "login", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", Scope = "member", Target = "CountdownMvc.Controllers.AccountController.#LinkLogin(System.String)", MessageId = "Login", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", Scope = "member", Target = "CountdownMvc.Controllers.AccountController.#LinkLoginCallback()", MessageId = "Login", Justification = "The word is correct.")]
[module: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", Scope = "member", Target = "CountdownMvc.Controllers.ManageMessageId.#RemoveLoginSuccess", MessageId = "Login", Justification = "The word is correct.")]