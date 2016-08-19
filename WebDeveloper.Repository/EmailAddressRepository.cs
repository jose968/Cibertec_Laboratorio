using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Model;
using System.Data.Entity;

namespace WebDeveloper.Repository
{
    public class EmailAddressRepository : BaseRepository<EmailAddress>
    {
        public EmailAddress GetById(int id)
        {
            using (var db = new WebContextDb())
            {
                return db.EmailAddress.FirstOrDefault(p => p.BusinessEntityID == id);
            }
        }

        public List<EmailAddress> GetListBySize(int size)
        {
            using (var db = new WebContextDb())
            {
                return db.EmailAddress
                    .OrderByDescending(p => p.ModifiedDate)
                    .Take(size).ToList();

            }
        }

        public EmailAddress GetCompleteBusinessEntityById(int id)
        {
            using (var db = new WebContextDb())
            {
                return db.EmailAddress
                    .Include(p => p.EmailAddress1)
                    .FirstOrDefault(p => p.BusinessEntityID == id);
            }

        }
    }
}
