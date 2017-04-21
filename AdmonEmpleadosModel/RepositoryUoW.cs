using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmonEmpleadosModel
{
    public class RepositoryUoW : EFRepository.RepositoryUoW, IDisposable, IRepositoryUoW
    {
        public RepositoryUoW(
            bool autoDetectChangesEnabled = false,
            bool proxyCreationEnabled = false):
            base(new AdmonEmpleadosEntities(), autoDetectChangesEnabled, proxyCreationEnabled)
        { }
    }
}
