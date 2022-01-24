using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using TrayCorpWeb.Models;
//using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace TrayCorpWeb.Controllers
{
    public class ProdutoController : Controller
    {
        string EndpointBase = "https://localhost:44369/api/";

        public async Task<ActionResult> Index(string ordenarPor, string valorPesquisa = "", string campoPesquisa = "")
        {
            ordenarPor = String.IsNullOrEmpty(ordenarPor) ? "Nome" : ordenarPor;


            // montar caixa com os campos para procura
            IReadOnlyDictionary<string, string> CampoPesquisa = new Dictionary<string, string>
            {
                {"Nome","Nome" },
            };

            ViewBag.campoPesquisa = new SelectList(CampoPesquisa, "Key", "Value");

            List<Produto> produto = new List<Produto>();

            // traz todos os registros da base
            valorPesquisa = string.IsNullOrEmpty(valorPesquisa) ? "%20":valorPesquisa;
            ordenarPor = string.IsNullOrEmpty(ordenarPor) ? "%20" : ordenarPor;

            produto = await lerListaProdutoApi(EndpointBase + "Produto/ProcurarProduto" + "/" +valorPesquisa+ "/" + ordenarPor);



            // Caso a base de dados esteja zerado, incluir 5 registros
           // produto = await lerListaProdutoApi(EndpointBase+"Produto");
            if (produto == null || produto.Count == 0)
            {
                var Produto1 = new Produto() {Nome = "Produto1", Estoque = 10, Valor = 10 };
                await IncluirRegistro(Produto1);
                var Produto2 = new Produto() {Nome = "Produto2", Estoque = 11, Valor = 11 };
                await IncluirRegistro(Produto2);
                var Produto3 = new Produto() {Nome = "Produto3", Estoque = 12, Valor = 12 };
                await IncluirRegistro(Produto3);
                var Produto4 = new Produto() {Nome = "Produto4", Estoque = 13, Valor = 13 };
                await IncluirRegistro(Produto4);
                var Produto5 = new Produto() {Nome = "Produto5", Estoque = 14, Valor = 14 };
                await IncluirRegistro(Produto5);
            }

            return View(produto);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("IdProduto,Nome,Estoque,Valor")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    // faz a chamada do endpoint
                    var endPoint = EndpointBase + "Produto/";
                    var jsonString = JsonConvert.SerializeObject(produto);
                    HttpContent httpContent = new StringContent(jsonString);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage ResultadoRequisicao = await client.PostAsync(endPoint, httpContent);
                }

                return RedirectToAction("Index");
            }
            return View(produto);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            // executar API
            var produto = await lerProdutoApi(EndpointBase + "Produto/" + id);


            if (produto == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return View(produto);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var produto = await lerProdutoApi(EndpointBase + "Produto/" + id);

            if (produto == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("IdProduto,Nome,Estoque,Valor")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    // faz a chamada do endpoint
                    var endPoint = EndpointBase + "Produto";
                    var jsonString = JsonConvert.SerializeObject(produto);
                    HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpResponseMessage ResultadoRequisicao = await client.PutAsync(endPoint, httpContent);
                }

                return RedirectToAction("Index");
            }
            return View(produto);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var produto = await lerProdutoApi(EndpointBase + "Produto/" + id);

            if (produto == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                // faz a chamada do endpoint
                var endPoint = EndpointBase + "Produto/" + id;

                client.BaseAddress = new Uri(endPoint);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResultadoRequisicao = await client.DeleteAsync(endPoint);
            }

            return RedirectToAction("Index");
        }


        public async Task<bool> IncluirRegistro(Produto produto)
        {
            bool resultado = true;
            using (var client = new HttpClient())
            {
                // faz a chamada do endpoint
                var endPoint = EndpointBase + "Produto/";
                var jsonString = JsonConvert.SerializeObject(produto);
                HttpContent httpContent = new StringContent(jsonString);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage ResultadoRequisicao = await client.PostAsync(endPoint, httpContent);
                if (ResultadoRequisicao.IsSuccessStatusCode)
                {
                    resultado = false;
                }
            }
            return resultado;
        }

        public async Task<List<Produto>> lerListaProdutoApi(string endPoint)
        {
            List<Produto> produto = new List<Produto>();

            using (var client = new HttpClient())
            {
                // faz a chamada do endpoint, traz um lista de objetos
                client.BaseAddress = new Uri(endPoint);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResultadoRequisicao = await client.GetAsync(endPoint);
                if (ResultadoRequisicao.IsSuccessStatusCode)
                {
                    var resposta = await ResultadoRequisicao.Content.ReadAsStringAsync();
                    produto = JsonConvert.DeserializeObject<List<Produto>>(resposta);
                }
            }
            return produto;
        }

        public async Task<Produto> lerProdutoApi(string endPoint)
        {
            Produto produto = new Produto();

            using (var client = new HttpClient())
            {
                // faz a chamada do endpoint, e traz apenas um objeto
                client.BaseAddress = new Uri(endPoint);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResultadoRequisicao = await client.GetAsync(endPoint);
                if (ResultadoRequisicao.IsSuccessStatusCode)
                {
                    var resposta = await ResultadoRequisicao.Content.ReadAsStringAsync();
                    produto = JsonConvert.DeserializeObject<Produto>(resposta);
                }
            }
            return produto;
        }


    }
}
