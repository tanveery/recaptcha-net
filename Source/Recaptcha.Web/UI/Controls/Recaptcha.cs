/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

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
    [DefaultProperty("PublicKey")]
    [ToolboxData("<{0}:Recaptcha runat=server></{0}:Recaptcha>")]
    public class Recaptcha : WebControl
    {
#pragma warning disable 618
        private readonly Lazy<RecaptchaVerificationHelper> verificationHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="Recaptcha"/> class.
        /// </summary>
        public Recaptcha()
        {
            verificationHelper = new Lazy<RecaptchaVerificationHelper>(CreateVerificationHelper);
        }
#pragma warning restore 618

        /// <summary>
        /// Gets or sets the public key of the reCAPTCHA control.
        /// </summary>
        /// <remarks>The value of the <see cref="PublicKey"/> property is required. If the key is not set, a runtime exception will be thrown. The key can be set either directly as a literal value or as an appSettings key from the application configuration file. An appSettings key needs to be specified within {} curly braces.</remarks>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("{recaptchaPublicKey}")]
        [Localizable(false)]
        public string PublicKey
        {
            get
            {
                String s = (String)ViewState["PublicKey"];
                return ((s == null) ? "{recaptchaPublicKey}" : s);
            }

            set
            {
                ViewState["PublicKey"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the private key of the reCAPTCHA control.
        /// </summary>
        /// <remarks>The value of the <see cref="PrivateKey"/> property is required. If the key is not set, a runtime exception will be thrown. The key can be set either directly as a literal value or as an appSettings key from the application configuration file. An appSettings key needs to be specified within {} curly braces.</remarks>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("{recaptchaPrivateKey}")]
        [Localizable(false)]
        public string PrivateKey
        {
            get
            {
                String s = (String)ViewState["PrivateKey"];
                return ((s == null) ? "{recaptchaPrivateKey}" : s);
            }
            set
            {
                ViewState["PrivateKey"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the theme of the reCAPTCHA control.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
#pragma warning disable 618
        [DefaultValue(RecaptchaTheme.Red)]
#pragma warning restore 618
        [Localizable(false)]
        [Obsolete("Use ColorTheme property to set a color theme, which is supported by the current version of API.")]
        public RecaptchaTheme Theme
        {
            get
            {
                object t = ViewState["RecaptchaTheme"];
#pragma warning disable 618
                return ((t == null) ? RecaptchaTheme.Red : (RecaptchaTheme)t);
#pragma warning restore 618
            }

            set
            {
                ViewState["RecaptchaTheme"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the color theme of the reCAPTCHA control.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(ColorTheme.Light)]
        [Localizable(false)]
        public ColorTheme ColorTheme
        {
            get
            {
                var value = ViewState["RecaptchaColorTheme"];
                return value is ColorTheme ? (ColorTheme)value : ColorTheme.Light;
            }
            set { ViewState["RecaptchaTheme"] = value; }
        }

        /// <summary>
        /// Gets or sets the language of the reCAPTCHA control.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(false)]
        public string Language
        {
            get
            {
                return ViewState["RecaptchaLanguage"] as string;
            }

            set
            {
                ViewState["RecaptchaLanguage"] = value;
            }
        }

        /// <summary>
        /// Gets the user's response to the reCAPTCHA challenge.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(false)]
        [Obsolete("Current version of API does not provide raw user's response.")]
        public string Response
        {
            // Return some non-empty string, otherwise current callers logic, which tries to check the Response first, might be broken.
            get { return Boolean.FalseString; }
        }

        /// <summary>
        /// Renders the reCAPTCHA control HTML. This method is automatically called by ASP.NET during the rendering process.
        /// </summary>
        /// <param name="output">The output writer, the HTML will writen to.</param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            if (this.DesignMode)
            {
                output.RenderBeginTag(HtmlTextWriterTag.P);
                output.Write("Recaptcha Control");
                output.RenderEndTag();
            }
            else
            {
                var htmlHelper = new RecaptchaHtmlHelper(this.PublicKey)
                {
                    ColorTheme = ColorTheme,
                    Language = Language
                };
                htmlHelper.Render(output);
            }
        }

        /// <summary>
        /// Verifies if the CAPTCHA is solved correctly by the end user.
        /// </summary>
        /// <returns>True if the CAPTCHA is solved correctly and no error was found, otherwise returns false.</returns>
        public Task<bool> VerifyIfSolvedAsync()
        {
            return verificationHelper.Value.VerifyIfSolvedAsync();
        }

        /// <summary>
        /// Verifies whether the CAPTCHA is solved correctly by the end user.
        /// </summary>
        /// <returns>Returns the result as a value of the <see cref="RecaptchaVerificationResult"/> enum.</returns>
        [Obsolete("Use VerifyIfSolvedAsync method instead.")]
        public RecaptchaVerificationResult Verify()
        {
#pragma warning disable 618
            return verificationHelper.Value.VerifyRecaptchaResponse();
#pragma warning restore 618
        }

        /// <summary>
        /// Verifies whether the CAPTCHA is solved correctly by the end user.
        /// </summary>
        /// <returns>Returns the result as a value of the <see cref="RecaptchaVerificationResult"/> enum.</returns>
        [Obsolete("Use VerifyIfSolvedAsync method instead.")]
        public Task<RecaptchaVerificationResult> VerifyTaskAsync()
        {
#pragma warning disable 618
            return verificationHelper.Value.VerifyRecaptchaResponseTaskAsync();
#pragma warning restore 618
        }

#pragma warning disable 618
        private RecaptchaVerificationHelper CreateVerificationHelper()
        {
            return new RecaptchaVerificationHelper(this.PrivateKey);
        }
#pragma warning restore 618
    }
}
