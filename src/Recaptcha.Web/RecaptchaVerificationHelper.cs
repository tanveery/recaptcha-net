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
using Newtonsoft.Json;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the functionality for verifying user's response to the recpatcha challenge.
    /// </summary>
    public class RecaptchaVerificationHelper
    {
        #region Fields

        private string _challenge = null;
        private bool _isVersion2 = false;

        #endregion Fields

        #region Constructors

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

            //Check if its version 2
            if (request.Form.AllKeys.Contains("g-recaptcha-response"))
            {
                Response = request.Form["g-recaptcha-response"];
                _isVersion2 = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(request.Form["recaptcha_challenge_field"]))
                {
                    this._challenge = request.Form["recaptcha_challenge_field"];
                    this.Response = request.Form["recaptcha_response_field"];
                }
                else
                {
                    this._challenge = request.Params["recaptcha_challenge_field"];
                    this.Response = request.Params["recaptcha_response_field"];
                }
            }
        }

        #endregion Constructors

        #region Properties

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

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Verifies whether the user's response to the recaptcha request is correct.
        /// </summary>
        /// <returns>Returns the result as a value of the <see cref="RecaptchaVerificationResult"/> enum.</returns>
        public RecaptchaVerificationResult VerifyRecaptchaResponse()
        {
            if(string.IsNullOrEmpty(Response))
            {
                return RecaptchaVerificationResult.NullOrEmptyCaptchaSolution;
            }

            string privateKey = RecaptchaKeyHelper.ParseKey(PrivateKey);

            if(_isVersion2)
            {
                return VerifyRecpatcha2Response(privateKey);
            }

            if (string.IsNullOrEmpty(_challenge))
            {
                return RecaptchaVerificationResult.ChallengeNotProvided;
            }

            string postData = String.Format("privatekey={0}&remoteip={1}&challenge={2}&response={3}", privateKey, this.UserHostAddress, this._challenge, this.Response);

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

                return ToRecaptchaVerificationResult(responseTokens);
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
            if (string.IsNullOrEmpty(Response))
            {
                return FromTaskResult<RecaptchaVerificationResult>(RecaptchaVerificationResult.NullOrEmptyCaptchaSolution);
            }

            string privateKey = RecaptchaKeyHelper.ParseKey(PrivateKey);

            if (_isVersion2)
            {
                return VerifyRecpatcha2ResponseTaskAsync(privateKey);
            }

            if (string.IsNullOrEmpty(_challenge))
            {
                return FromTaskResult<RecaptchaVerificationResult>(RecaptchaVerificationResult.ChallengeNotProvided);
            }

            Task<RecaptchaVerificationResult> result = Task<RecaptchaVerificationResult>.Factory.StartNew(() =>
            {
                string postData = String.Format("privatekey={0}&remoteip={1}&challenge={2}&response={3}", privateKey, this.UserHostAddress, this._challenge, this.Response);

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

                    return ToRecaptchaVerificationResult(responseTokens);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

            return result;
        }

        #endregion Public Methods

        #region Private Methods

        private Task<RecaptchaVerificationResult> VerifyRecpatcha2ResponseTaskAsync(string privateKey)
        {
            Task<RecaptchaVerificationResult> taskResult = Task<RecaptchaVerificationResult>.Factory.StartNew(() =>
            {
                string postData = String.Format("secret={0}&response={1}&remoteip={2}", privateKey, this.Response, this.UserHostAddress);

                byte[] postDataBuffer = System.Text.Encoding.ASCII.GetBytes(postData);

                Uri verifyUri = null;

                verifyUri = new Uri("https://www.google.com/recaptcha/api/siteverify", UriKind.Absolute);

                try
                {
                    var webRequest = (HttpWebRequest)WebRequest.Create(verifyUri);
                    webRequest.ContentType = "application/x-www-form-urlencoded";
                    webRequest.ContentLength = postDataBuffer.Length;
                    webRequest.Method = "POST";

                    var proxy = WebRequest.GetSystemWebProxy();
                    proxy.Credentials = CredentialCache.DefaultCredentials;

                    webRequest.Proxy = proxy;

                    using (var requestStream = webRequest.GetRequestStream())
                    {
                        requestStream.Write(postDataBuffer, 0, postDataBuffer.Length);
                    }

                    var webResponse = (HttpWebResponse)webRequest.GetResponse();

                    string sResponse = null;

                    using (var sr = new StreamReader(webResponse.GetResponseStream()))
                    {
                        sResponse = sr.ReadToEnd();
                    }

                    var result = JsonConvert.DeserializeObject<Recaptcha2VerificationResult>(sResponse);
                    return ToRecaptchaVerificationResult(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

            return taskResult;
        }

        private RecaptchaVerificationResult VerifyRecpatcha2Response(string privateKey)
        {
            string postData = String.Format("secret={0}&response={1}&remoteip={2}", privateKey, this.Response, this.UserHostAddress);

            byte[] postDataBuffer = System.Text.Encoding.ASCII.GetBytes(postData);

            Uri verifyUri = null;

            verifyUri = new Uri("https://www.google.com/recaptcha/api/siteverify", UriKind.Absolute);

            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(verifyUri);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = postDataBuffer.Length;
                webRequest.Method = "POST";

                var proxy = WebRequest.GetSystemWebProxy();
                proxy.Credentials = CredentialCache.DefaultCredentials;

                webRequest.Proxy = proxy;

                using (var requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(postDataBuffer, 0, postDataBuffer.Length);
                }

                var webResponse = (HttpWebResponse)webRequest.GetResponse();

                string sResponse = null;

                using (var sr = new StreamReader(webResponse.GetResponseStream()))
                {
                    sResponse = sr.ReadToEnd();
                }

                var result = JsonConvert.DeserializeObject<Recaptcha2VerificationResult>(sResponse);
                return ToRecaptchaVerificationResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private RecaptchaVerificationResult ToRecaptchaVerificationResult(string[] result)
        {
            if (result.Length == 2)
            {
                Boolean success = result[0].Equals("true", StringComparison.CurrentCulture);

                if (success)
                {
                    return RecaptchaVerificationResult.Success;
                }
                else
                {
                    if (result[1].Equals("incorrect-captcha-sol", StringComparison.CurrentCulture))
                    {
                        return RecaptchaVerificationResult.IncorrectCaptchaSolution;
                    }
                    else if (result[1].Equals("invalid-site-private-key", StringComparison.CurrentCulture))
                    {
                        return RecaptchaVerificationResult.InvalidPrivateKey;
                    }
                    else if (result[1].Equals("invalid-request-cookie", StringComparison.CurrentCulture))
                    {
                        return RecaptchaVerificationResult.InvalidCookieParameters;
                    }
                }
            }

            return RecaptchaVerificationResult.UnknownError;
        }

        private RecaptchaVerificationResult ToRecaptchaVerificationResult(Recaptcha2VerificationResult result)
        {
            if(result.Success)
            {
                return RecaptchaVerificationResult.Success;
            }

			if (result.ErrorCodes == null || result.ErrorCodes.Length == 0)
			{
				return RecaptchaVerificationResult.UnknownError;
			}

			switch (result.ErrorCodes[0])
            {
                case "missing-input-secret":
                    return RecaptchaVerificationResult.InvalidPrivateKey;
                case "invalid-input-secret":
                    return RecaptchaVerificationResult.InvalidPrivateKey;
                case "missing-input-response":
                    return RecaptchaVerificationResult.NullOrEmptyCaptchaSolution;
                case "invalid-input-response":
                    return RecaptchaVerificationResult.IncorrectCaptchaSolution;
            }

            return RecaptchaVerificationResult.UnknownError;
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

        #endregion Private Methods
    }
}