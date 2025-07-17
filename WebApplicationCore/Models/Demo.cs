using System;

namespace WebApplicationCore.Models
{
    public class Demo<AnyType>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AnyType Data { get; set; }
   
    }
}
