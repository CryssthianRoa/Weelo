using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloBackEnd.Context;
using WeeloBackEnd.Models;

namespace WeeloBackEnd.Repositories
{
    public class PropertyImageRepo
    {
        Weelo_DatabaseContext _db;
        public PropertyImageRepo(String connectionString)
        {
            _db = new Weelo_DatabaseContext(connectionString);
        }

        public async Task<List<PropertyImage>> GetImages(int idProperty)
        {
            return _db.PropertyImages.Where(p => p.IdProperty == idProperty).ToList();
        }

        public async Task<bool> SetPhoto(int idProperty, string file)
        {
            bool response = false;
            try
            {
                PropertyImage propertyImage = new PropertyImage
                {
                    IdProperty = idProperty,
                    File = file,
                    Enabled = true
                };
                _db.Add(propertyImage);
                _db.SaveChanges();

                response = true;
            }
            catch (Exception ex)
            {
                response = false;
            }

            return response;
        }
    }
}
