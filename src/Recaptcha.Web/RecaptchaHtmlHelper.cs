/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the functionality to generate reCAPTCHA HTML.
    /// </summary>
    public class RecaptchaHtmlHelper
    {
        private const string RecaptchaEndpoint = "https://www.google.com/recaptcha/api";

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
            RenderNoscript = true;
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
        /// Gets a value that specifies whether Fallback parameter should be passed into rendered HTML.
        /// <para />If the Fallback parameter is passed, then alternative view of the reCAPTCHA widget is forced, see https://developers.google.com/recaptcha/docs/faq#no_checkbox for details.
        /// <para /> This might be useful, when default widget does not work on the end user's environment and reCAPTCHA API fails to determine that case.
        /// </summary>
        protected virtual bool IsFallback
        {
            get { return false; }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether &lt;noscript&gt; HTML should be included in the rendered HTML.
        /// <para /> This allows to support users that don't have JavaScript enabled.
        /// The HTML is rendered as defined at https://developers.google.com/recaptcha/docs/faq#no_js.
        /// <para /> The default is true.
        /// </summary>
        private bool RenderNoscript { get; set; }

        /// <summary>
        /// Renders the reCAPTCHA's HTML to the provided writer.
        /// </summary>
        internal void Render(HtmlTextWriter writer)
        {
            var uriBulder = new UriBuilder(RecaptchaEndpoint + ".js");
            var query = new NameValueCollection();
            if (String.IsNullOrEmpty(language) == false)
            {
                query.Add("hl", language);
            }

            if (IsFallback)
            {
                query.Add("fallback", Boolean.TrueString.ToLower());
            }

            if (query.Count > 0)
            {
                uriBulder.Query = String.Join("&", query.AllKeys.Select(k => k +'=' + HttpUtility.UrlEncode(query[k])));
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

            if (RenderNoscript)
            {
                uriBulder = new UriBuilder(RecaptchaEndpoint + "/fallback");
                uriBulder.Query = "k=" + publicKey;

                writer.RenderBeginTag(HtmlTextWriterTag.Noscript);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "302px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "352px");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "302px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "352px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "relative");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "302px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "352px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Src, uriBulder.Uri.ToString());
                writer.AddAttribute("frameborder", "0");
                writer.AddAttribute("scrolling", "no");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "302px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "352px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
                writer.RenderBeginTag(HtmlTextWriterTag.Iframe);
                writer.RenderEndTag();      // iframe
                writer.RenderEndTag();      // div

                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "250px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "80px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
                writer.AddStyleAttribute("bottom", "21px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Left, "25px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "0px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "0px");
                writer.AddStyleAttribute("right", "25px");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                const string TextAreaId = "g-recaptcha-response";
                writer.AddAttribute(HtmlTextWriterAttribute.Id, TextAreaId);
                writer.AddAttribute(HtmlTextWriterAttribute.Name, TextAreaId);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, TextAreaId);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "250px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "80px");
                writer.AddStyleAttribute("border", "1px solid #c1c1c1");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "0px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "0px");
                writer.AddStyleAttribute("resize", "none");
                writer.AddAttribute(HtmlTextWriterAttribute.Value, string.Empty);
                writer.RenderBeginTag(HtmlTextWriterTag.Textarea);
                writer.RenderEndTag();      // textarea
                writer.RenderEndTag();      // div
                writer.RenderEndTag();      // div
                writer.RenderEndTag();      // div
                writer.RenderEndTag();      // noscript
            }
        }
    }
}
