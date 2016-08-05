﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the base functionality for a reCAPTCHA HTML helper class.
    /// </summary>
    public abstract class RecaptchaHtmlHelperBase : IRecaptchaHtmlHelper
    {
        #region Constructors

        /// <summary>
        /// Creates an instance of the <see cref="RecaptchaHtmlHelperBase"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key to be part of the recaptcha HTML.</param>
        public RecaptchaHtmlHelperBase(string publicKey)
        {
            if (String.IsNullOrEmpty(publicKey))
            {
                throw new InvalidOperationException("Public key cannot be null or empty.");
            }

            this.PublicKey = RecaptchaKeyHelper.ParseKey(publicKey);
        }

        /// <summary>
        /// Creates an instance of the <see cref="RecaptchaHtmlHelperBase"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key of the recaptcha HTML.</param>
        /// <param name="theme">Sets the theme of the recaptcha HTML.</param>
        /// <param name="language">Sets the language of the recaptcha HTML.</param>
        /// <param name="tabIndex">Sets the tab index of the recaptcha HTML.</param>    
        public RecaptchaHtmlHelperBase(string publicKey, RecaptchaTheme theme, string language, int tabIndex)
        {
            this.PublicKey = RecaptchaKeyHelper.ParseKey(publicKey);

            if (String.IsNullOrEmpty(this.PublicKey))
            {
                throw new InvalidOperationException("Public key cannot be null or empty.");
            }

            this.Theme = theme;
            this.Language = language;
            this.TabIndex = tabIndex;
        }

        /// <summary>
        /// Creates an instance of the <see cref="RecaptchaHtmlHelperBase"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key of the recaptcha HTML.</param>
        /// <param name="theme">Sets the theme of the recaptcha HTML.</param>
        /// <param name="language">Sets the language of the recaptcha HTML.</param>
        /// <param name="tabIndex">Sets the tab index of the recaptcha HTML.</param>    
        /// <param name="useSsl">Determines whether to use SSL in reCAPTCHA API URLs.</param>
        public RecaptchaHtmlHelperBase(string publicKey, RecaptchaTheme theme, string language, int tabIndex, SslBehavior useSsl)
        {
            this.PublicKey = RecaptchaKeyHelper.ParseKey(publicKey);

            if (String.IsNullOrEmpty(this.PublicKey))
            {
                throw new InvalidOperationException("Public key cannot be null or empty.");
            }

            this.Theme = theme;
            this.Language = language;
            this.TabIndex = tabIndex;

            UseSsl = useSsl;
        }

        /// <summary>
        /// Creates an instance of the <see cref="RecaptchaHtmlHelperBase"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key of the recaptcha HTML.</param>
        /// <param name="theme">Sets the theme of the recaptcha HTML.</param>
        /// <param name="language">Sets the language of the recaptcha HTML.</param>
        /// <param name="tabIndex">Sets the tab index of the recaptcha HTML.</param>    
        /// <param name="useSsl">Determines whether to use SSL in reCAPTCHA API URLs.</param>
        /// <param name="withCallBack">Determines if we need a callback for our reCaptch</param>
        public RecaptchaHtmlHelperBase(string publicKey, RecaptchaTheme theme, string language, int tabIndex, SslBehavior useSsl, bool withCallBack)
        {
            this.PublicKey = RecaptchaKeyHelper.ParseKey(publicKey);

            if (String.IsNullOrEmpty(this.PublicKey))
            {
                throw new InvalidOperationException("Public key cannot be null or empty.");
            }

            this.Theme = theme;
            this.Language = language;
            this.TabIndex = tabIndex;

            UseSsl = useSsl;
            WithCallback = withCallBack;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the ID of the HTML tag that represents recaptcha.
        /// </summary>
        public string RecaptchaHtmlId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the public key of the recaptcha HTML.
        /// </summary>
        public string PublicKey
        {
            get;
            set;
        }

        /// <summary>
        /// Determines if HTTPS intead of HTTP is to be used in reCAPTCHA API calls.
        /// </summary>
        public SslBehavior UseSsl
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the theme of the reCAPTCHA HTML.
        /// </summary>
        public RecaptchaTheme Theme
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the language of the recaptcha HTML.
        /// </summary>
        public string Language
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tab index of the recaptcha HTML.
        /// </summary>
        public int TabIndex
        {
            get;
            set;
        }

        public bool WithCallback { get; set; }

        public string CallbackName { get; set; }

        #endregion Properties
    }
}
