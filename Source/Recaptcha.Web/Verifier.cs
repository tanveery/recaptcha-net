/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the functionality for verifying reCAPTCHA client-side API response with request to the server-side reCAPTCHA API.
    /// </summary>
    public class Verifier
    {
        private const string ResponseParameterKey = "g-reCAPTCHA-response";
        private readonly string responseValue;
        private readonly string privateKey;

        /// <summary>
        /// Creates an instance of the <see cref="Verifier"/> class.
        /// </summary>
        /// <param name="privateKey">Sets the private key (Secret key) to be part of the reCAPTCHA verification request.</param>
        internal Verifier(string privateKey)
        {
            if (String.IsNullOrEmpty(privateKey))
            {
                throw new ArgumentNullException("privateKey", "Private key cannot be null or empty.");
            }

            if (HttpContext.Current == null || HttpContext.Current.Request == null)
            {
                throw new InvalidOperationException("HTTP context is not set or current request request is not available.");
            }

            this.privateKey = privateKey;

            var request = HttpContext.Current.Request;
            responseValue = request.Form[ResponseParameterKey];
            if (string.IsNullOrEmpty(responseValue))
            {
                responseValue = request.Params[ResponseParameterKey];
            }
        }

        /// <summary>
        /// Gets the private key (Secret key) used to make the reCAPTCHA verification request.
        /// </summary>
        public string PrivateKey
        {
            get { return privateKey; }
        }

        /// <summary>
        /// Verifies if the CAPTCHA is solved correctly by the end user.
        /// </summary>
        /// <returns>True if the CAPTCHA is solved correctly and no error was found, otherwise returns false.</returns>
        public async Task<bool> VerifyIfSolvedAsync()
        {
            var result = await GetErrorCodeAsync();
            return result == ErrorCode.NoError;
        }

        /// <summary>
        /// Verifies if the CAPTCHA is solved correctly by the end user.
        /// </summary>
        /// <returns>Returns the result as a value of the <see cref="ErrorCode"/> enum.</returns>
        public async Task<ErrorCode> GetErrorCodeAsync()
        {
            if (string.IsNullOrEmpty(responseValue))
            {
                return ErrorCode.MissingInputResponse;
            }

            var privateKeyValue = KeyHelper.LoadKey(privateKey);
            if (String.IsNullOrEmpty(privateKeyValue))
            {
                return ErrorCode.MissingInputSecret;
            }

            var uriBuilder = new UriBuilder("https://www.google.com/recaptcha/api/siteverify");
            uriBuilder.Query = String.Format("secret={0}&response={1}", privateKeyValue, responseValue);

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uriBuilder.Uri);
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var resultObject = JObject.Parse(responseString);
                if (resultObject.Value<bool>("success"))
                {
                    return ErrorCode.NoError;
                }

                return ParseErrorCodes(resultObject.Value<IEnumerable<string>>("error-codes"));
            }
        }

        private static ErrorCode ParseErrorCodes(IEnumerable<string> responseValues)
        {
            if (responseValues == null || responseValues.Any() == false)
            {
                return ErrorCode.Unknown;
            }

            var result = ErrorCode.NoError;
            foreach (var value in responseValues)
            {
                switch (value)
                {
                    case "missing-input-secret":
                        result |= ErrorCode.MissingInputSecret;
                        break;
                    case "invalid-input-secret":
                        result |= ErrorCode.InvalidInputSecret;
                        break;
                    case "missing-input-response":
                        result |= ErrorCode.MissingInputResponse;
                        break;
                    case "invalid-input-response":
                        result |= ErrorCode.InvalidInputResponse;
                        break;
                }
            }

            return result;
        }
    }
}