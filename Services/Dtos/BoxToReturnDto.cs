using System.Collections.Generic;
using ProductDb.DataClasses;
using ProductDb.DataClasses.Enums;

namespace Services.Dtos
{
    public class BoxToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BoxSize Size { get; set; } = BoxSize.Small;
        public List<ProductToReturnDto> Products { get; set; }
        
    }
}