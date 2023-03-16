﻿namespace Products.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //Navigation 
        public ICollection<Product> Products{ get; set;}
    }
}
