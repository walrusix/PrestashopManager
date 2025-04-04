using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walrus.PrestashopManager.Domain.Common;

namespace Walrus.PrestashopManager.Domain.Application
{
    public class Application : BaseEntity<Guid>
    {
        public string Name { set; get; }
        public decimal AnnualPrice { set; get; }

    }
}
