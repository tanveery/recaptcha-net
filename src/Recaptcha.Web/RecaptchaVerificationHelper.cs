﻿/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Web;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Recaptcha.Web.Configuration;
#if NETCOREAPP
using Microsoft.AspNetCore.Http;
#endif

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the functionality for verifying user's response to the recpatcha challenge.
    /// </summary>
    public class RecaptchaVerificationHelper
    {
        #region Constructors

        private RecaptchaVerificationHelper()
        { }

        /// <summary>
        /// Creates an instance of the <see cref="RecaptchaVerificationHelper"/> class.
        /// </summary>
        /// <param name="secretKey">Sets the secret key of the reCAPTCHA verification request.</param>
        /// <param name="response">The recaptcha response. If not specified, it will be taken from the Request.Form</param>
        internal RecaptchaVerificationHelper(
#if NETCOREAPP
            HttpContext httpContext,
#endif
            string secretKey, string response = null)
        {
            if (String.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("Private key cannot be null or empty.");
            }

#if NETCOREAPP
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
#else
            if (HttpContext.Current == null || HttpContext.Current.Request == null)
            {
                throw new InvalidOperationException("Http request context does not exist.");
            }
#endif

#if NETCOREAPP
            var request = httpContext.Request;
#else
            HttpRequest request = HttpContext.Current.Request;
#endif

#if NETCOREAPP
            this.UseSsl = request.IsHttps;
#else
            this.UseSsl = request.IsSecureConnection;
#endif

            this.SecretKey = secretKey;
#if NETCOREAPP
            this.UserHostAddress = request.Path.Value;
#else
            this.UserHostAddress = request.UserHostAddress;
#endif

            if (response == null)
                response = request.Form["g-recaptcha-response"];

            Response = response;
        }

#endregion Constructors

#region Properties

        /// <summary>
        /// Determines if HTTPS intead of HTTP is to be used in reCAPTCHA verification API calls.
        /// </summary>
        public bool UseSsl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the privae key of the recaptcha verification request.
        /// </summary>
        public string SecretKey
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
            if (string.IsNullOrEmpty(Response))
            {
                throw new InvalidOperationException("Reponse is emptry.");
            }

            string secretKey = SecretKey;

            if(string.IsNullOrEmpty(secretKey))
            {
                var config = RecaptchaConfigurationManager.GetConfiguration();
                secretKey = config.SecretKey;
            }

            return VerifyRecpatcha2Response(secretKey);
        }

        /// <summary>
        /// Verifies whether the user's response to the recaptcha request is correct.
        /// </summary>
        /// <returns>Returns the result as a value of the <see cref="RecaptchaVerificationResult"/> enum.</returns>
        public Task<RecaptchaVerificationResult> VerifyRecaptchaResponseTaskAsync()
        {
            if (string.IsNullOrEmpty(Response))
            {
                throw new InvalidOperationException("Reponse is emptry.");
            }

            string secretKey = SecretKey;

            if (string.IsNullOrEmpty(secretKey))
            {
                var config = RecaptchaConfigurationManager.GetConfiguration();
                secretKey = config.SecretKey;
            }

            return VerifyRecpatcha2ResponseTaskAsync(secretKey);
        }

#endregion Public Methods

#region Private Methods

        private Task<RecaptchaVerificationResult> VerifyRecpatcha2ResponseTaskAsync(string secretKey)
        {
            Task<RecaptchaVerificationResult> taskResult = Task<RecaptchaVerificationResult>.Factory.StartNew(() =>
            {
                string postData = String.Format("secret={0}&response={1}&remoteip={2}", secretKey, this.Response, this.UserHostAddress);

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

                    return JsonConvert.DeserializeObject<RecaptchaVerificationResult>(sResponse);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

            return taskResult;
        }

        private RecaptchaVerificationResult VerifyRecpatcha2Response(string secretKey)
        {
            string postData = String.Format("secret={0}&response={1}&remoteip={2}", secretKey, this.Response, this.UserHostAddress);

            byte[] postDataBuffer = System.Text.Encoding.ASCII.GetBytes(postData);
            Uri verifyUri = new Uri("https://www.google.com/recaptcha/api/siteverify", UriKind.Absolute);
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

                return JsonConvert.DeserializeObject<RecaptchaVerificationResult>(sResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

#endregion Private Methods
    }
}