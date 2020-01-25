using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_MVC.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public Nullable<int> CategoryId { get; set; }

        public string CategoryName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
    }
}