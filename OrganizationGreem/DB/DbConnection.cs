using OrganizationGreem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationGreem.DB
{
    internal class DbConnection
    {
        static public GreenEntities db = new GreenEntities();
        static public Logins logins;
    }
}
