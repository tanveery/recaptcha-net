﻿/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using Recaptcha.Web.Configuration;
using System;
using System.IO;

#if NETCOREAPP
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using IHtmlString = Microsoft.AspNetCore.Html.IHtmlContent;
using HtmlHelper = Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper;
#else
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
#endif

namespace Recaptcha.Web.Mvc
{
    /// <summary>
    /// Represents the Recaptcha method extensions container for the <see cref="System.Web.Mvc.HtmlHelper"/> and <see cref="System.Web.Mvc.Controller"/> classes.
    /// </summary>
    public static class RecaptchaMvcExtensions
    {
        #region Public Methods

        /// <summary>
        /// Renders the recaptcha HTML in an MVC view. It is an extension method to the <see cref="System.Web.Mvc.HtmlHelper"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="System.Web.Mvc.HtmlHelper"/> object to which the extension is added.</param>
        /// <param name="siteKey">Sets the site key of recaptcha.</param>
        /// <param name="renderApiScript">Determines if the API script call is to be rendered.</param>
        /// <param name="theme">Sets the theme of recaptcha.</param>
        /// <param name="language">Sets the language of recaptcha. If no language is specified, the language of the current UI culture will be used.</param>
        /// <param name="tabIndex">Sets the tab index of recaptcha.</param>
        /// <param name="size">Sets the size of recaptcha.</param>
        /// <param name="useSsl">Sets the value to the UseSsl property.</param>
        /// <param name="apiVersion">Determines the version of the reCAPTCHA API.</param>
        /// <returns>Returns an instance of the IHtmlString type.</returns>
        [Obsolete("This method is obsolete and will be removed in future. Please use RecaptchaWidget method instead.")]
        public static IHtmlString Recaptcha(
            this HtmlHelper htmlHelper,
            string siteKey = null,
            bool renderApiScript = true,
            RecaptchaTheme? theme = null,
            string language = null,
            int? tabIndex = null,
            RecaptchaSize? size = null,
            RecaptchaSslBehavior? useSsl = null,
            string apiVersion = null)
        {
            return RecaptchaWidget(htmlHelper, siteKey, renderApiScript, theme, language, tabIndex, size, useSsl, apiVersion);
        }

        /// <summary>
        /// Renders the recaptcha HTML in an MVC view. It is an extension method to the <see cref="System.Web.Mvc.HtmlHelper"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="System.Web.Mvc.HtmlHelper"/> object to which the extension is added.</param>
        /// <param name="siteKey">Sets the site key of recaptcha.</param>
        /// <param name="renderApiScript">Determines if the API script call is to be rendered.</param>
        /// <param name="theme">Sets the theme of recaptcha.</param>
        /// <param name="language">Sets the language of recaptcha. If no language is specified, the language of the current UI culture will be used.</param>
        /// <param name="tabIndex">Sets the tab index of recaptcha.</param>
        /// <param name="size">Sets the size of recaptcha.</param>
        /// <param name="useSsl">Sets the value to the UseSsl property.</param>
        /// <param name="apiVersion">Determines the version of the reCAPTCHA API.</param>
        /// <returns>Returns an instance of the IHtmlString type.</returns>
        public static IHtmlString RecaptchaWidget(
            this HtmlHelper htmlHelper,
            string siteKey = null,
            bool renderApiScript = true,
            RecaptchaTheme? theme = null,
            string language = null,
            int? tabIndex = null,
            RecaptchaSize? size = null,
            RecaptchaSslBehavior? useSsl = null,
            string apiVersion = null)
        {
            var config = RecaptchaConfigurationManager.GetConfiguration();
            string ver;
            if (!string.IsNullOrEmpty(apiVersion))
            {
                ver = apiVersion;
            }
            else
            {
                ver = config.ApiVersion;
            }

            if (ver == null || ver == "2")
            {
                var rHtmlHelper = new Recaptcha2HtmlHelper(siteKey ?? config.SiteKey);
#if NETCOREAPP
                return new HtmlString(rHtmlHelper.CreateWidgetHtml(renderApiScript, theme != null ? (RecaptchaTheme)theme : config.Theme, language ?? config.Language, tabIndex != null ? (int)tabIndex : 0, size != null ? (RecaptchaSize)size : config.Size, useSsl != null ? (RecaptchaSslBehavior)useSsl : config.UseSsl));
#else
                var writer = new HtmlTextWriter(new StringWriter());
                writer.Write(rHtmlHelper.CreateWidgetHtml(renderApiScript, theme != null ? (RecaptchaTheme)theme : config.Theme, language ?? config.Language, tabIndex != null ? (int)tabIndex : 0, size != null ? (RecaptchaSize)size : config.Size, useSsl != null ? (RecaptchaSslBehavior)useSsl : config.UseSsl));

                return htmlHelper.Raw(writer.InnerWriter.ToString());
#endif
            }
            else
            {
                throw new InvalidOperationException("The API version is either invalid or not supported.");
            }
        }

        /// <summary>
        /// Renders the recaptcha HTML in an MVC view. It is an extension method to the <see cref="System.Web.Mvc.HtmlHelper"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="System.Web.Mvc.HtmlHelper"/> object to which the extension is added.</param>
        /// <param name="siteKey">Sets the site key of recaptcha.</param>
        /// <param name="language">Sets the language of recaptcha. If no language is specified, the language of the current UI culture will be used.</param>
        /// <param name="useSsl">Sets the value to the UseSsl property.</param>
        /// <param name="apiVersion">Determines the version of the reCAPTCHA API.</param>
        /// <returns>Returns an instance of the IHtmlString type.</returns>
        public static IHtmlString RecaptchaApiScript(
            this HtmlHelper htmlHelper,
            string siteKey = null,
            string language = null,
            RecaptchaSslBehavior? useSsl = null,
            string apiVersion = null)
        {
            var config = RecaptchaConfigurationManager.GetConfiguration();
            string ver;
            if (!string.IsNullOrEmpty(apiVersion))
            {
                ver = apiVersion;
            }
            else
            {
                ver = config.ApiVersion;
            }

            if (ver == null || ver == "2")
            {
                var rHtmlHelper = new Recaptcha2HtmlHelper(siteKey ?? config.SiteKey);
#if NETCOREAPP
                return new HtmlString(rHtmlHelper.CreateApiScripttHtml(language ?? config.Language, useSsl != null ? (RecaptchaSslBehavior)useSsl : config.UseSsl));
#else
                var writer = new HtmlTextWriter(new StringWriter());
                writer.Write(rHtmlHelper.CreateApiScripttHtml(language ?? config.Language, useSsl != null ? (RecaptchaSslBehavior)useSsl : config.UseSsl));

                return htmlHelper.Raw(writer.InnerWriter.ToString());
#endif
            }
            else
            {
                throw new InvalidOperationException("The API version is either invalid or not supported.");
            }
        }

        /// <summary>
        /// Gets an instance of the <see cref="RecaptchaVerificationHelper"/> class that can be used to verify user's response to the recaptcha's challenge. 
        /// </summary>
        /// <param name="controller">The <see cref="System.Web.Mvc.Controller"/> object to which the extension method is added to.</param>
        /// <param name="secretKey">The private key required for making the recaptcha verification request.</param>
        /// <param name="response">The recaptcha response. If not specified, it will be taken from the Request.Form</param>
        /// <returns>Returns an instance of the <see cref="RecaptchaVerificationHelper"/> class.</returns>
        public static RecaptchaVerificationHelper GetRecaptchaVerificationHelper(this Controller controller, string secretKey, string response = null)
        {
            return new RecaptchaVerificationHelper(
#if NETCOREAPP
                controller.HttpContext,
#endif
                secretKey, response);
        }

        /// <summary>
        /// Gets an instance of the <see cref="RecaptchaVerificationHelper"/> class that can be used to verify user's response to the recaptcha's challenge. 
        /// </summary>
        /// <param name="controller">The <see cref="System.Web.Mvc.Controller"/> object to which the extension method is added to.</param>
        /// <returns>Returns an instance of the <see cref="RecaptchaVerificationHelper"/> class.</returns>
        public static RecaptchaVerificationHelper GetRecaptchaVerificationHelper(this Controller controller)
        {
            var config = RecaptchaConfigurationManager.GetConfiguration();
            return new RecaptchaVerificationHelper(
#if NETCOREAPP
                controller.HttpContext,
#endif
                config.SecretKey);
        }

#endregion Public Methods
    }
}