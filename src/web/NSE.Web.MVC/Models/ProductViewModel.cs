using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Image { get; set; }
        public int QuantityStock { get; set; }
    }
}
