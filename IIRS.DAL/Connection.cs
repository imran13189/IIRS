using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIRS.DAL
{
    public class Connection
    {
        public IIRSEntities _db { get; set; }
        public Connection()
        {
            _db = new IIRSEntities();
        }
    }
}
