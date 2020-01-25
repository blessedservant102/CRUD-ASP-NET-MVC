using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_MVC.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Display(Name = "Category Name ")]
        public string CategoryName { get; set; }
    }
}