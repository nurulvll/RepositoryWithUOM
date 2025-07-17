using System;

namespace WebApplicationCore.Models
{
    public class Student<T>
    {

        public T Id { set; get; }

        public void Set<T>(T Data)
        {

        }


    }
}
