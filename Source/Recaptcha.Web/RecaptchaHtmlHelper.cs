/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the functionality to generate reCAPTCHA HTML.
    /// </summary>
    public class RecaptchaHtmlHelper
    {
        /// <summary>
        /// List of supported language codes as defined at https://developers.google.com/recaptcha/docs/language.
        /// </summary>
        private static readonly HashSet<string> SupportedLanguages =
            new HashSet<string>
            {
                "ar",
                "bg",
                "ca",
                "zh-CN",
                "zh-TW",
                "hr",
                "cs",
                "da",
                "nl",
                "en-GB",
                "en",
                "fil",
                "fi",
                "fr",
                "fr-CA",
                "De",
                "de-AT",
                "de-CH",
                "el",
                "iw",
                "hi",
                "hu",
                "id",
                "it",
                "ja",
                "ko",
                "lv",
                "lt",
                "no",
                "fa",
                "pl",
                "pt",
                "pt-BR",
                "pt-PT",
                "ro",
                "ru",
                "sr",
                "sk",
                "sl",
                "es",
                "es-419",
                "sv",
                "th",
                "tr",
                "uk",
                "vi"
            };

        private readonly string publicKey;
        private ColorTheme? colorTheme;
        private string language;
#pragma warning disable 618
        private RecaptchaTheme theme;
#pragma warning restore 618

        /// <summary>
        /// Creates an instance of the <see cref="RecaptchaHtmlHelper"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key (Site key) used to render the reCAPTCHA HTML.</param>
        public RecaptchaHtmlHelper(string publicKey)
        {
            if (String.IsNullOrEmpty(publicKey))
            {
                throw new ArgumentNullException("publicKey", "Public key cannot be null or empty.");
            }

            this.publicKey = KeyHelper.LoadKey(publicKey);
        }

        /// <summary>
        /// Creates an instance of the <see cref="RecaptchaHtmlHelper"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key (Site key) used to render the reCAPTCHA HTML.</param>
        /// <param name="theme">Sets the theme used to render the reCAPTCHA HTML.</param>
        /// <param name="language">Sets the language used to render the reCAPTCHA HTML.</param>
        /// <param name="tabIndex">Sets the tab index used to render the reCAPTCHA HTML.</param>
        [Obsolete("This constructor sets some obsolete properties. Use a constructor with single parameters and set other properties manually.")]
        public RecaptchaHtmlHelper(string publicKey, RecaptchaTheme theme, string language, int tabIndex)
            : this(publicKey)
        {
            this.Language = language;
#pragma warning disable 618
            this.TabIndex = tabIndex;
            this.Theme = theme;
#pragma warning restore 618
        }

        /// <summary>
        /// Gets the public key (Site key) used to render the reCAPTCHA HTML.
        /// </summary>
        public string PublicKey
        {
            get { return publicKey; }
        }

        /// <summary>
        /// Determines if HTTPS intead of HTTP is to be used in reCAPTCHA API calls.
        /// </summary>
        [Obsolete("Current version of API does not allow to use HTTP.")]
        public bool UseSsl
        {
            get { return true; }
        }

        /// <summary>
        /// Gets or sets the theme used to render the reCAPTCHA HTML.
        /// </summary>
        [Obsolete("Use ColorTheme property to set a color theme, which is supported by the current version of API.")]
        public RecaptchaTheme Theme
        {
            get { return theme; }
            set
            {
                if (theme != value || colorTheme.HasValue == false)
                {
                    theme = value;
                    switch (value)
                    {
#pragma warning disable 618
                        case RecaptchaTheme.Red:
                        case RecaptchaTheme.Blackglass:
#pragma warning restore 618
                            colorTheme = ColorTheme.Dark;
                            break;
                        default:
                            colorTheme = ColorTheme.Light;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the color theme used to render the reCAPTCHA HTML.
        /// </summary>
        public ColorTheme ColorTheme
        {
            get { return colorTheme.GetValueOrDefault(ColorTheme.Light); }
            set { colorTheme = value; }
        }

        /// <summary>
        /// Gets or sets the language used to render the reCAPTCHA HTML.
        /// </summary>
        public string Language
        {
            get { return language; }
            set
            {
                if (language != value
                    && String.IsNullOrWhiteSpace(value) == false
                    && SupportedLanguages.Contains(value))
                {
                    language = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the tab index used to render the reCAPTCHA HTML.
        /// </summary>
        [Obsolete("Current version of API does not allow to set tab index.")]
        public int TabIndex { get; set; }

        /// <summary>
        /// Renders the reCAPTCHA's HTML to the provided writer.
        /// </summary>
        internal void Render(HtmlTextWriter writer)
        {
            var uriBulder = new UriBuilder("https://www.google.com/recaptcha/api.js");
            if (String.IsNullOrEmpty(language) == false)
            {
                uriBulder.Query = "hl=" + language;
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Src, uriBulder.Uri.ToString());
            writer.AddAttribute("async", null);
            writer.AddAttribute("defer", null);
            writer.RenderBeginTag(HtmlTextWriterTag.Script);
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "g-recaptcha");
            writer.AddAttribute("data-sitekey", publicKey);

            if (colorTheme.HasValue)
            {
                writer.AddAttribute("data-theme", colorTheme.Value.ToString().ToLower());
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();
        }
    }
}
