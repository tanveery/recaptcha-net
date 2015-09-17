namespace Recaptcha.Web
{
    public interface IRecaptchaHtmlHelper
    {
        /// <summary>
        /// Gets the public key of the recaptcha HTML.
        /// </summary>
        string PublicKey { get; }

        /// <summary>
        /// Determines if HTTPS intead of HTTP is to be used in reCAPTCHA API calls.
        /// </summary>
        SslBehavior UseSsl { get; }

        /// <summary>
        /// Gets or sets the theme of the reCAPTCHA HTML.
        /// </summary>
        RecaptchaTheme Theme { get; set; }

        /// <summary>
        /// Gets or sets the language of the recaptcha HTML.
        /// </summary>
        string Language { get; set; }

        /// <summary>
        /// Gets or sets the tab index of the recaptcha HTML.
        /// </summary>
        int TabIndex { get; set; }

        /// <summary>
        /// Gets the recaptcha's HTML that needs to be rendered in an HTML page.
        /// </summary>
        /// <returns>Returns the HTML as an instance of the <see cref="string"/> type.</returns>
        string ToString();
    }
}