using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloBackEnd.Context;
using WeeloBackEnd.Models;

namespace WeeloBackEnd.Repositories
{
    public class OwnerRepo
    {
        Weelo_DatabaseContext _db;
        public OwnerRepo(String connectionString)
        {
            _db = new Weelo_DatabaseContext(connectionString);
        }

        public async Task<List<Owner>> GetOwners()
        {
            return _db.Owners.ToList();
        }
    }
}
