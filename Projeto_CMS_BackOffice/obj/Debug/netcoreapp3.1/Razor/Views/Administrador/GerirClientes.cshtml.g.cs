#pragma checksum "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b6d2f2079fd20b781d3521f088bb7601ae488d5f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Administrador_GerirClientes), @"mvc.1.0.view", @"/Views/Administrador/GerirClientes.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\_ViewImports.cshtml"
using Projeto_CMS_BackOffice;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\_ViewImports.cshtml"
using Projeto_CMS_BackOffice.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b6d2f2079fd20b781d3521f088bb7601ae488d5f", @"/Views/Administrador/GerirClientes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2e9b2e183eb0482d4489a19e4be266a1cca6f229", @"/Views/_ViewImports.cshtml")]
    public class Views_Administrador_GerirClientes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Projeto_CMS_BackOffice.Cliente>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RemoverSuspensaoCliente", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SuspenderCliente", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-popup-content"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("autocomplete", new global::Microsoft.AspNetCore.Html.HtmlString("off"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-info"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
  
    ViewData["Title"] = "GerirClientes";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Gerir Clientes</h1>\r\n\r\n");
#nullable restore
#line 9 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
 if (ViewBag.Message != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div");
            BeginWriteAttribute("class", " class=\"", 174, "\"", 225, 4);
            WriteAttributeValue("", 182, "alert", 182, 5, true);
            WriteAttributeValue(" ", 187, "alert-", 188, 7, true);
#nullable restore
#line 11 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
WriteAttributeValue("", 194, ViewBag.Type, 194, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 207, "alert-dismissible", 208, 18, true);
            EndWriteAttribute();
            WriteLiteral(" role=\"alert\">\r\n        <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>\r\n        ");
#nullable restore
#line 13 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
   Write(ViewBag.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
#nullable restore
#line 15 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                Email\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 24 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
           Write(Html.DisplayNameFor(model => model.Telefone));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 27 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
           Write(Html.DisplayNameFor(model => model.Foto));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n\r\n            </th>\r\n            <th>\r\n\r\n            </th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 38 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 42 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
               Write(Html.DisplayFor(modelItem => item.IdCNavegation.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 45 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
               Write(Html.DisplayFor(modelItem => item.Telefone));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 1191, "\"", 1254, 2);
            WriteAttributeValue("", 1197, "http://127.0.0.1:2626/api/cliente/ImageGetUser/", 1197, 47, true);
#nullable restore
#line 48 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
WriteAttributeValue("", 1244, item.Foto, 1244, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" height=\"70\" style=\"margin-left: auto; margin-right: auto; display: block;\" />\r\n                </td>\r\n                <td>\r\n");
#nullable restore
#line 51 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
                     if (item.Suspenso == false)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <p style=\"color:forestgreen\">Conta Ativa</p>\r\n");
#nullable restore
#line 54 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
                    }
                    else if (item.Suspenso == true)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <p style=\"color:darkred\">Conta Suspensa</p>\r\n");
#nullable restore
#line 58 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\r\n                <td>\r\n");
#nullable restore
#line 61 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
                     if (item.Suspenso == false)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <a");
            BeginWriteAttribute("id", " id=\"", 1858, "\"", 1872, 1);
#nullable restore
#line 63 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
WriteAttributeValue("", 1863, item.IdC, 1863, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-danger\" onclick=\"openForm(this.id)\" style=\"cursor:pointer; color:white;\">Suspender</a>\r\n");
#nullable restore
#line 64 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b6d2f2079fd20b781d3521f088bb7601ae488d5f12105", async() => {
                WriteLiteral(" \r\n                    <input type=\"submit\" value=\"Desbloquear\" class=\"btn btn-warning\" onclick=\"return confirm(\'Tem a certeza que quer desbloquear o utilizador ?\');\" style=\"cursor:pointer; color:white;\"/>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-userId", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 67 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
                                                                 WriteLiteral(item.IdC);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["userId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-userId", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["userId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 70 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\r\n            </tr>\r\n");
#nullable restore
#line 73 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n<div class=\"form-popup\" id=\"suspenderForm\" style=\"display: none;\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b6d2f2079fd20b781d3521f088bb7601ae488d5f15404", async() => {
                WriteLiteral(@"
        <label><b>Suspender Cliente</b></label>
        <br />
        <input type=""text"" placeholder=""Motivo da suspensão"" name=""Motivo"" required style=""width: 40%;"">
        <input id=""userId"" type=""text"" placeholder=""Motivo"" name=""userId"" hidden>
        <br />
        <br />
        <input type=""text"" placeholder=""Número de dias"" name=""Dias"" required style=""width: 20%;"">
        <br />
        <span class=""close-form"" onclick=""closeForm()"">&times;</span> 
        <br />
        <input type=""submit"" value=""Suspender"" class=""btn btn-danger"" style=""width: 30%;"" />
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b6d2f2079fd20b781d3521f088bb7601ae488d5f17651", async() => {
                WriteLiteral("Voltar");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        function openForm(id) {
            document.getElementById(""suspenderForm"").style.display = ""block"";

            $(""#userId"").val(id);

        }

        function closeForm() {
            document.getElementById(""suspenderForm"").style.display = ""none"";
        }
    </script>
");
#nullable restore
#line 110 "D:\3 Ano\Projeto\Projeto\Projeto_CMS_BackOffice\Views\Administrador\GerirClientes.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Projeto_CMS_BackOffice.Cliente>> Html { get; private set; }
    }
}
#pragma warning restore 1591