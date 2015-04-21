/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the functionality for verifying user's response to the recpatcha challenge.
    /// </summary>
    public class RecaptchaVerificationHelper
    {
        private string _Challenge = null;

        private RecaptchaVerificationHelper()
        { }

        /// <summary>
        /// Creates an instance of the <see cref="RecaptchaVerificationHelper"/> class.
        /// </summary>
        /// <param name="privateKey">Sets the private key of the recaptcha verification request.</param>
        internal RecaptchaVerificationHelper(string privateKey)
        {
            if (String.IsNullOrEmpty(privateKey))
            {
                throw new InvalidOperationException("Private key cannot be null or empty.");
            }

            if (HttpContext.Current == null || HttpContext.Current.Request == null)
            {
                throw new InvalidOperationException("Http request context does not exist.");
            }

            HttpRequest request = HttpContext.Current.Request;

            this.UseSsl = request.IsSecureConnection;

            this.PrivateKey = privateKey;
            this.UserHostAddress = request.UserHostAddress;

            if (!string.IsNullOrEmpty(request.Form["recaptcha_challenge_field"]))
            {
                this._Challenge = request.Form["recaptcha_challenge_field"];
                this.Response = request.Form["recaptcha_response_field"];
            }
            else
            {
                this._Challenge = request.Params["recaptcha_challenge_field"];
                this.Response = request.Params["recaptcha_response_field"];
            }
        }

        /// <summary>
        /// Determines if HTTPS intead of HTTP is to be used in Recaptcha verification API calls.
        /// </summary>
        public bool UseSsl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the privae key of the recaptcha verification request.
        /// </summary>
        public string PrivateKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the user's host address of the recaptcha verification request.
        /// </summary>
        public string UserHostAddress
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the user's response to the recaptcha challenge of the recaptcha verification request.
        /// </summary>
        public string Response
        {
            get;
            private set;
        }

        /// <summary>
        /// Verifies whether the user's response to the recaptcha request is correct.
        /// </summary>
        /// <returns>Returns the result as a value of the <see cref="RecaptchaVerificationResult"/> enum.</returns>
        public RecaptchaVerificationResult VerifyRecaptchaResponse()
        {
            if(string.IsNullOrEmpty(_Challenge))
            {
                return RecaptchaVerificationResult.ChallengeNotProvided;
            }

            if(string.IsNullOrEmpty(Response))
            {
                return RecaptchaVerificationResult.NullOrEmptyCaptchaSolution;
            }

            string privateKey = RecaptchaKeyHelper.ParseKey(PrivateKey);

            string postData = String.Format("privatekey={0}&remoteip={1}&challenge={2}&response={3}", privateKey, this.UserHostAddress, this._Challenge, this.Response);

            byte[] postDataBuffer = System.Text.Encoding.ASCII.GetBytes(postData);

            Uri verifyUri = null;

            if (!UseSsl)
            {
                verifyUri = new Uri("http://www.google.com/recaptcha/api/verify", UriKind.Absolute);
            }
            else
            {
                verifyUri = new Uri("https://www.google.com/recaptcha/api/verify", UriKind.Absolute);
            }

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(verifyUri);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = postDataBuffer.Length;
                webRequest.Method = "POST";

                IWebProxy proxy = WebRequest.GetSystemWebProxy();
                proxy.Credentials = CredentialCache.DefaultCredentials;

                webRequest.Proxy = proxy;

                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(postDataBuffer, 0, postDataBuffer.Length);
                }

                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

                string[] responseTokens = null;
                using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
                {
                    responseTokens = sr.ReadToEnd().Split('\n');
                }

                if (responseTokens.Length == 2)
                {
                    Boolean success = responseTokens[0].Equals("true", StringComparison.CurrentCulture);

                    if (success)
                    {
                        return RecaptchaVerificationResult.Success;
                    }
                    else
                    {
                        if (responseTokens[1].Equals("incorrect-captcha-sol", StringComparison.CurrentCulture))
                        {
                            return RecaptchaVerificationResult.IncorrectCaptchaSolution;
                        }
                        else if (responseTokens[1].Equals("invalid-site-private-key", StringComparison.CurrentCulture))
                        {
                            return RecaptchaVerificationResult.InvalidPrivateKey;
                        }
                        else if (responseTokens[1].Equals("invalid-request-cookie", StringComparison.CurrentCulture))
                        {
                            return RecaptchaVerificationResult.InvalidCookieParameters;
                        }
                    }
                }

                return RecaptchaVerificationResult.UnknownError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Verifies whether the user's response to the recaptcha request is correct.
        /// </summary>
        /// <returns>Returns the result as a value of the <see cref="RecaptchaVerificationResult"/> enum.</returns>
        public Task<RecaptchaVerificationResult> VerifyRecaptchaResponseTaskAsync()
        {
            if (string.IsNullOrEmpty(_Challenge))
            {
                return FromTaskResult<RecaptchaVerificationResult>(RecaptchaVerificationResult.ChallengeNotProvided);
            }

            if (string.IsNullOrEmpty(Response))
            {
                return FromTaskResult<RecaptchaVerificationResult>(RecaptchaVerificationResult.NullOrEmptyCaptchaSolution);
            }

            Task<RecaptchaVerificationResult> result = Task<RecaptchaVerificationResult>.Factory.StartNew(() =>
            {
                string privateKey = RecaptchaKeyHelper.ParseKey(PrivateKey);

                string postData = String.Format("privatekey={0}&remoteip={1}&challenge={2}&response={3}", privateKey, this.UserHostAddress, this._Challenge, this.Response);

                byte[] postDataBuffer = System.Text.Encoding.ASCII.GetBytes(postData);

                Uri verifyUri = null;

                if (!UseSsl)
                {
                    verifyUri = new Uri("http://www.google.com/recaptcha/api/verify", UriKind.Absolute);
                }
                else
                {
                    verifyUri = new Uri("https://www.google.com/recaptcha/api/verify", UriKind.Absolute);
                }

                try
                {
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(verifyUri);
                    webRequest.ContentType = "application/x-www-form-urlencoded";
                    webRequest.ContentLength = postDataBuffer.Length;
                    webRequest.Method = "POST";

                    IWebProxy proxy = WebRequest.GetSystemWebProxy();
                    proxy.Credentials = CredentialCache.DefaultCredentials;

                    webRequest.Proxy = proxy;

                    Stream requestStream = webRequest.GetRequestStream();
                    requestStream.Write(postDataBuffer, 0, postDataBuffer.Length);

                    HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

                    string[] responseTokens = null;
                    using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
                    {
                        responseTokens = sr.ReadToEnd().Split('\n');
                    }

                    if (responseTokens.Length == 2)
                    {
                        Boolean success = responseTokens[0].Equals("true", StringComparison.CurrentCulture);

                        if (success)
                        {
                            return RecaptchaVerificationResult.Success;
                        }
                        else
                        {
                            if (responseTokens[1].Equals("incorrect-captcha-sol", StringComparison.CurrentCulture))
                            {
                                return RecaptchaVerificationResult.IncorrectCaptchaSolution;
                            }
                            else if (responseTokens[1].Equals("invalid-site-private-key", StringComparison.CurrentCulture))
                            {
                                return RecaptchaVerificationResult.InvalidPrivateKey;
                            }
                            else if (responseTokens[1].Equals("invalid-request-cookie", StringComparison.CurrentCulture))
                            {
                                return RecaptchaVerificationResult.InvalidCookieParameters;
                            }
                        }
                    }

                    return RecaptchaVerificationResult.UnknownError;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

            return result;
        }

        // Added this method to support backward compatibility with
        // .NET Framework 4.0 since Task.FromResult<T> is available
        // only in 4.5+.
        private Task<TValue> FromTaskResult<TValue>(TValue value)
        {
            var tcs = new TaskCompletionSource<TValue>();
            tcs.SetResult(value);
            return tcs.Task;
        }
    }
}