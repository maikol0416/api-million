
using System;

namespace ServiceApplication.Dto 
{
    public class PropertyImageDto : BaseDto
    {
        		public bool Enabled { get; set; }
		public byte[] FileContent { get; set; }
		public int IdProperty { get; set; }


        
        public PropertyImageDto()
            
            {
                

                
            }
        
        
    }
}