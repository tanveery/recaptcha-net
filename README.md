<h1>reCAPTCHA library for .NET</h1>
reCAPTCHA for .NET is one of the most popular and well-documented reCAPTCHA libraries used by thousands of .NET developers in their ASP.NET web forms and MVC applications. The library is created and maintained by <a href="http://twitter.com/tanveery">@tanveery</a>.
<h2>Features</h2>
<p>The primary features of the library are:</p>
<ul>
    <li>Render recaptcha control (HTML) with appropriate options for pre-defined themes and culture (language).</li>
    <li>Verify user's answer to recaptcha's challenge.</li>
    <li>Supports ASP.NET Web Forms and ASP.NET MVC.</li>
    <li>Supprts reCAPTCHA version 1 and version 2 in a seamless fashion.</li>
    <li>One of the most well-documented reCAPTCHA libraries in the open source community.</li>
</ul>
<h2>API Support</h2>
<p>The library supports Google's reCATPCAH API version 1 and version 2 in a seamless fashion. To switch between the two APIs, all you need is to set <code>1</code> or <code>2</code> as a value to the <code>recaptchaApiVersion</code> app settings key.</p>
<h2>Creating a reCAPTCHA API Key</h2>
<p>Before you can use reCAPTCHA in your web application, you must first create a reCAPTCHA API key (a pair of public and private keys). Creating reCAPTCHA API key is very straight-forward. The following are the steps:</p>
<ol>
    <li>Go to the Google's <a href="https://www.google.com/recaptcha" target="_blank">reCAPTCHA</a> site.</li>
    <li>Click on the <strong>Get reCAPTCHA</strong> button. You will be required to login with your Google account.</li>
    <li>Under the <strong>Register a new site</strong> section, enter a label and the domain of your web application. You can enter more than one domain if you want to. Optionally, you can add one more owners of this new reCAPTCHA API key.</li>
    <li>Click on the <strong>Register</strong> button.
    <li>Under the <strong>Adding reCAPTCHA to your site</strong> section, take note of your <strong>Site Key</strong> and <strong>Secret Key</strong> which you would need to specify in your application's web.config file.</li>
</ol>
<h2>Installation</h2>
<h3>reCAPTCHA Nuget Package</h3>
<p>The best and the recommended way to install the latest version of reCAPTCHA for .NET is through Nuget. From the <a href="http://docs.nuget.org/consume/package-manager-console">Nuget's Package Manager Console</a> in your Visual Studio .NET IDE, simply execute the following command:</p>
<pre><code>PM&gt; Install-Package RecaptchaNet</code></pre>
<h3>Latest Release</h3>
<p>You can also download a released build of reCAPTCHA for .NET by going to the <a href="https://github.com/tanveery/recaptcha-net/releases">Releases</a> section of this project. The latest release is <a href="https://github.com/tanveery/recaptcha-net/releases/tag/v2.0">reCAPTCHA for .NET v2.0</a>.</p>
<h2>Issues</h2>
If you find a bug in the library or you have an idea about a new feature, please try to search in the existing list of <a href="https://github.com/tanveery/recaptcha-net/issues">issues</a>. If the bug or idea is not listed and addressed there, please <a href="https://github.com/tanveery/recaptcha-net/issues/new">open a new issue</a>.
<h2>Quick Starter</h2>
<h3>How to Use reCAPTCHA in an ASP.NET Web Forms Application</h3>
<p>Add the following line just under the Page directive in your .aspx or .ascx file:</p>
<pre><code>&lt;%@ Register Assembly="Recaptcha.Web" Namespace="Recaptcha.Web.UI.Controls"
TagPrefix="cc1" %&gt;
</code></pre>
<p>Then at the desired line in the same file add the reCAPTCHA control as follows:</p>
<pre><code>&lt;cc1:Recaptcha ID="Recaptcha1" PublicKey="Your site key"
PrivateKey="Your secret key" runat="server" /&gt;
</code></pre>
<p>Rather than setting the recaptcha key of the control through its PublicKey and PrivateKey properties, you can set them in your web.config file instead:</p>
<p><a href="#keyInWebConfig">How to Set Recaptcha Key in Web.config File</a></p>
<h3>How to Set Recaptcha Key in Web.config File</h3>
<p>After you set the private and public keys in your web.config file, all you need in your web form is this following piece of code:</p>
<pre><code>&lt;cc1:Recaptcha ID="Recaptcha1" runat="server" /&gt;
</code></pre>
<p>By default, the theme of the reCAPTCHA control is Red. However, you can change this default theme to one of the other three themes if you like. Those themes are: Blackglass, White, and Clean. Theme can be set by using the <strong>RecaptchaTheme</strong> enum. The following is an example:</p>
<pre><code>&lt;cc1:Recaptcha ID="Recaptcha1" Theme="RecaptchaTheme.Clean" runat="server" /&gt;
</code></pre>
<h4>Add the reCAPTCHA Control to the Visual Studio Toolbox</h4>
<p>Instead of writing the above code manually, you can easily drag and drop the same reCAPTCHA control from the Visual Studio Toolbox onto your page designer just like the way you would do for other standard ASP.NET controls. However, you would need to add the reCAPTCHA control to the Toolbox first. Simply, right click on the Toolbox and select Choose Items... from the context menu and then under the .NET Framework Components tab click on the Browse button and locate the <strong>Recaptcha.Web.dll</strong> assembly.</p>
<h3>Verify User's Response to reCAPTCHA Challenge</h3>
<p>When your end-user submits the form that contains the reCAPTCHA control, you obviously would want to verify whether the user's answer was valid based on what was displayed in the recaptcha image. It is very easy to do with one or two lines.</p>
<p>First of all as expected, import the namespace <strong>Recaptcha.Web</strong> in your code-behind file:</p>
<pre><code>using Recaptcha.Web;
</code></pre>
<p>To verify whether the user's answer is correct, call the control's <strong>Verify()</strong> method which returns RecaptchaVerificationResult. You can also use the control's <strong>Response</strong> property to check what the actual answer is. Generally, you would want to use the Response property to check if the user provided a blank response which of course is always wrong:</p>
<pre><code>if (String.IsNullOrEmpty(Recaptcha1.Response))
{
    lblMessage.Text = "Captcha cannot be empty.";
}
else
{
    RecaptchaVerificationResult result = await Recaptcha1.Verify();
    if (result == RecaptchaVerificationResult.Success)
    {
        Response.Redirect("Welcome.aspx");
    }
    if (result == RecaptchaVerificationResult.IncorrectCaptchaSolution)
    {
        lblMessage.Text = "Incorrect captcha response.";
    }
    else
    {
        lblMessage.Text = "Some other problem with captcha.";
    }
}
</code></pre>
<p>Instead of calling the <strong>Verify()</strong> method, you can call the <strong>VerifyTaskAsync()</strong> method to verify the user's response asynchronously which at the same time can be used along with the new await keyword:</p>
<pre><code>if (String.IsNullOrEmpty(Recaptcha1.Response))
{
    lblMessage.Text = "Captcha cannot be empty.";
}
else
{
    RecaptchaVerificationResult result = await Recaptcha1.VerifyTaskAsync();
    if (result == RecaptchaVerificationResult.Success)
    {
        Response.Redirect("Welcome.aspx");
    }
    if (result == RecaptchaVerificationResult.IncorrectCaptchaSolution)
    {
        lblMessage.Text = "Incorrect captcha response.";
    }
    else
    {
        lblMessage.Text = "Some other problem with captcha.";
    }
}
</code></pre>
<h3>How to Use reCAPTCHA in an ASP.NET MVC Web Application</h3>
<h4>Add the reCAPTCHA Control to Your MVC View</h4>
<p>Add the following line at the top of your view (a .cshtml file):</p>
<pre><code>@using Recaptcha.Web.Mvc;
</code></pre>
<p>Then at the desired line in the same file call the reCAPTCHA extension method of the HtmlHelper class as follows:</p>
<pre><code>@Html.Recaptcha(publicKey:"Your site key", privateKey:"Your secret key")
</code></pre>
<p>Rather than setting the recaptcha key through the PublicKey and PrivateKey properties of the HtmlHelper's recaptcha extension, you can set them in your web.config file instead:</p>
<p><a href="#keyInWebConfig">How to Set Recaptcha Key in Web.config File</a></p>
<p>After you set the private and public keys in your web.config file, all you need in your view is this following piece of code:</p>
<pre><code>@Html.Recaptcha()
</code></pre>
<p>By default, the theme of recaptcha is Red. However, you can change this default theme to one of the other three themes if you like. Those themes are: Blackglass, White, and Clean. Theme can be set by using the RecaptchaTheme enum. The following is an example:</p>
<pre><code>@Html.Recaptcha(theme:Recaptcha.Web.RecaptchaTheme.Clean);
</code></pre>
<h3>Verify User's Response to reCAPTCHA Challenge</h3>
<p>When your end-user submits the form that contains the reCAPTCHA control, you obviously would want to verify whether the user's answer was valid based on what was displayed in the recaptcha image. It is very easy to do with few lines.</p>
<p>First of all as expected, import the namespaces <strong>Recaptcha.Web</strong> and <strong>Recaptcha.Web.Mvc</strong> in your controller file:</p>
<pre><code>using Recaptcha.Web;
using Recaptcha.Web.Mvc;
</code></pre>
<p>To verify whether the user's answer is correct, you need to create an instance of the <strong>RecaptchaVerificationHelper</strong> class by calling the extension method <strong>GetRecaptchaVerificationHelper()</strong> of the controller. You can then call the <strong>RecaptchaVerificationHelper</strong> object's <strong>VerifyRecaptchaResponse()</strong> method which returns a <strong>RecaptchaVerificationResult</strong> enum. You can also use the helper object's <strong>Response</strong> property to check what the actual answer of the user is. Generally, you would want to use the Response property to check if the user provided a blank response which of course is always wrong:</p>
<pre><code>RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();
if (String.IsNullOrEmpty(recaptchaHelper.Response))
{
    ModelState.AddModelError("", "Captcha answer cannot be empty.");
    return View(model);
}
RecaptchaVerificationResult recaptchaResult = await recaptchaHelper.VerifyRecaptchaResponse();
if (recaptchaResult != RecaptchaVerificationResult.Success)
{
    ModelState.AddModelError("", "Incorrect captcha answer.");
}
</code></pre>
<p>Instead of calling the <strong>VerifyRecaptchaResponse()</strong> method, you can call the <strong>VerifyRecaptchaResponseTaskAsync()</strong> method to verify the user's response asynchronously which at the same time can be used along with the new await keyword:</p>
<pre><code>RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();
if (String.IsNullOrEmpty(recaptchaHelper.Response))
{
    ModelState.AddModelError("", "Captcha answer cannot be empty.");
    return View(model);
}
RecaptchaVerificationResult recaptchaResult = await recaptchaHelper.VerifyRecaptchaResponseTaskAsync();
if (recaptchaResult != RecaptchaVerificationResult.Success)
{
    ModelState.AddModelError("", "Incorrect captcha answer.");
}
</code></pre>
<p><strong>Note</strong>: The <strong>GetRecaptchaVerificationHelper()</strong> is an extension method to the MVC's built-in <strong>Controller</strong> class. This means you must import the <strong>Recaptcha.Web.Mvc</strong> namespace explicitly at the top of the controller file otherwise the code will not compile.</p>
<h3 id="keyInWebConfig">How to Set reCAPTCHA Key in Web.config File</h3>
<p>As you may have already seen, you can directly assign public and private keys to the respective properties of Recaptcha ASP.NET control or reCAPTCHA MVC HTML extension. However, a better way is to store these keys in your web.config file. The obvious benefit is that you can change these keys anytime you want without requiring you to modify your code and perhaps most important benefit is that you the keys you define in your web.config are global in your web project.</p>
<p>In the appSettings section of your web.config file, add the keys as follows:</p>
<pre><code>&lt;appSettings&gt;
&lt;add key="recaptchaPublicKey" value="Your site key" /&gt;
&lt;add key="recaptchaPrivateKey" value="Your secret key" /&gt;
&lt;add key="recaptchaApiVersion" value="2" /&gt;
&lt;/appSettings&gt;
</code></pre>
<p><strong>Note</strong>: The <strong>appSettings</strong> keys are automatically added to your web.config file if you install reCAPTCHA for .NET through Nuget. However, you would still need to provide your own public and private keys in the web.config file of your project.</p>
