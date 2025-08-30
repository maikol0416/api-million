
using System;

namespace ServiceApplication.Dto 
{
    public class PropertyTraceDto : BaseDto
    {
        		public DateTime DateSale { get; set; }
		public int IdProperty { get; set; }
		public string Name { get; set; }
		public decimal Tax { get; set; }
		public decimal Value { get; set; }


        
        public PropertyTraceDto()
            
            {
                

                
            }
        
        
    }
}