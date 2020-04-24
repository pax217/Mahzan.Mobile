using Mahzan.SqLite.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.SqLite.Interfaces
{
    public interface IAspNetUsersRepository
    {
        Task<IEnumerable<AspNetUsers>> GetAsync();
        Task<AspNetUsers> GetByIdAsync();
        Task<bool> InsertAsync(AspNetUsers aspNetUsers);
        Task<bool> UpdateAsync(AspNetUsers aspNetUsers);
        Task<bool> DeleteAsync(AspNetUsers aspNetUsers);
        Task<IEnumerable<AspNetUsers>> QueryAsync(Func<AspNetUsers, bool> func);
    }
}
