using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using RpgMvc.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RpgMvc.Controllers
{
    public class UsuariosController : Controller
    {
        public string uriBase = "http://gdias.somee.com/RpgApi/Usuarios/";



        [HttpGet]
        public ActionResult Index()
        {
            return View("CadastrarUsuario");
        }


        [HttpPost]
        public async Task<ActionResult> RegistrarAsync(UsuarioViewModel u)
        {
            HttpClient httpClient = new HttpClient();
            string uriComplementar = "Registrar";

            var content = new StringContent(JsonConvert.SerializeObject(u));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uriBase + uriComplementar, content);

            string serialized = await response.Content.ReadAsStringAsync();

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["Mensagem"] =
                    string.Format("Usuário {0} registrado com sucesso! Faça o login para acessar.", u.Username);

                    return View("AutenticarUsuario");
            }
            else
            {
                TempData["MensagemErro"] = serialized;
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public ActionResult IndexLogin()
        {
            return View("AutenticarUsuario");
        }


        [HttpPost]
        public async Task<ActionResult> AutenticarAsync(UsuarioViewModel u)
        {
            HttpClient httpClient = new HttpClient();
            string uriComplementar = "Autenticar";

            var content = new StringContent(JsonConvert.SerializeObject(u));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uriBase + uriComplementar, content);

            string serialized = await response.Content.ReadAsStringAsync();

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
               HttpContext.Session.SetString("SessionTokenUsuario", serialized);
               TempData["Mensagem"] = string.Format("Bem-vindo {0}!!!", u.Username);
               
               return RedirectToAction("Index", "Personagens");
               //return IndexLogin();
            }
            else
            {
                TempData["MensagemErro"] = serialized;
                return IndexLogin(); //retornar para o index/view
            }
        }



    }
}