using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RpgMvc.Models;

namespace RpgMvc.Controllers
{
    public class DisputasController : Controller
    {
        public string uriBase = "http://gdias.somee.com/RpgApi/Disputas/"; //chamar somee

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            //(A) Criação da variável http e obtenção do token guardado na session
            HttpClient httpClient = new HttpClient();

            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //(B) Definição da rota da API que buscará a lista de personagens na API, retornando uma lista se o método
            string uriBuscaPersonagens = "http://gdias.somee.com/RpgApi/Personagem/GetAll";

            HttpResponseMessage response = await httpClient.GetAsync(uriBuscaPersonagens);

            string serialized = await response.Content.ReadAsStringAsync();

            /*(C) Se o status da requisição for 200 (Ok) então deserializamos para transformar numa lista de
            personagens, e depois geramos duas ViewBags a partir da lista de personagens, uma como os
            atacantes e outra como os oponentes. ViewsBags são maneiras de trafegar dados entre a controller e
            views e isso é que fará o carregamento do dropdownlist aparecer.*/

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<PersonagemViewModel> listaPersonagens = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));
                ViewBag.ListaAtacantes = listaPersonagens;
                ViewBag.ListaOponentes = listaPersonagens;
            }
            else
                TempData["MensagemErro"] = serialized; //(D) Mensagem de erro sendo enviado através do TempData. TempData é outra maneira de trafegar dados entre uma controller e uma View.
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> IndexAsync(DisputaViewModel disputa)
        {
            HttpClient httpClient = new HttpClient();
            
            string uriComplementar = "Arma";
            
            var content = new StringContent(JsonConvert.SerializeObject(disputa));
           
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            HttpResponseMessage response = await httpClient.PostAsync(uriBase + uriComplementar, content);
            
            string serialized = await response.Content.ReadAsStringAsync();
            
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                disputa = await Task.Run(() =>
                JsonConvert.DeserializeObject<DisputaViewModel>(serialized));
                TempData["Mensagem"] = disputa.Narracao;
            }
            else
                TempData["MensagemErro"] = serialized;
           
            return RedirectToAction("Index", "Personagens");
        }


        [HttpGet]
        public async Task<ActionResult> IndexHabilidadesAsync()
        {
            //(A) Criação da variável http e obtenção do token guardado na session
            HttpClient httpClient = new HttpClient();

            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //(B) Definição da rota da API que buscará a lista de personagens na API, retornando uma lista se o método
            string uriBuscaPersonagens = "http://gdias.somee.com/RpgApi/Personagem/GetAll";

            HttpResponseMessage response = await httpClient.GetAsync(uriBuscaPersonagens);

            string serialized = await response.Content.ReadAsStringAsync();

            /*(C) Se o status da requisição for 200 (Ok) então deserializamos para transformar numa lista de
            personagens, e depois geramos duas ViewBags a partir da lista de personagens, uma como os
            atacantes e outra como os oponentes. ViewsBags são maneiras de trafegar dados entre a controller e
            views e isso é que fará o carregamento do dropdownlist aparecer.*/

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<PersonagemViewModel> listaPersonagens = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));
                ViewBag.ListaAtacantes = listaPersonagens;
                ViewBag.ListaOponentes = listaPersonagens;
            }
            else
                TempData["MensagemErro"] = serialized; //(D) Mensagem de erro sendo enviado através do TempData. TempData é outra maneira de trafegar dados entre uma controller e uma View.


            string uriBuscaHabilidades = "http://gdias.somee.com/RpgApi/PersonagemHabilidades/GetHabilidades";

            response = await httpClient.GetAsync(uriBuscaHabilidades);

            serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<HabilidadeViewModel> listaHabilidades = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<HabilidadeViewModel>>(serialized));
                ViewBag.ListaHabilidades = listaHabilidades;
            }
            else
                TempData["MensagemErro"] = serialized;

            return View("IndexHabilidades");
        }

    [HttpPost]
        public async Task<ActionResult> IndexHabilidadesAsync(DisputaViewModel disputa)
        {
            HttpClient httpClient = new HttpClient();

            string uriComplementar = "Habilidades";

            var content = new StringContent(JsonConvert.SerializeObject(disputa));

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uriBase + uriComplementar, content);

            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                disputa = await Task.Run(() =>
                JsonConvert.DeserializeObject<DisputaViewModel>(serialized));
                TempData["Mensagem"] = disputa.Narracao;
            }
            else
                TempData["MensagemErro"] = serialized;

            return RedirectToAction("Index", "Personagens");
        }

    [HttpGet]
        public async Task<ActionResult> DisputaGeralAsync()
        {
            HttpClient httpClient = new HttpClient();

            string token = HttpContext.Session.GetString("SessionTokenUsuario");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            string uriBuscaPersonagens = "http://gdias.somee.com/RpgApi/Personagem/GetAll";
            
            HttpResponseMessage response = await httpClient.GetAsync(uriBuscaPersonagens);
            
            string serialized = await response.Content.ReadAsStringAsync();
            List<PersonagemViewModel> listaPersonagens = await Task.Run(() =>
                JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));
           
            string uriDisputa = "http://gdias.somee.com/RpgApi/Disputas/DisputaEmGrupo";
            
            DisputaViewModel disputa = new DisputaViewModel();
            disputa.ListaIdPersonagens = new List<int>();
            disputa.ListaIdPersonagens.AddRange(listaPersonagens.Select(p => p.Id));
           
            var content = new StringContent(JsonConvert.SerializeObject(disputa));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            response = await httpClient.PostAsync(uriDisputa, content);
            serialized = await response.Content.ReadAsStringAsync();
           
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                disputa = await Task.Run(() =>
                    JsonConvert.DeserializeObject<DisputaViewModel>(serialized));
                TempData["Mensagem"] = string.Join(" - ", disputa.Resultados);
            }
            else
                TempData["MensagemErro"] = serialized;

            return RedirectToAction("Index", "Personagens");
        }


    }
}