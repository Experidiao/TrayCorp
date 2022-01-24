﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrayCorp.Domain.Interfaces
{
    public interface IRepository<T>  where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int Id);
        Task<int> Create(T entidade);
        Task<bool> Delete(int Id);
        Task<bool> Update(T entidade);
    }
}
