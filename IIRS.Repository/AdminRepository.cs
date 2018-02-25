using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IIRS.DAL;
using IIRS.Core.Model;

namespace IIRS.Repository
{
    public class AdminRepository:Connection
    {
       public List<GetAdminFiles_Result> GetFiles(string search)
        {
            return _db.GetAdminFiles(search).ToList();
        }
    }
}
