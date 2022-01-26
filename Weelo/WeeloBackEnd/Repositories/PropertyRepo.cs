using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloBackEnd.Context;
using WeeloBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace WeeloBackEnd.Repositories
{
    public class PropertyRepo
    {
        Weelo_DatabaseContext _db;

        public PropertyRepo(String connectionString)
        {
            _db = new Weelo_DatabaseContext(connectionString);
        }

        public async Task<List<Property>> GetProperties()
        {
            var properties = _db.Properties.ToList();
            List<PropertyImage> propertyImage = new List<PropertyImage>();
            Owner owner = new Owner();
            if (properties != null)
            {
                foreach (var prop in properties)
                {
                    owner = _db.Owners.Where(o => o.IdOwner == prop.IdOwner).Select(o => new Owner {
                        IdOwner = o.IdOwner,
                        Name = o.Name,
                        Address = o.Address,
                        Photo = o.Photo,
                        Birthday = o.Birthday
                    }).FirstOrDefault();
                    propertyImage = _db.PropertyImages.Where(p => p.IdProperty == prop.IdProperty).Select(p => new PropertyImage
                    {
                        IdPropertyImage = p.IdPropertyImage,
                        IdProperty = prop.IdProperty,
                        File = p.File,
                        Enabled = p.Enabled
                    }).ToList();
                    prop.PropertyImages = propertyImage;
                    prop.IdOwnerNavigation = owner;
                }
            }
            
            return properties;
        }

        public async Task<bool> UpdateProperties(Property property)
        {
            bool response = false;
            try
            {
                var propertyExists = _db.Properties.Where(p => p.IdProperty == property.IdProperty).FirstOrDefault();
                if (propertyExists != null)
                {
                    propertyExists.IdOwner = property.IdOwner;
                    propertyExists.Name = property.Name;
                    propertyExists.Address = property.Address;
                    propertyExists.Price = property.Price;
                    propertyExists.CodeInternal = property.CodeInternal;
                    propertyExists.Year = property.Year;

                    _db.SaveChanges();
                }
                else
                {
                    Property propertySet = new Property
                    {
                        IdOwner = property.IdOwner,
                        Name = property.Name,
                        Address = property.Address,
                        Price = property.Price,
                        CodeInternal = property.CodeInternal,
                        Year = property.Year
                    };
                    _db.Add(propertySet);
                    _db.SaveChanges();
                }
                response = true;
            }
            catch (Exception ex)
            {
                response = false;
            }

            return response;
        }

        public async Task<bool> DeleteProperty(int idProperty)
        {
            bool response = false;
            try
            {
                _db.PropertyImages.RemoveRange(_db.PropertyImages.Where(p => p.IdProperty == idProperty).ToList());
                _db.PropertyTraces.RemoveRange(_db.PropertyTraces.Where(p => p.IdProperty == idProperty).ToList());
                _db.Properties.Remove(_db.Properties.Where(p => p.IdProperty == idProperty).FirstOrDefault());
                _db.SaveChanges();
                response = true;
            }
            catch (Exception)
            {
                response =false;
            }

            return response;
        }

        
    }
}
