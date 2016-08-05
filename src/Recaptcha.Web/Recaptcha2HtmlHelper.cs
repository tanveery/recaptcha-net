/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Text;
using System.Web;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the functionality to generate HTML for Recaptcha API v2.0.
    /// </summary>
    public class Recaptcha2HtmlHelper : RecaptchaHtmlHelperBase
    {
        #region Constructors

        /// <summary>
        /// Creates an instance of the <see cref="Recaptcha2HtmlHelper"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key to be part of the recaptcha HTML.</param>
        public Recaptcha2HtmlHelper(string publicKey)
            : base(publicKey)
        {
            DataType = null;
            DataSize = null;
        }

        /// <summary>
        /// Creates an instance of the <see cref="Recaptcha2HtmlHelper"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key of the recaptcha HTML.</param>
        /// <param name="theme">Sets the theme of the recaptcha HTML.</param>
        /// <param name="language">Sets the language of the recaptcha HTML.</param>
        /// <param name="tabIndex">Sets the tab index of the recaptcha HTML.</param>   
        public Recaptcha2HtmlHelper(string publicKey, RecaptchaTheme theme, string language, int tabIndex)
            : base(publicKey, theme, language, tabIndex)
        {
            DataType = null;
            DataSize = null;
        }

        /// <summary>
        /// Creates an instance of the <see cref="Recaptcha2HtmlHelper"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key of the recaptcha HTML.</param>
        /// <param name="theme">Sets the theme of the recaptcha HTML.</param>
        /// <param name="language">Sets the language of the recaptcha HTML.</param>
        /// <param name="tabIndex">Sets the tab index of the recaptcha HTML.</param>    
        /// <param name="useSsl">Determines whether to use SSL in reCAPTCHA API URLs.</param>
        public Recaptcha2HtmlHelper(string publicKey, RecaptchaTheme theme, string language, int tabIndex, SslBehavior useSsl)
            : base(publicKey, theme, language, tabIndex, useSsl)
        {
            DataType = null;
            DataSize = null;
        }

        /// <summary>
        /// Creates an instance of the <see cref="Recaptcha2HtmlHelper"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key of the recaptcha HTML.</param>
        /// <param name="theme">Sets the theme of the recaptcha HTML.</param>
        /// <param name="language">Sets the language of the recaptcha HTML.</param>
        /// <param name="tabIndex">Sets the tab index of the recaptcha HTML.</param>    
        /// <param name="dataType">Sets the type of the recaptcha HTML.</param>
        /// <param name="dataSize">Sets the size for the recpatcha HTML.</param>
        /// <param name="useSsl">Determines whether to use SSL in reCAPTCHA API URLs.</param>
        public Recaptcha2HtmlHelper(string publicKey, RecaptchaTheme theme, string language, int tabIndex, RecaptchaDataType? dataType, RecaptchaDataSize? dataSize, SslBehavior useSsl)
            : base(publicKey, theme, language, tabIndex, useSsl)
        {
            DataType = dataType;
            DataSize = dataSize;
        }

        /// <summary>
        /// Creates an instance of the <see cref="Recaptcha2HtmlHelper"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key of the recaptcha HTML.</param>
        /// <param name="theme">Sets the theme of the recaptcha HTML.</param>
        /// <param name="language">Sets the language of the recaptcha HTML.</param>
        /// <param name="tabIndex">Sets the tab index of the recaptcha HTML.</param>    
        /// <param name="dataType">Sets the type of the recaptcha HTML.</param>
        /// <param name="dataSize">Sets the size for the recpatcha HTML.</param>
        /// <param name="useSsl">Determines whether to use SSL in reCAPTCHA API URLs.</param>
        /// <param name="withCallback">Determines if we need a callback for our reCaptcha</param>
        public Recaptcha2HtmlHelper(string publicKey, RecaptchaTheme theme, string language, int tabIndex, RecaptchaDataType? dataType, RecaptchaDataSize? dataSize, SslBehavior useSsl, bool withCallback, string callBack)
            : base(publicKey, theme, language, tabIndex, useSsl, withCallback)
        {
            DataType = dataType;
            DataSize = dataSize;
            CallbackName = callBack;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the size of the reCAPTCHA control.
        /// </summary>
        public RecaptchaDataSize? DataSize
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets they type of the reCAPTCHA control.
        /// </summary>
        public RecaptchaDataType? DataType
        {
            get;
            set;
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Gets the recaptcha's HTML that needs to be rendered in an HTML page.
        /// </summary>
        /// <returns>Returns the HTML as an instance of the <see cref="String"/> type.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string lang = "";

            var callbackString = WithCallback ? string.Format("?onload={0}&render=explicit", CallbackName) : "";

            if (!String.IsNullOrEmpty(Language))
            {
                var chardata = WithCallback ? '&' : '?';
                lang = string.Format("{0}hl={1}", chardata, Language);
            }

            bool doUseSsl = false;

            switch (this.UseSsl)
            {
                case SslBehavior.DoNotUseSsl:
                    break;
                case SslBehavior.AlwaysUseSsl:
                    doUseSsl = true;
                    break;
                case SslBehavior.SameAsRequestUrl:
                    doUseSsl = HttpContext.Current.Request.IsSecureConnection;
                    break;
            }

            var protocol = "https://";

            if (!doUseSsl)
            {
                protocol = "http://";
            }

            var reCaptchaId = string.Concat("recaptcha-", CallbackName);
            sb.Append(string.Format("<script src=\"{0}www.google.com/recaptcha/api.js{1}{2}\" async defer ></script>", protocol, callbackString, lang));
            sb.Append(string.Format("<div class=\"g-recaptcha\" data-sitekey=\"{0}\"  id=\"{1}\"",  PublicKey, reCaptchaId));
            if (Theme != RecaptchaTheme.Default)
            {
                var theme = "light";

                if (Theme == RecaptchaTheme.Dark)
                {
                    theme = "dark";
                }

                sb.Append(string.Format(" data-theme=\"{0}\"", theme));
            }

            if(TabIndex != 0)
            {
                sb.Append(string.Format(" data-tabindex=\"{0}\"", TabIndex));
            }

            if (DataSize != null)
            {
                string dataSize = null;

                switch(DataSize)
                {
                    case RecaptchaDataSize.Compact:
                        dataSize = "compact";
                        break;
                    default:
                        dataSize = "normal";
                        break;
                }

                sb.Append(string.Format(" data-size=\"{0}\"", dataSize));
            }

            if (DataType != null)
            {
                string dataType = null;

                switch (DataType)
                {
                    case RecaptchaDataType.Audio:
                        dataType = "audio";
                        break;
                    default:
                        dataType = "image";
                        break;
                }

                sb.Append(string.Format(" data-type=\"{0}\"", dataType));
            }

            sb.Append("></div>");
            if (this.WithCallback)
            {
                var reCapthcaRenderScript =
                    @"<script type='text/javascript'>
                    var siteKey =  'replaceMeWithAKey';
                    var reCaptchaCallback = function() {
                    var elements = document.getElementsByClassName('g-recaptcha');
                    for (var i = 0; i < elements.length; i++) {
                        var id = elements[i].getAttribute('id');
	                    if (document.getElementById(id).innerHTML === '') {
		                    grecaptcha.render(id, {
			                    'sitekey' : siteKey
		                    });
	                    }
                    }
                    };
                    </script>";
                reCapthcaRenderScript = reCapthcaRenderScript.Replace("replaceMeWithAKey", this.PublicKey);
                reCapthcaRenderScript = reCapthcaRenderScript.Replace("reCaptchaCallback", CallbackName);

                sb.Append(reCapthcaRenderScript);
            }
            return sb.ToString();
        }

        #endregion Public Methods
    }
}
