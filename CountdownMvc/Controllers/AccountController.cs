namespace CountdownMvc.Controllers
{
	using System.Threading.Tasks;
	using System.Web;
	using System.Web.Mvc;

	using CountdownMvc.Models;
	using CountdownMvc.Models.UserIdentity;

	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.Owin;
	using Microsoft.Owin.Security;

	#region Public Enumerations

	/// <summary>
	/// The enumeration of messages to manage account.
	/// </summary>
	public enum ManageMessageId
	{
		/// <summary>
		/// The change password success.
		/// </summary>
		ChangePasswordSuccess,

		/// <summary>
		/// The set password success.
		/// </summary>
		SetPasswordSuccess,

		/// <summary>
		/// The remove login success.
		/// </summary>
		RemoveLoginSuccess,

		/// <summary>
		/// The error.
		/// </summary>
		Error
	}

	#endregion

	/// <summary>
	/// The instance of account controller.
	/// </summary>
	public class AccountController : Controller
	{
		#region Private Fields

		/// <summary>
		/// The XSRF key.
		/// </summary>
		private const string XsrfKey = "XsrfId";

		/// <summary>
		/// The custom user manager.
		/// </summary>
		private static CustomUserManager customUserManager;

		#endregion

		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AccountController"/> class.
		/// </summary>
		public AccountController()
		{
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the user manager.
		/// </summary>
		/// <value>
		/// The user manager.
		/// </value>
		public static CustomUserManager UserManager
		{
			get
			{
				return customUserManager ??
					  (customUserManager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<CustomUserManager>());
			}

			private set
			{
				customUserManager = value;
			}
		}

		#endregion

		#region Private Properties

		/// <summary>
		/// Gets the authentication manager.
		/// </summary>
		/// <value>
		/// The authentication manager.
		/// </value>
		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Authenticates the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="password">The password.</param>
		/// <returns>The task of boolean value.</returns>
		public async Task<bool> Authenticate(string name, string password)
		{
			if (await UserManager.FindAsync(name, password) != null)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Logins the specified return URL.
		/// </summary>
		/// <param name="returnUrl">The return URL.</param>
		/// <returns>The Login view.</returns>
		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return this.View();
		}

		/// <summary>
		/// Logins the specified model.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="returnUrl">The return URL.</param>
		/// <returns>The task of action result.</returns>
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				var user = await UserManager.FindAsync(model.UserName, model.Password);
				if (user != null)
				{
					await this.SignInAsync(user, model.RememberMe);
					return this.RedirectToLocal(returnUrl);
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Invalid username or password.");
				}
			}

			// If we got this far, something failed, redisplay form
			return this.View(model);
		}

		/// <summary>
		/// Registers this instance.
		/// </summary>
		/// <returns>The view of register.</returns>
		[AllowAnonymous]
		public ActionResult Register()
		{
			return this.View();
		}

		/// <summary>
		/// Registers the specified model.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns>The task of action result.</returns>
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (!UserManager.IsExist(model.UserName))
				{
					var user = new ApplicationUser()
					{
						UserName = model.UserName,
						Password = model.Password,
						Email = model.Email
					};

					await UserManager.CreateAsync(user);

					await this.SignInAsync(user, isPersistent: false);

					return this.RedirectToAction("Index", "Countdown");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "The user name is exist.");
				}
			}

			// If we got this far, something failed, redisplay form
			return this.View(model);
		}

		/// <summary>
		/// Disassociates the specified login provider.
		/// </summary>
		/// <param name="loginProvider">The login provider.</param>
		/// <param name="providerKey">The provider key.</param>
		/// <returns>The task of action result.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
		{
			ManageMessageId? message = null;
			IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));

			if (result.Succeeded)
			{
				message = ManageMessageId.RemoveLoginSuccess;
			}
			else
			{
				message = ManageMessageId.Error;
			}

			return this.RedirectToAction("Manage", new { Message = message });
		}

		/// <summary>
		/// Manages the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>The view of manage.</returns>
		public ActionResult Manage(ManageMessageId? message)
		{
			ViewBag.StatusMessage =
				message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
				: message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
				: message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
				: message == ManageMessageId.Error ? "An error has occurred."
				: string.Empty;
			ViewBag.HasLocalPassword = this.HasPassword();
			ViewBag.ReturnUrl = Url.Action("Manage");
			return this.View();
		}

		/// <summary>
		/// Manages the specified model.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns>The task of action result.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Manage(ManageUserViewModel model)
		{
			bool hasPassword = this.HasPassword();
			ViewBag.HasLocalPassword = hasPassword;
			ViewBag.ReturnUrl = Url.Action("Manage");
			if (hasPassword)
			{
				if (ModelState.IsValid)
				{
					var user = await UserManager.FindAsync(User.Identity.GetUserName(), model.OldPassword);

					if (user != null)
					{
						user.Password = model.NewPassword;

						await UserManager.UpdateAsync(user);

						return this.RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
					}
					else
					{
						this.AddErrors(new IdentityResult("Old password is wrong."));
					}
				}
			}
			else
			{
				// User does not have a password so remove any validation errors caused by a missing OldPassword field
				ModelState state = ModelState["OldPassword"];
				if (state != null)
				{
					state.Errors.Clear();
				}

				if (ModelState.IsValid)
				{
					IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
					if (result.Succeeded)
					{
						return this.RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
					}
					else
					{
						this.AddErrors(result);
					}
				}
			}

			// If we got this far, something failed, redisplay form
			return this.View(model);
		}

		/// <summary>
		/// Externals the login.
		/// </summary>
		/// <param name="provider">The provider.</param>
		/// <param name="returnUrl">The return URL.</param>
		/// <returns>The view of external login.</returns>
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult ExternalLogin(string provider, string returnUrl)
		{
			// Request a redirect to the external login provider
			return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
		}

		/// <summary>
		/// Links the login.
		/// </summary>
		/// <param name="provider">The provider.</param>
		/// <returns>The view of link login.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LinkLogin(string provider)
		{
			// Request a redirect to the external login provider to link a login for the current user
			return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
		}

		/// <summary>
		/// Links the login callback.
		/// </summary>
		/// <returns>The task of action result.</returns>
		public async Task<ActionResult> LinkLoginCallback()
		{
			var loginInfo = await this.AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());

			if (loginInfo == null)
			{
				return this.RedirectToAction("Manage", new { Message = ManageMessageId.Error });
			}

			var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);

			if (result.Succeeded)
			{
				return this.RedirectToAction("Manage");
			}

			return this.RedirectToAction("Manage", new { Message = ManageMessageId.Error });
		}

		/// <summary>
		/// Logs the off.
		/// </summary>
		/// <returns>The redirect to action index of home controller.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			this.AuthenticationManager.SignOut();
			return this.RedirectToAction("Index", "Home");
		}

		/// <summary>
		/// Externals the login failure.
		/// </summary>
		/// <returns>The view of external login failure.</returns>
		[AllowAnonymous]
		public ActionResult ExternalLoginFailure()
		{
			return this.View();
		}

		/// <summary>
		/// Removes the account list.
		/// </summary>
		/// <returns>The view of remove account list.</returns>
		[ChildActionOnly]
		public ActionResult RemoveAccountList()
		{
			var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
			ViewBag.ShowRemoveButton = this.HasPassword() || linkedAccounts.Count > 1;
			return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Releases unmanaged resources and optionally releases managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && UserManager != null)
			{
				UserManager.Dispose();
				UserManager = null;
			}

			base.Dispose(disposing);
		}

		#endregion

		#region Helpers

		#region Private Methods

		/// <summary>
		/// Signs the in asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="isPersistent">if set to <c>true</c> is persistent.</param>
		/// <returns>The task of creation identity.</returns>
		private async Task SignInAsync(ApplicationUser user, bool isPersistent)
		{
			this.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
			var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
			this.AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
		}

		/// <summary>
		/// Adds the errors.
		/// </summary>
		/// <param name="result">The result.</param>
		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error);
			}
		}

		/// <summary>
		/// Determines whether this instance has password.
		/// </summary>
		/// <returns>Has it password.</returns>
		private bool HasPassword()
		{
			var user = UserManager.FindByName(User.Identity.GetUserName());

			if (user != null)
			{
				return user.Password != null;
			}

			return false;
		}

		/// <summary>
		/// Redirects to local.
		/// </summary>
		/// <param name="returnUrl">The return URL.</param>
		/// <returns>The action to redirect to return url.</returns>
		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return this.Redirect(returnUrl);
			}
			else
			{
				return this.RedirectToAction("Index", "Home");
			}
		}

		#endregion

		/// <summary>
		/// The instance of result of challenges.
		/// </summary>
		private class ChallengeResult : HttpUnauthorizedResult
		{
			#region Public Constructors

			/// <summary>
			/// Initializes a new instance of the <see cref="ChallengeResult" /> class.
			/// </summary>
			/// <param name="provider">The provider.</param>
			/// <param name="redirectUri">The redirect URI.</param>
			public ChallengeResult(string provider, string redirectUri)
				: this(provider, redirectUri, null)
			{
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="ChallengeResult" /> class.
			/// </summary>
			/// <param name="provider">The provider.</param>
			/// <param name="redirectUri">The redirect URI.</param>
			/// <param name="userId">The user identifier.</param>
			public ChallengeResult(string provider, string redirectUri, string userId)
			{
				this.LoginProvider = provider;
				this.RedirectUri = redirectUri;
				this.UserId = userId;
			}

			#endregion

			#region Public Properties

			/// <summary>
			/// Gets or sets the login provider.
			/// </summary>
			/// <value>
			/// The login provider.
			/// </value>
			public string LoginProvider { get; set; }

			/// <summary>
			/// Gets or sets the redirect URI.
			/// </summary>
			/// <value>
			/// The redirect URI.
			/// </value>
			public string RedirectUri { get; set; }

			/// <summary>
			/// Gets or sets the user identifier.
			/// </summary>
			/// <value>
			/// The user identifier.
			/// </value>
			public string UserId { get; set; }

			#endregion

			#region Public Methods

			/// <summary>
			/// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult" /> class.
			/// </summary>
			/// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
			public override void ExecuteResult(ControllerContext context)
			{
				var properties = new AuthenticationProperties() { RedirectUri = this.RedirectUri };

				if (this.UserId != null)
				{
					properties.Dictionary[AccountController.XsrfKey] = this.UserId;
				}

				context.HttpContext.GetOwinContext().Authentication.Challenge(properties, this.LoginProvider);
			}

			#endregion
		}
		#endregion
	}
}