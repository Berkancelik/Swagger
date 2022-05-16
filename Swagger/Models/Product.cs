using System;
using System.Collections.Generic;

#nullable disable

namespace Swagger.Models
{
    public  class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? Date { get; set; }
        public string Category { get; set; }
    }
}
