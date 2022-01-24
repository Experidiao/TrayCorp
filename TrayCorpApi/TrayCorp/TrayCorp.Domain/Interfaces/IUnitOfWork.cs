using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrayCorp.Domain.Interfaces
{
    public interface IUnitOfWork 
    {
        Task<bool> Commit();
    }
}
