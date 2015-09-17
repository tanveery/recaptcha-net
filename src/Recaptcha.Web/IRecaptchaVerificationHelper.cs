namespace Recaptcha.Web
{
    using System.Threading.Tasks;

    public interface IRecaptchaVerificationHelper
    {
        /// <summary>
        /// Determines if HTTPS intead of HTTP is to be used in Recaptcha verification API calls.
        /// </summary>
        bool UseSsl { get; }

        /// <summary>
        /// Gets the privae key of the recaptcha verification request.
        /// </summary>
        string PrivateKey { get; }

        /// <summary>
        /// Gets the user's host address of the recaptcha verification request.
        /// </summary>
        string UserHostAddress { get; }

        /// <summary>
        /// Gets the user's response to the recaptcha challenge of the recaptcha verification request.
        /// </summary>
        string Response { get; }

        /// <summary>
        /// Verifies whether the user's response to the recaptcha request is correct.
        /// </summary>
        /// <returns>Returns the result as a value of the <see cref="RecaptchaVerificationResult"/> enum.</returns>
        RecaptchaVerificationResult VerifyRecaptchaResponse();

        /// <summary>
        /// Verifies whether the user's response to the recaptcha request is correct.
        /// </summary>
        /// <returns>Returns the result as a value of the <see cref="RecaptchaVerificationResult"/> enum.</returns>
        Task<RecaptchaVerificationResult> VerifyRecaptchaResponseTaskAsync();
    }
}