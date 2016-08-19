using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Model;
using System.Data.Entity;

namespace WebDeveloper.Repository
{
    public class BusinessEntityAddressRepository : BaseRepository<BusinessEntityAddress>
    {
        public BusinessEntityAddress GetById(int BusinessEntityID, int AddressID, int AddressTypeID)
        {
            using (var db = new WebContextDb())
            {
                return db.BusinessEntityAddress.FirstOrDefault(p => p.BusinessEntityID == BusinessEntityID && p.AddressID == AddressID && p.AddressTypeID == AddressTypeID);
            }
        }

        public List<BusinessEntityAddress> GetListBySize(int size)
        {
            using (var db = new WebContextDb())
            {
                return db.BusinessEntityAddress
                    .OrderByDescending(p => p.ModifiedDate)
                    .Take(size).ToList();

            }
        }

        public BusinessEntityAddress GetCompleteBusinessEntityById(int id)
        {
            using (var db = new WebContextDb())
            {
                return db.BusinessEntityAddress
                    .Include(p => p.Address)
                    .FirstOrDefault(p => p.BusinessEntityID == id);
            }

        }
    }
}
