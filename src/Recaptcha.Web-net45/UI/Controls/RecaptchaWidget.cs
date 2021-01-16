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
    /// An ASP.NET control that wraps Google's reCAPTCHA widget.
    /// </summary>
    [DefaultProperty("SiteKey")]
    [ToolboxData("<{0}:RecaptchaWidget runat=server></{0}:RecaptchaWidget>")]
    public class RecaptchaWidget : RecaptchaControlBase
    {
        #region Fields

        private RecaptchaVerificationHelper _verificationHelper = null;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the secret key of the recaptcha widget.
        /// </summary>
        /// <remarks>The value of the <see cref="SecretKey"/> property is required when recaptcha response is to be verified. The key can be set either directly through this property or as an appSettings key (recaptcha:secretkey) in the application configuration file.</remarks>
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
        /// Gets or sets the theme of the recaptcha widget.
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
                    ViewState["RecaptchaTheme"] = config.Theme;
                }

                return (RecaptchaTheme)ViewState["RecaptchaTheme"];
            }

            set
            {
                ViewState["RecaptchaTheme"] = value;
            }
        }

        /// <summary>
        /// Determines whether to render API script.
        /// </summary>
        /// <remarks>The default value is true.</remarks>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(true)]
        [Localizable(false)]
        public bool RenderApiScript
        {
            get
            {
                if (ViewState["RecaptchaRenderApiScript"] == null)
                {
                    ViewState["RecaptchaRenderApiScript"] = true;
                }

                return (bool)ViewState["RecaptchaRenderApiScript"];
            }

            set
            {
                ViewState["RecaptchaRenderApiScript"] = value;
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
        /// Gets or sets the size of the recaptcha control.
        /// </summary>
        /// <remarks>This property is only relevant for v2 API. It has no effect if you are using v1 API.</remarks>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(RecaptchaSize.Default)]
        [Localizable(false)]
        public RecaptchaSize Size
        {
            get
            {
                if (ViewState["RecaptchaSize"] == null)
                {
                    var config = RecaptchaConfigurationManager.GetConfiguration();
                    ViewState["RecaptchaSize"] = config.Size;
                }

                return (RecaptchaSize)ViewState["RecaptchaSize"];
            }

            set
            {
                ViewState["RecaptchaSize"] = value;
            }
        }

        #endregion Properties

        #region Control Events

        /// <summary>
        /// Calls the OnLoad method of the parent class <see cref="System.Web.UI.WebControls.WebControl"/> and initializes the internal state of the <see cref="RecaptchaWidget"/> control for verification of the user's response to the recaptcha challenge.
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
                    var htmlHelper = new Recaptcha2HtmlHelper(this.SiteKey);
                    output.Write(htmlHelper.CreateWidgetHtml(RenderApiScript, Theme, Language, TabIndex, Size, UseSsl));
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
