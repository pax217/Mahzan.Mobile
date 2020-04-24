using Mahzan.SqLite.Entities;
using Mahzan.SqLite.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.SqLite.Implementations
{
    public class AspNetUsersRepository : IAspNetUsersRepository
    {
        public Task<bool> DeleteAsync(AspNetUsers aspNetUsers)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AspNetUsers>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AspNetUsers> GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(AspNetUsers aspNetUsers)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AspNetUsers>> QueryAsync(Func<AspNetUsers, bool> func)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(AspNetUsers aspNetUsers)
        {
            throw new NotImplementedException();
        }
    }
}
