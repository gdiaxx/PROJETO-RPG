

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
    public class ArmasController : Controller
    {
        public string uriBaseArma = "http://gdias.somee.com/RpgApi/Armas/";


         public async Task<IActionResult> IndexAsync()
        {
            string uriComplementar = "GetAll";
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(uriBaseArma + uriComplementar);
            
            string serialized = await response.Content.ReadAsStringAsync();

            List<ArmaViewModel> listaArmas = await Task.Run(() => 
                JsonConvert.DeserializeObject<List<ArmaViewModel>>(serialized));

            return View(listaArmas);
        }
        
[HttpPost] //criar arma
        public async Task<ActionResult> CreateAsync(ArmaViewModel a)
        {
            HttpClient httpClient = new HttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(a));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uriBaseArma, content);

            string serialized = await response.Content.ReadAsStringAsync();

            await Task.Run(() => 
                JsonConvert.DeserializeObject<List<ArmaViewModel>>(serialized));

            TempData["Mensagem"] = string.Format("Arma {0} salva com sucesso!", a.Nome);

            return RedirectToAction("Index");
        }

[HttpGet] //get do create
        public async Task<ActionResult> Create()
        {
            HttpClient httpClient = new HttpClient();

            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string uriBuscarPersonagem = "http://gdias.somee.com/RpgApi/Personagem/GetAll";
            HttpResponseMessage response = await httpClient.GetAsync(uriBuscarPersonagem);

            string serialized = await response.Content.ReadAsStringAsync();
            List<PersonagemViewModel> p = await Task.Run(() => 
                    JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));

            ViewBag.Lista1 = p;

            return View();
            
        }

[HttpGet] //detalhes da arma
        public async Task<ActionResult> DetailsAsync(int id)
        {
            string uriComplementar = string.Format("GetId/{0}", id);
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(uriBaseArma + uriComplementar);

            string serialized = await response.Content.ReadAsStringAsync();

            ArmaViewModel a = await Task.Run(() => 
                JsonConvert.DeserializeObject<ArmaViewModel>(serialized));

            return View(a);
        }

[HttpGet] //get edit
        public async Task<ActionResult> EditAsync(int id)
        {

            HttpClient httpClient = new HttpClient();

            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
           
            string uriBuscarPersonagem = "http://gdias.somee.com/RpgApi/Personagem/GetAll";
            HttpResponseMessage response1 = await httpClient.GetAsync(uriBuscarPersonagem);
            
            string serialized = await response1.Content.ReadAsStringAsync();
           
           List<PersonagemViewModel> p = await Task.Run(() => 
                    JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));
            
            ViewBag.Lista2 = p;

           string uriComplementarArma = string.Format("GetId/{0}", id);
           HttpResponseMessage response = await httpClient.GetAsync(uriBaseArma + uriComplementarArma);

           string serializedArma = await response.Content.ReadAsStringAsync();
            
            ArmaViewModel a = await Task.Run(() => 
                    JsonConvert.DeserializeObject<ArmaViewModel>(serializedArma));

            return View(a);
        }
       
        
[HttpPost] //criar edição
        public async Task<ActionResult> EditAsync(ArmaViewModel a)
        {
            HttpClient httpClient = new HttpClient();

            string token = HttpContext.Session.GetString("SessionTokenUsuario");
           
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(a));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string uriComplementar2 = string.Format("Put");
            HttpResponseMessage response = await httpClient.PutAsync(uriBaseArma + uriComplementar2, content);

            TempData["Mensagem"] = string.Format("Arma {0} atualizada com sucesso!", a.Nome);

             return RedirectToAction("Index");
        }

[HttpGet] //deletar arma
        public async Task<ActionResult> DeleteAsync(int id)
        {
            string uriComplementar = string.Format("DeleteId/{0}", id);
           
            HttpClient httpClient = new HttpClient();

            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);
            
            HttpResponseMessage response = await httpClient.DeleteAsync(uriBaseArma + uriComplementar);

            TempData["Mensagem"] = string.Format("Arma Id {0} removida com sucesso!", id);
            return RedirectToAction("Index");
        }
    }
}