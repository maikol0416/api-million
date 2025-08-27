
using System;

namespace ServiceApplication.Dto 
{
    public class PropertyDto : BaseDto
    {
        		public string Address { get; set; }
		public string CodeInternal { get; set; }
		public int IdOwner { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Year { get; set; }


        
        public PropertyDto()
            
            {
                

                
            }
        
        
    }
}