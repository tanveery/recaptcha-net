<h1>reCAPTCHA library for .NET</h1>
reCAPTCHA for .NET is one of the most popular and well-documented reCAPTCHA libraries used by thousands of .NET developers in their ASP.NET web applications. The library is created and maintained by <a href="http://twitter.com/tanveery">@tanveery</a>.
<h2>Highlights</h2>
<p>The following are the highlights of the library:</p>
<ul>
    <li>Renders reCAPTCHA widget and verifies reCAPTCHA response with minimal amount of code</li>
    <li>Provides reCAPTCHA web control (ASP.NET Web Forms for .NET Framework 4.5 and above</li>
    <li>Provides HTML helper to quickly render reCAPTCHA widget (ASP.NET MVC 5 / ASP.NET Core 3.1 and above)
    <li>Supprts reCAPTCHA version 2</li>
    <li>One of the most well-documented reCAPTCHA libraries in the open source community</li>
</ul>
<h2>Creating a reCAPTCHA API Key</h2>
<p>Before you can use reCAPTCHA in your web application, you must first create a reCAPTCHA API key (a pair of site and secret keys). Creating reCAPTCHA API key is very straight-forward. The following are the steps:</p>
<ol>
    <li>Go to the Google's <a href="https://www.google.com/recaptcha" target="_blank">reCAPTCHA</a> site.</li>
    <li>Click on the <strong>Admin Console</strong> menu option. You will be required to login with your Google account.</li>
    <li>In the <strong>Admin Console</strong> page, click on the <strong>Create</strong> button.</li>
    <li>Enter a label for your web application.</li>
    <li>Select <strong>reCAPTCHA v2</strong> option and then <strong>"I'm not a robot" Checkbox</strong> sub-option from the <strong>reCAPTCHA Type</strong> list.</li>
    <li>Enter the domain of your web application, e.g. example.com. If you are creating this key for your localhost, just enter localhost. You can enter more than one domain which is useful if you want the key to work across different hosts.</li>
    <li>Accept the reCAPTCHA terms of service.
    <li>Click on the <strong>Submit</strong> button.
    <li>Copy your <strong>Site Key</strong> and <strong>Secret Key</strong> which you would need to specify in your application's web.config file.</li>
</ol>
<h2>Installation</h2>
<h3>reCAPTCHA Nuget Package</h3>
<p>The best and the recommended way to install the latest version of reCAPTCHA for .NET is through Nuget. From the <a href="http://docs.nuget.org/consume/package-manager-console">Nuget's Package Manager Console</a> in your Visual Studio .NET IDE, simply execute the following command:</p>
<pre><code>PM&gt; Install-Package RecaptchaNet</code></pre>
<h3>Latest Release</h3>
<p>You can also download a released build of reCAPTCHA for .NET by going to the <a href="https://github.com/tanveery/recaptcha-net/releases">Releases</a> section of this project. The latest release is <a href="https://github.com/tanveery/recaptcha-net/releases/tag/v2.0">reCAPTCHA for .NET v2.0</a>.</p>
<h2>Issues</h2>
If you find a bug in the library or you have an idea about a new feature, please try to search in the existing list of <a href="https://github.com/tanveery/recaptcha-net/issues">issues</a>. If the bug or idea is not listed and addressed there, please <a href="https://github.com/tanveery/recaptcha-net/issues/new">open a new issue</a>.
<h2>reCAPTCHA for .NET Reference</h2>
<h3>Attributes</h3>
<p>The attributes are used to control the behavior and appearance of the reCAPTCHA widget. They are specified in one of the three ways:</p>
<ul>
    <li>As API parameters (ASP.NET MVC and ASP.NET Core helper methods)</li>
    <li>As properties of a web control (ASP.NET Web Control)</li>
    <li>Configuration (web.config / appsettings.json)
</ul>
<p>Assigning a value through method or property takes precedence over configuration. Of course, you don't need to set any attribute anywhere unless its requried. The following is the entire list of the attributes:</p>
<table>
    <tr>
        <th>Attribute</th>
        <th>Description</th>
        <th>Type</th>
        <th>Values</th>
        <th>Default Value</th>
        <th>Configuration Key</th>
        <th>Required</th>
    </tr>    
    <tr>
        <td><strong>Site Key</strong></td>
        <td>Site key for reCAPTCHA. It is required for rendering the widget.</td>
        <td><code>String</code></td>        
        <td><em>The site key associated with the site you register in <a href="https://www.google.com/recaptcha/admin">Google reCAPTCHA Admin Console</a>.</em></td>
        <td><em>No default value. Must be provided.</em</td>
        <td><code>RecaptchaSiteKey</td>
        <td>Yes</td>
    </tr>    
    <tr>
        <td><strong>Secret Key</strong></td>
        <td>Secret key for the reCAPTCHA. It is required for verifying reCAPTCHA response.</td>
        <td><code>String</code></td>
        <td><em>The secret key associated with the site you register in <a href="https://www.google.com/recaptcha/admin">Google reCAPTCHA Admin Console</a>.</em></td>
        <td><em>No default value. Must be provided.</em</td>
        <td><code>RecaptchaSecretKey</td>
        <td>Yes</td>
    </tr>      
    <tr>
        <td><strong>APIVersion</strong></td>
        <td>Determines the version of the reCAPTCHA API.</td>
        <td><code>String</code></td>
        <td>-</td>
        <td>2</td>
        <td><code>RecaptchaApiVersion</td>
        <td>No</td>
    </tr>      
    <tr>
        <td><strong>Language</strong></td>
        <td>Forces the reCAPTCHA widget to render in a specific language. By default, the user's language is used.</td>
        <td><code>String</code></td>
        <td><em>One of the values from the <a href="https://developers.google.com/recaptcha/docs/language">Language Codes</a> list.</em></td>
        <td><em>User's language</em></td>
        <td><code>RecaptchaLanguage</code></td>
        <td>No</td>
    </tr>    
    <tr>
        <td><strong>Size</strong></td>
        <td>The size of the reCAPTCHA widget.</td>
        <td><code>RecaptchaSize</code> enum</td>
        <td><code>Default</code>, <code>Normal</code>, <code>Compact</code></td>
        <td><code>Default</code></td>
        <td><code>RecaptchaSize</code></td>
        <td>No</td>
    </tr>   
    <tr>
        <td><strong>TabIndex</strong></td>
        <td>The tabindex of the reCAPTCHA widget.</td>
        <td><code>Int32</code></td>
        <td><em>Any integer</em></td>
        <td>0</td>
        <td>-</td>
        <td>No</td>
    </tr>     
    <tr>
        <td><strong>Theme</strong></td>
        <td>The ccolor theme of the reCAPTCHA widget.</td>
        <td><code>RecaptchaTheme</code> enum</td>
        <td><code>Default</code>, <code>Light</code>, <code>Dark</code></td>
        <td><code>Default</code></td>
        <td><code>RecaptchaTheme</code></td>
        <td>No</td>
    </tr>
    <tr>
        <td><strong>Use SSL</strong></td>
        <td>Determines if SSL is to be used in Google reCAPTCHA API calls.</td>
        <td><code>RecaptchaSslBehavior</code> enum</td>
        <td><code>AlwaysUseSsl</code>, <code>SameAsRequestUrl</code>, <code>DoNotUseSsl</code></td>
        <td><code>AlwaysUseSsl</code></td>
        <td><code>RecaptchaUseSsl</code></td>
        <td>No</td>
    </tr>      
</table>
<h2>How to Use reCAPTHCA for .NET Library</h2>
<table>
    <tr>
        <th style="width:20%">Use Case</th>
        <th style="width:20%">Description</th>
        <th style="width:30%">ASP.NET Web Form</th>
        <th style="width:30%">ASP.NET MVC 5 and ASP.NET Core</th>
    </tr>
    <tr>
        <td><strong>Render reCAPTCHA widget</strong></td>
        <td>Render reCAPTCHA widget along with the API script.</td>
        <td>
            <p>Use the Recaptcha.Web.UI.RecaptchaWidget web control in your web form.</p>
            <p><strong>Example</strong><br>
            <pre><code>&lt;%@ Register Assembly="Recaptcha.Web" Namespace="Recaptcha.Web.UI.Controls"
TagPrefix="cc1" %&gt;
&lt;cc1:Recaptcha ID="Recaptcha1" runat="server" /&gt;
</code></pre></p>
        </td>
        <td>Call RecpatchaWidget method of HTML helper class in your view.</td>
    </tr>  
    <tr>
        <td><strong>Render widget without API script</strong></td>
        <td>Render reCAPTCHA widget without the API script. This is useful when you want to render multiple reCAPTCHA widgets or your you want to render the API script in a specific location of your page.</td>
        <td>Use the Recaptcha.Web.UI.RecaptchaWidget web control in your web form and set its RenderApiScript to false.</td>
        <td>Call RecpatchaWidget method of HTML helper class in your view and pass renderApiScript argument as false.</td>
    </tr>    
</table>
<h2>Quick Starter</h2>
<h3>How to Use reCAPTCHA in an ASP.NET Web Forms Application</h3>
<p>Add the following line just under the Page directive in your .aspx or .ascx file:</p>
<pre><code>&lt;%@ Register Assembly="Recaptcha.Web" Namespace="Recaptcha.Web.UI.Controls"
TagPrefix="cc1" %&gt;
</code></pre>
<p>Then at the desired line in the same file add the reCAPTCHA control as follows:</p>
<pre><code>&lt;cc1:Recaptcha ID="Recaptcha1" SiteKey="Your site key"
PrivateKey="Your secret key" runat="server" /&gt;
</code></pre>
<p>Rather than setting the recaptcha key of the control through its PublicKey and PrivateKey properties, you can set them in your web.config file instead:</p>
<p><a href="#keyInWebConfig">How to Set Recaptcha Key in Web.config File</a></p>
<h3>How to Set Recaptcha Key in Web.config File</h3>
<p>After you set the private and public keys in your web.config file, all you need in your web form is this following piece of code:</p>
<pre><code>&lt;cc1:Recaptcha ID="Recaptcha1" runat="server" /&gt;
</code></pre>
<p>By default, the theme of the reCAPTCHA control is Light. However, you can change this default theme to one of the another value like Dark. Theme can be set by using the <strong>RecaptchaTheme</strong> enum. The following is an example:</p>
<pre><code>&lt;cc1:Recaptcha ID="Recaptcha1" Theme="RecaptchaTheme.Dark" runat="server" /&gt;
</code></pre>
<h4>Add the reCAPTCHA Control to the Visual Studio Toolbox</h4>
<p>Instead of writing the above code manually, you can easily drag and drop the same reCAPTCHA control from the Visual Studio Toolbox onto your page designer just like the way you would do for other standard ASP.NET controls. However, you would need to add the reCAPTCHA control to the Toolbox first. Simply, right click on the Toolbox and select Choose Items... from the context menu and then under the .NET Framework Components tab click on the Browse button and locate the <strong>Recaptcha.Web.dll</strong> assembly.</p>
<h3>Verify User's Response to reCAPTCHA Challenge</h3>
<p>When your end-user submits the form that contains the reCAPTCHA control, you obviously would want to verify whether the user's answer was valid based on what was displayed in the recaptcha image. It is very easy to do with one or two lines.</p>
<p>First of all as expected, import the namespace <strong>Recaptcha.Web</strong> in your code-behind file:</p>
<pre><code>using Recaptcha.Web;
</code></pre>
<p>To verify whether the user's answer is correct, call the control's <strong>Verify()</strong> method which returns RecaptchaVerificationResult. You can also use the control's <strong>Response</strong> property to check what the actual answer is. Generally, you would want to use the Response property to check if the user provided a blank response which of course is always wrong:</p>
<pre><code class="language-cs">if (String.IsNullOrEmpty(Recaptcha1.Response))
{
    lblMessage.Text = "Captcha cannot be empty.";
}
else
{
    var result = Recaptcha1.Verify();
    if (result.Success)
    {
        Response.Redirect("Welcome.aspx");
    }
    else
    {
        lblMessage.Text = "Error(s): ";
        foreach(var err in result.ErrorCodes)
        {
            lblMessage.Text = lblMessage.Text + err;
        }
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
    var result = await Recaptcha1.VerifyTaskAsync();
    if (result.Success)
    {
        Response.Redirect("Welcome.aspx");
    }
    else
    {
        lblMessage.Text = "Error(s): ";
        foreach(var err in result.ErrorCodes)
        {
            lblMessage.Text = lblMessage.Text + err;
        }
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
RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
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
<p>As you may have already seen, you can directly assign site and secret keys to the respective properties of Recaptcha ASP.NET control or reCAPTCHA MVC HTML extension. However, a better way is to store these keys in your web.config file. The obvious benefit is that you can change these keys anytime you want without requiring you to modify your code and perhaps most important benefit is that you the keys you define in your web.config are global in your web project.</p>
<p>In the appSettings section of your web.config file, add the keys as follows:</p>
<pre><code>&lt;appSettings&gt;
&lt;add key="RecaptchaSiteKey" value="Your site key" /&gt;
&lt;add key="RecaptchaSecretKey" value="Your secret key" /&gt;
&lt;/appSettings&gt;
</code></pre>
<p><strong>Note</strong>: The <strong>appSettings</strong> keys are automatically added to your web.config file if you install reCAPTCHA for .NET through Nuget. However, you would still need to provide your own site and secret keys in the web.config file of your project.</p>
