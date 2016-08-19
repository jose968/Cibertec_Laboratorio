using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Model;
using System.Data.Entity;


namespace WebDeveloper.Repository
{
    public class BusinessEntityRepository : BaseRepository<BusinessEntity>
    {
        public BusinessEntity GetById(int id)
        {
            using (var db = new WebContextDb())
            {
                return db.BusinessEntity.FirstOrDefault(p => p.BusinessEntityID == id);
            }
        }

        public List<BusinessEntity> GetListBySize(int size)
        {
            using (var db = new WebContextDb())
            {
                return db.BusinessEntity
                    .OrderByDescending(p => p.ModifiedDate)
                    .Take(size).ToList();

            }
        }

        public BusinessEntity GetCompleteBusinessEntityById(int id)
        {
            using (var db = new WebContextDb())
            {
                return db.BusinessEntity
                    .Include(p => p.BusinessEntityContact)
                    .FirstOrDefault(p => p.BusinessEntityID == id);
            }

        }
    }
}
