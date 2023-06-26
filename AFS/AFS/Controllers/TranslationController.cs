using AFS.Dtos;
using AFS.Models;
using AFS.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using Newtonsoft.Json;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AFS.Controllers
{
    [Authorize]
    public class TranslationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private ITranslationService _translationService;
        public TranslationController(ITranslationService translationService, IHttpClientFactory httpClientFactory)
        {
            _translationService = translationService;
            _httpClientFactory = httpClientFactory; 
        }
        public IActionResult Index()
        {
            var list = _translationService.List();
            
            return View(list);
        }

        [HttpPost]
        public IActionResult Index(TranslationListVm model)
        {
            var list = _translationService.List(model.Term);

            return View(list);
        }

        public IActionResult TranslateToElvish()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TranslateToElvish(Translation model)
        {
            
            //translation api call
            TranlationRequest text = new TranlationRequest() { 
            text= model.Text,
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(text);
            var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://api.funtranslations.com/translate/quenya.json";
            var client = _httpClientFactory.CreateClient();
   
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = data;
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                ApiResponse resp = JsonConvert.DeserializeObject<ApiResponse>(content);
                model.TextTranslation = resp.contents.translated;
                _translationService.Add(model);
            }
            else
            {
                TempData["msg"] = "Rate limit exceeded..";
            }
           
            client.Dispose();

            return View(model);
        }

    }
}
