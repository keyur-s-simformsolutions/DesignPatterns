using Throttling.Data;
using System;
using System.Threading.Tasks;

namespace Throttling.Core.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Hotel> Hotels { get; }
        Task Save();
    }
}
