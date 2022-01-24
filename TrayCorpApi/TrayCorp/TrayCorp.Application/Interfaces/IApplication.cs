using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace TrayCorp.Application.Interfaces
{
    public interface IApplication<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<int> Create(T entidade);
        Task<bool> Delete(int Id);
        Task<bool> Update(T entidade);

    }
}
