/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using Recaptcha.Web.Configuration;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Recaptcha.Web.UI.Controls
{
    /// <summary>
    /// An ASP.NET control that wraps Google's reCAPTCHA control.
    /// </summary>
    [DefaultProperty("SiteKey")]
    [ToolboxData("<{0}:Recaptcha runat=server></{0}:Recaptcha>")]
    public class Recaptcha : WebControl
    {
        #region Fields

        private RecaptchaVerificationHelper _verificationHelper = null;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the API version of the reCAPTCHA control.
        /// </summary>
        /// <remarks>The value of the <see cref="ApiVersion"/> property is optional. If the value is not set, version 2 is automatically assumed.</remarks>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("2")]
        [Localizable(false)]
        public string ApiVersion
        {
            get
            {
                if (ViewState["RecaptchaApiVersion"] == null)
                {
                    var config = RecaptchaConfigurationManager.GetConfiguration();
                    ViewState["RecaptchaApiVersion"] = config.ApiVersion;
                }

                return (String)ViewState["RecaptchaApiVersion"];
            }

            set
            {
                ViewState["RecaptchaApiVersion"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the public key of the recaptcha control.
        /// </summary>
        /// <remarks>The value of the <see cref="SiteKey"/> property is required. If the key is not set, a runtime exception will be thrown. The key can be set either directly as a literal value or as an appSettings key from the application configuration file. An appSettings key needs to be specified within {} curly braces.</remarks>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(false)]
        public string SiteKey
        {
            get
            {
                if (ViewState["RecaptchaSiteKey"] == null)
                {
                    var config = RecaptchaConfigurationManager.GetConfiguration();
                    ViewState["RecaptchaSiteKey"] = config.SiteKey;
                }

                return (String)ViewState["RecaptchaSiteKey"];
            }
            set
            {
                ViewState["RecaptchaSiteKey"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the private key of the recaptcha control.
        /// </summary>
        /// <remarks>The value of the <see cref="SecretKey"/> property is required. If the key is not set, a runtime exception will be thrown. The key can be set either directly as a literal value or as an appSettings key from the application configuration file. An appSettings key needs to be specified within {} curly braces.</remarks>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(false)]
        public string SecretKey
        {
            get
            {
                if (ViewState["RecaptchaSecretKey"] == null)
                {
                    var config = RecaptchaConfigurationManager.GetConfiguration();
                    ViewState["RecaptchaSecretKey"] = config.SecretKey;
                }

                return (String)ViewState["RecaptchaSecretKey"];
            }
            set
            {
                ViewState["RecaptchaSecretKey"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the theme of the recaptcha control.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(RecaptchaTheme.Default)]
        [Localizable(false)]
        public RecaptchaTheme Theme
        {
            get
            {
                if (ViewState["RecaptchaTheme"] == null)
                {
                    var config = RecaptchaConfigurationManager.GetConfiguration();
                    ViewState["RecaptchaTheme"] = config.DefaultTheme;
                }

                return (RecaptchaTheme)ViewState["RecaptchaTheme"];
            }

            set
            {
                ViewState["RecaptchaTheme"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the language of the recaptcha control.
        /// </summary>
        /// <remarks>If the property is not set then the language of the current UI culture will be used.</remarks>
        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(false)]
        public string Language
        {
            get
            {
                if (ViewState["RecaptchaLanguage"] == null)
                {
                    var config = RecaptchaConfigurationManager.GetConfiguration();
                    ViewState["RecaptchaLanguage"] = config.DefaultLanguage;
                }

                return (String)ViewState["RecaptchaLanguage"];
            }

            set
            {
                ViewState["RecaptchaLanguage"] = value;
            }
        }

        /// <summary>
        /// Determines whether to use SSL in reCAPTCHA URLs.
        /// </summary>
        /// <remarks>The default value is <see cref="SslBehavior.SameAsRequestUrl"/>.</remarks>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Localizable(false)]
        public SslBehavior UseSsl
        {
            get
            {
                if (ViewState["RecaptchaUseSsl"] == null)
                {
                    var config = RecaptchaConfigurationManager.GetConfiguration();
                    ViewState["RecaptchaUseSsl"] = config.UseSsl;
                }

                return (SslBehavior)ViewState["RecaptchaUseSsl"];
            }

            set
            {
                ViewState["RecaptchaUseSsl"] = value;
            }
        }

        /// <summary>
        /// Gets the user's response to the recaptcha challenge.
        /// </summary>
        [Bindable(true)]
        [Category("Data")]
        [Localizable(false)]
        public string Response
        {
            get
            {
                if (_verificationHelper != null)
                {
                    return _verificationHelper.Response;
                }

                return String.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the data size of the recaptcha control.
        /// </summary>
        /// <remarks>This property is only relevant for v2 API. It has no effect if you are using v1 API.</remarks>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(RecaptchaDataSize.Normal)]
        [Localizable(false)]
        public RecaptchaDataSize DataSize
        {
            get
            {
                if (ViewState["RecaptchaDataSize"] == null)
                {
                    var config = RecaptchaConfigurationManager.GetConfiguration();
                    ViewState["RecaptchaDataSize"] = config.DefaultSize;
                }

                return (RecaptchaDataSize)ViewState["RecaptchaDataSize"];
            }

            set
            {
                ViewState["RecaptchaDataSize"] = value;
            }
        }

        #endregion Properties

        #region Control Events

        /// <summary>
        /// Calls the OnLoad method of the parent class <see cref="System.Web.UI.WebControls.WebControl"/> and initializes the internal state of the <see cref="Recaptcha"/> control for verification of the user's response to the recaptcha challenge.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> object passed to the Load event of the control.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.Page.IsPostBack)
            {
                _verificationHelper = new RecaptchaVerificationHelper(this.SecretKey);
            }
        }

        /// <summary>
        /// Redners the HTML output. This method is automatically called by ASP.NET during the rendering process.
        /// </summary>
        /// <param name="output">The output object to which the method will write HTML to.</param>
        /// <exception cref="InvalidOperationException">The exception is thrown if the public key is not set.</exception>
        protected override void RenderContents(HtmlTextWriter output)
        {
            if (this.DesignMode)
            {
                output.Write("<p>Recaptcha Control</p>");
            }
            else
            {
                if (ApiVersion == null || ApiVersion == "2")
                {
                    var htmlHelper = new Recaptcha2HtmlHelper(this.SiteKey, this.Theme, this.Language, this.TabIndex, this.DataSize, this.UseSsl);
                    output.Write(htmlHelper.ToString());
                }
                else
                {
                    throw new InvalidOperationException("The API version is either invalid or not supported.");
                }
            }
        }

        #endregion Control Events

        #region Public Methods

        /// <summary>
        /// Verifies the user's answer to the recaptcha challenge.
        /// </summary>
        /// <returns>Returns the verification result as <see cref="RecaptchaVerificationResult"/> enum value.</returns>
        ///<exception cref="InvalidOperationException">The private key is null or empty.</exception>
        ///<exception cref="System.Net.WebException">The time-out period for the recaptcha verification request expired.</exception>
        public RecaptchaVerificationResult Verify()
        {
            if (_verificationHelper == null)
            {
                _verificationHelper = new RecaptchaVerificationHelper(this.SecretKey);
            }

            return _verificationHelper.VerifyRecaptchaResponse();
        }

        /// <summary>
        /// Verifies the user's answer to the recaptcha challenge.
        /// </summary>
        /// <returns>Returns the verification result as <see cref="RecaptchaVerificationResult"/> enum value.</returns>
        ///<exception cref="InvalidOperationException">The private key is null or empty.</exception>
        ///<exception cref="System.Net.WebException">The time-out period for the recaptcha verification request expired.</exception>
        public Task<RecaptchaVerificationResult> VerifyTaskAsync()
        {
            if (_verificationHelper == null)
            {
                _verificationHelper = new RecaptchaVerificationHelper(this.SecretKey);
            }

            return _verificationHelper.VerifyRecaptchaResponseTaskAsync();
        }

        #endregion Public Methods
    }
}
