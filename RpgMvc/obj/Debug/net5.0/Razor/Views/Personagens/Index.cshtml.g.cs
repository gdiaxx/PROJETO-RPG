#pragma checksum "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9e0025b691f59e98832dc703e17669e7e52dad09"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Personagens_Index), @"mvc.1.0.view", @"/Views/Personagens/Index.cshtml")]
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
#line 1 "C:\PROJETOS_WEB\RpgMvc\Views\_ViewImports.cshtml"
using RpgMvc;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\PROJETOS_WEB\RpgMvc\Views\_ViewImports.cshtml"
using RpgMvc.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9e0025b691f59e98832dc703e17669e7e52dad09", @"/Views/Personagens/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0ea8742186739939b51d95376aeaeffef3697b50", @"/Views/_ViewImports.cshtml")]
    public class Views_Personagens_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<RpgMvc.Models.PersonagemViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!--Namespace da classe de Modelo para esta view-->\r\n");
            WriteLiteral("\r\n\r\n<!--Configuração para exibir mensagem de sucesso -->\r\n");
#nullable restore
#line 6 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
 if (@TempData["Mensagem"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"alert alert-success\" role=\"alert\">\r\n        ");
#nullable restore
#line 9 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
   Write(TempData["Mensagem"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
#nullable restore
#line 10 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("<!--Configuração para exibir mensagem de erro -->\r\n");
#nullable restore
#line 12 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
 if (@TempData["MensagemErro"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"alert alert-danger\" role=\"alert\">\r\n        ");
#nullable restore
#line 15 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
   Write(TempData["MensagemErro"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
#nullable restore
#line 16 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 18 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
  ViewBag.Title = "Personagens"; 

#line default
#line hidden
#nullable disable
            WriteLiteral("<!--Título da página para o navegador-->\r\n<h2>Relação de Personagens</h2> <!--Título da página-->\r\n<p>\r\n\r\n    <!--Links apontando para views na mesma pasta-->\r\n    ");
#nullable restore
#line 23 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
Write(Html.ActionLink("Criar Novo Personagem", "Create"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</p>\r\n<table class=\"table\">\r\n    <tr>\r\n        <!--Títulos das colunas da tabela-->\r\n        <th>");
#nullable restore
#line 28 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n        <th>");
#nullable restore
#line 29 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n        <th>");
#nullable restore
#line 30 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.Classe));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n        <th>");
#nullable restore
#line 31 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.Disputas));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n        <th>");
#nullable restore
#line 32 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.Vitorias));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n        <th>");
#nullable restore
#line 33 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.Derrotas));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n        <th></th>\r\n    </tr>\r\n    <!--Looping para escrever os dados na tabela-->\r\n");
#nullable restore
#line 37 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 40 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 41 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 42 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Classe));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 43 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Disputas ));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 44 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Vitorias));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 45 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Derrotas));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>\r\n                <!--Coluna para Links/botões-->\r\n                ");
#nullable restore
#line 48 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
           Write(Html.ActionLink("Editar", "Edit", new { id = item.Id } ));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                ");
#nullable restore
#line 49 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
           Write(Html.ActionLink("Detalhes", "Details", new { id = item.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                ");
#nullable restore
#line 50 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
           Write(Html.ActionLink("Deletar", "Delete", new { id = item.Id }
                , new { onclick = "return confirm('Deseja realmente deletar?');"}));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n\r\n                ");
#nullable restore
#line 53 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
           Write(Html.ActionLink("Listar Habilidades", "Index", "PersonagemHabilidades", new {id = item.Id}));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                ");
#nullable restore
#line 54 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
           Write(Html.ActionLink("Atribuir Habilidade", "Create", "PersonagemHabilidades", new {id = item.Id, nome = item.Nome}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 57 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>\r\n\r\n");
#nullable restore
#line 60 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
Write(Html.ActionLink("Clique aqui para um embate com armas!", "Index", "Disputas",
null, new { @class = "btn btn-warning"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n ");
#nullable restore
#line 63 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
Write(Html.ActionLink("Clique aqui para um embate com habilidades!!!", "IndexHabilidades", "Disputas",
null, new { @class = "btn btn-dark" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 66 "C:\PROJETOS_WEB\RpgMvc\Views\Personagens\Index.cshtml"
Write(Html.ActionLink("Clique aqui para um embate em grupo!!!", "DisputaGeral", "Disputas",
null, new { @class = "btn btn-danger" }));

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<RpgMvc.Models.PersonagemViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
