using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Data.Repositories.Interfaces
{
    public interface IStatusRepository
    {
        Task<Status> GetStatusById(int id);
    }
}