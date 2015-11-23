/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the result value of recaptcha verification process.
    /// </summary>
    public enum RecaptchaVerificationResult
    {
        #region Common values for v1 and v2

        /// <summary>
        /// Verification failed but the exact reason is not known.
        /// </summary>
        UnknownError = 0,
        /// <summary>
        /// Verification succeeded with no errors.
        /// </summary>
        Success = 1,
        /// <summary>
        /// The user's response to recaptcha challenge is incorrect.
        /// </summary>
        IncorrectCaptchaSolution = 2,
        /// <summary>
        /// The private supplied at the time of verification process is invalid. Private key is also known as secret key in reCAPTCHA v2.
        /// </summary>
        InvalidPrivateKey = 4,
        /// <summary>
        /// The user's response to the recaptcha challenge is null or empty.
        /// </summary>
        NullOrEmptyCaptchaSolution = 5,
        /// <summary>
        /// The private key is missing. Private key is also known as secret key in reCAPTCHA v2.
        /// </summary>
        NullOrEmptyPrivateKey = 7,

        #endregion Common values for v1 and v2

        #region Values for reCAPTCHA v1

        /// <summary>
        /// The request parameters in the client-side cookie are invalid.
        /// </summary>
        InvalidCookieParameters = 3,
        /// <summary>
        /// The recaptcha challenge could not be retrieved.
        /// </summary>
        ChallengeNotProvided = 6

        #endregion Values for reCAPTCHA v1
    }
}
