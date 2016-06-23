using Stockapp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stockapp.Portal.Models
{
    public class UpdateStockDTO
    {
        public Stock Stock { get; set; }

        public DateTimeOffset DateOfChange { get; set; }
    }
}