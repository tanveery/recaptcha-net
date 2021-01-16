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
    /// Represents the base recaptcha control.
    /// </summary>
    public abstract class RecaptchaControlBase : WebControl
    {
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
        /// Gets or sets the site key of the recaptcha control.
        /// </summary>
        /// <remarks>The value of the <see cref="SiteKey"/> property is required when API script is to be rendered. The key can be set either directly through this property or as an appSettings key (recaptcha:sitekey) in the application configuration file.</remarks>
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
        /// Gets or sets the language of the recaptcha control.
        /// </summary>
        /// <remarks>If the property is not set then the user's language is used. This property is used when the API script is rendered.</remarks>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
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
        /// <remarks>The default value is <see cref="SslBehavior.AlwaysUseSsl"/>.</remarks>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(SslBehavior.AlwaysUseSsl)]
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

        #endregion Properties
    }
}
