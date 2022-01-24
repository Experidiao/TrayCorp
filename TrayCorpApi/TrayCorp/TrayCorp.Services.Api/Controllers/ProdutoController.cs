using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrayCorp.Application.Interfaces;
using TrayCorp.Application.DTO;
using TrayCorp.Domain.Models;
using TrayCorp.Services.Api.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrayCorp.Services.Api.Controllers
{
    public class ProdutoController : ApiController
    {
        private readonly IProdutoApplication _produtoApplication;
        private readonly IMapper _mapper;
        public ProdutoController(IProdutoApplication produtoApplication, IMapper mapper)
        {
            _produtoApplication = produtoApplication;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Produto>> Get()
        {
            return await _produtoApplication.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("{IdProduto:int}")]
        public async Task<IActionResult> GetById(int IdProduto)
        {
            var resultado = await _produtoApplication.GetById(IdProduto);
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoDTO produtoDTO)
        {
            var uv = new ProdutoValidation();
            int resposta = 0;

            try
            {
                ValidationResult results = uv.Validate(_mapper.Map<Produto>(produtoDTO));
                if (!results.IsValid) return CustomResponse(results);

                resposta = await _produtoApplication.Create(_mapper.Map<Produto>(produtoDTO));
                if (resposta == 0)
                {
                    results.Errors.Add(new ValidationFailure("", "Erroa ao gravar os dados do Produto"));
                    return CustomResponse(results);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "1", result = ex.Message });
            }

            return Ok(new { error = "0", result = resposta });
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<bool> Put([FromBody] ProdutoDTO produtoDTO)
        {
            return await _produtoApplication.Update(_mapper.Map<Produto>(produtoDTO));
        }

        [AllowAnonymous]
        [HttpDelete("{IdProduto:int}")]
        public async Task<bool> Delete(int IdProduto)
        {
            return await _produtoApplication.Delete(IdProduto);
        }


        [AllowAnonymous]
        [HttpGet("ObterProdutoPorNome/{nome}")]
        public async Task<IActionResult> ObterProdutoPorNome(string nome)
        {
            List<Produto> resultado = new List<Produto>();
            string erro = "0";
            try
            {
                resultado = await _produtoApplication.ObterProdutoPorNome(nome);
            }
            catch (Exception ex)
            {
                erro = "1";
                return Ok(new { error = erro, result = ex.Message });

            }
            return Ok(resultado);
        }

        [HttpGet("ProcurarProduto/{ProcurarPor}/{ordenarPor}")]
        public async Task<IActionResult> ProcurarProduto(string ProcurarPor, string ordenarPor)
        {
            List<Produto> resultado = new List<Produto>();
            string erro = "0";
            try
            {
                resultado = await _produtoApplication.ProcurarProduto(ProcurarPor, ordenarPor);
            }
            catch (Exception ex)
            {
                erro = "1";
                return Ok(new { error = erro, result = ex.Message });

            }
            return Ok(resultado);
        }

    }
}
