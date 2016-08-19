using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Model;
using System.Data.Entity;

namespace WebDeveloper.Repository
{
    public class BusinessEntityContactRepository : BaseRepository<BusinessEntityContact>
    {
        public BusinessEntityContact GetById(int id)
        {
            using (var db = new WebContextDb())
            {
                return db.BusinessEntityContact.FirstOrDefault(p => p.BusinessEntityID == id);
            }
        }

        public List<BusinessEntityContact> GetListBySize(int size)
        {
            using (var db = new WebContextDb())
            {
                return db.BusinessEntityContact
                    .OrderByDescending(p => p.ModifiedDate)
                    .Take(size).ToList();

            }
        }

        public BusinessEntityContact GetCompleteBusinessEntityById(int id)
        {
            using (var db = new WebContextDb())
            {
                return db.BusinessEntityContact
                    .Include(p => p.BusinessEntity)
                    .FirstOrDefault(p => p.BusinessEntityID == id);
            }

        }
    }
}
