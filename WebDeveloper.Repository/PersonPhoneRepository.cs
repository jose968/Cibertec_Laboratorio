using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Model;
using System.Data.Entity;


namespace WebDeveloper.Repository
{
    public class PersonPhoneRepository : BaseRepository<PersonPhone>
    {
        public PersonPhone GetById(int id)
        {
            using (var db = new WebContextDb())
            {
                return db.PersonPhone.FirstOrDefault(p => p.BusinessEntityID == id);
            }
        }

        public List<PersonPhone> GetListBySize(int size)
        {
            using (var db = new WebContextDb())
            {
                return db.PersonPhone
                    .OrderByDescending(p => p.ModifiedDate)
                    .Take(size).ToList();

            }
        }

        public PersonPhone GetCompleteBusinessEntityById(int id)
        {
            using (var db = new WebContextDb())
            {
                return db.PersonPhone
                    .Include(p => p.PhoneNumberType)
                    .FirstOrDefault(p => p.BusinessEntityID == id);
            }

        }
    }
}
