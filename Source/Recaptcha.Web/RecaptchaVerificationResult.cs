/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the result value of reCAPTCHA verification process.
    /// </summary>
    public enum RecaptchaVerificationResult
    {
        /// <summary>
        /// Verification failed but the exact reason is not known.
        /// </summary>
        UnknownError = 0,

        /// <summary>
        /// Verification succeeded.
        /// </summary>
        Success = 1,

        /// <summary>
        /// The user's response to reCAPTCHA challenge is incorrect.
        /// </summary>
        IncorrectCaptchaSolution = 2,

        /// <summary>
        /// The request parameters in the client-side cookie are invalid.
        /// </summary>
        [Obsolete("Current version of API does not use cookies.")]
        InvalidCookieParameters = 3,

        /// <summary>
        /// The private supplied at the time of verification process is invalid.
        /// </summary>
        InvalidPrivateKey = 4,

        /// <summary>
        /// The user's response to the reCAPTCHA challenge is null or empty.
        /// </summary>
        NullOrEmptyCaptchaSolution = 5,

        /// <summary>
        /// The reCAPTCHA challenge could not be retrieved.
        /// </summary>
        [Obsolete("Current version of API does not use challenge.")]
        ChallengeNotProvided = 6
    }
}
