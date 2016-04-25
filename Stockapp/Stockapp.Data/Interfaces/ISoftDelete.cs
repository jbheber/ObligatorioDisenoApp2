using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data.Interfaces
{
    public interface ISoftDelete
    {
        /// <summary>
        /// Used for soft delete (logic delete)
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
