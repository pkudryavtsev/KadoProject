using System;
using ProductDb.DataClasses;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProductDb;
using Services.Dtos;
using AutoMapper;
using Services.Helpers;
using DAL;
using System.Linq.Expressions;
using System.Linq;
using DAL.Specifications;

namespace Services
{
    public class BoxService
    {
        private readonly Repo _repo;
        private readonly IMapper _mapper;

        public BoxService(IMapper mapper, Repo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }


    }
}
