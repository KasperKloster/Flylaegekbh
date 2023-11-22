using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Repos
{
    public abstract class RepoBase
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
        public virtual void Create() { }
        public virtual void Update() { }
        public virtual void Delete() { }
        public virtual void Get() { }
    }
}
