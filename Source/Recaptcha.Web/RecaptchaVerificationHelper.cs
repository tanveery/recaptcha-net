/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Threading.Tasks;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the functionality for verifying reCAPTCHA client-side API response with request to the server-side reCAPTCHA API.
    /// <para/> Used for backward compatibility only.
    /// </summary>
    [Obsolete("Use Verifier class instead.")]
    public class RecaptchaVerificationHelper : Verifier
    {
        /// <summary>
        /// Creates an instance of the <see cref="RecaptchaVerificationHelper"/> class.
        /// </summary>
        /// <param name="privateKey">Sets the private key (Secret key) to be part of the reCAPTCHA verification request.</param>
        internal RecaptchaVerificationHelper(string privateKey)
            : base(privateKey) { }

        /// <summary>
        /// Determines if HTTPS intead of HTTP is to be used in reCAPTCHA API requests.
        /// </summary>
        [Obsolete("Current version of API does not allow to use HTTP.")]
        public bool UseSsl
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the user's host address used to make the reCAPTCHA verification request.
        /// </summary>
        [Obsolete("Current version of API does not use IP host address.")]
        public string UserHostAddress
        {
            // Return some non-empty string, otherwise backward compatibility might be broken.
            get { return Boolean.FalseString; }
        }

        /// <summary>
        /// Gets the user's response to the reCAPTCHA challenge, which should be passed to the verification request.
        /// </summary>
        [Obsolete("Current version of API does not provide raw user's response.")]
        public string Response
        {
            // Return some non-empty string, otherwise current callers logic, which tries to check the Response first, might be broken.
            get { return Boolean.FalseString; }
        }

        /// <summary>
        /// Verifies whether the CAPTCHA is solved correctly by the end user.
        /// </summary>
        /// <returns>Returns the result as a value of the <see cref="RecaptchaVerificationResult"/> enum.</returns>
        [Obsolete("Use VerifyIfSolvedAsync method instead.")]
        public RecaptchaVerificationResult VerifyRecaptchaResponse()
        {
            var result = Task.Factory.StartNew(async () => await GetErrorCodeAsync().ConfigureAwait(false)).Unwrap().Result;
            return ConvertErrorCode(result);
        }

        /// <summary>
        /// Verifies whether the CAPTCHA is solved correctly by the end user.
        /// </summary>
        /// <returns>Returns the result as a value of the <see cref="RecaptchaVerificationResult"/> enum.</returns>
        [Obsolete("Use VerifyIfSolvedAsync method instead.")]
        public async Task<RecaptchaVerificationResult> VerifyRecaptchaResponseTaskAsync()
        {
            var result = await GetErrorCodeAsync();
            return ConvertErrorCode(result);
        }

        private static RecaptchaVerificationResult ConvertErrorCode(ErrorCode source)
        {
            if (source == ErrorCode.NoError)
            {
                return RecaptchaVerificationResult.Success;
            }

            if (source.HasFlag(ErrorCode.MissingInputSecret)
                || source.HasFlag(ErrorCode.InvalidInputSecret))
            {
                return RecaptchaVerificationResult.InvalidPrivateKey;
            }

            if (source.HasFlag(ErrorCode.MissingInputResponse))
            {
                return RecaptchaVerificationResult.NullOrEmptyCaptchaSolution;
            }

            if (source.HasFlag(ErrorCode.InvalidInputResponse))
            {
                return RecaptchaVerificationResult.IncorrectCaptchaSolution;
            }

            return RecaptchaVerificationResult.UnknownError;
        }
    }
}