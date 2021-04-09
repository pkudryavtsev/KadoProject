using System.ComponentModel.DataAnnotations;

namespace Services.Dtos
{
    public class ProductToCreateDto
    {
        public string Name { get; set; } 
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public double Depth { get; set; }
        public double Area { get; set; }
        public string PictureUrl { get; set; }
        public int ProductTypeId { get; set; }
        [Required]
        public int ProductBrandId { get; set; }
        public int CategoryId { get; set; }
        public string Tags { get; set; }  
    }
}