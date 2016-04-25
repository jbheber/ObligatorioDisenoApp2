using Stockapp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data
{
    public class Admin : Person, ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
