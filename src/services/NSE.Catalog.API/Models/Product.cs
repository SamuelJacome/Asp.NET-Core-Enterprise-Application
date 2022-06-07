using NSE.Core.DomainObjects;
using System;

namespace NSE.Catalogo.API.Models
{
    public class Product : Entity,  IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Image { get; set; }
        public int QuantityStock { get; set; }
    }
}