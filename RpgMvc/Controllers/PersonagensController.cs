using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RpgMvc.Models;

namespace RpgMvc.Controllers
{
    public class PersonagensController : Controller
    {
        public string uriBase = "http://gdias.somee.com/RpgApi/Personagem/"; //chamar somee

        public async Task<ActionResult> IndexAsync() //pegar e listar todos os personagens
        {
            string uriComplementar = "GetAll";

            HttpClient httpClient = new HttpClient();

            string token = HttpContext.Session.GetString("SessionTokenUsuario"); //iniciar sessao com token
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar); //juncao para o link funcionar

            string serialized = await response.Content.ReadAsStringAsync(); //conteudo serializado

            List<PersonagemViewModel> listaPersonagens = await Task.Run(() =>
                JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized)); //deserializar objeto 
            //pegar lista de personagens

            return View(listaPersonagens);
        }

    [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
   
    [HttpPost]
        public async Task<ActionResult> CreateAsync(PersonagemViewModel p)
        {
            HttpClient httpClient = new HttpClient();
           
            string token = HttpContext.Session.GetString("SessionTokenUsuario");
           
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);
            
            var content = new StringContent(JsonConvert.SerializeObject(p));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
           
            HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
            
            string serialized = await response.Content.ReadAsStringAsync();
            
            await Task.Run(() =>
                JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));
            
            TempData["Mensagem"] = string.Format("Personagem {0} Salvo com sucesso!", p.Nome);
            return RedirectToAction("Index");
        }

    [HttpGet]
        public async Task<ActionResult> DetailsAsync(int id) //busca na api um personagem de acordo com o id
        {
            HttpClient httpClient = new HttpClient();
            
            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);
            
            HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());
            
            string serialized = await response.Content.ReadAsStringAsync();
            
            PersonagemViewModel p = await Task.Run(() =>
                JsonConvert.DeserializeObject<PersonagemViewModel>(serialized));
            
            return View(p);
        }

    [HttpGet]
        public async Task<ActionResult> EditAsync(int id)
        {
            HttpClient httpClient = new HttpClient();
           
            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);
           
            HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());
           
            string serialized = await response.Content.ReadAsStringAsync();
           
            PersonagemViewModel p = await Task.Run(() =>
                JsonConvert.DeserializeObject<PersonagemViewModel>(serialized));
            
            return View(p);
        }
        
    [HttpPost]
        public async Task<ActionResult> EditAsync(PersonagemViewModel p)
        {
            HttpClient httpClient = new HttpClient();
            
            string token = HttpContext.Session.GetString("SessionTokenUsuario");
           
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);
           
            var content = new StringContent(JsonConvert.SerializeObject(p));
           
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
           
            HttpResponseMessage response = await httpClient.PutAsync(uriBase, content);
           
            string serialized = await response.Content.ReadAsStringAsync();
           
            await Task.Run(() =>
                JsonConvert.DeserializeObject<PersonagemViewModel>(serialized));
           
            TempData["Mensagem"] =
                string.Format("Personagem {0}, classe {1} atualizado com sucesso!", p.Nome, p.Classe);
           
            return RedirectToAction("Index");
        }

    [HttpGet]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            HttpClient httpClient = new HttpClient();

            string token = HttpContext.Session.GetString("SessionTokenUsuario");

            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await httpClient.DeleteAsync(uriBase + id.ToString());

            TempData["Mensagem"] = string.Format("Personagem Id {0} removido com sucesso!", id);

            return RedirectToAction("Index");
        }
   
    }

}