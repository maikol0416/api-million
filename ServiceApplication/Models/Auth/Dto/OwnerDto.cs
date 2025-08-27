
using System;

namespace ServiceApplication.Dto 
{
    public class OwnerDto : BaseDto
    {
        		public string Address { get; set; }
		public DateTime Birthday { get; set; }
		public string Name { get; set; }
		public byte[] Photo { get; set; }


        
        public OwnerDto()
            
            {
                

                
            }
        
        
    }
}