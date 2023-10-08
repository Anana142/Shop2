using System;
using System.Collections.Generic;
using System.Text;

namespace Shop
{
    public class Category
    {
        public int Id { get; set; } 
        public string Title { get; set; }


        public override string ToString()
        {
            return Title;
        }
    }
    

}
   
