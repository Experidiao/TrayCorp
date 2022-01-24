using AutoMapper;
using TrayCorp.Application.DTO;
using TrayCorp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrayCorp.Application.AutoMapper
{
    public class AutoMappingProfile :Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<ProdutoDTO, Produto>().ReverseMap();
        }
    }
}
