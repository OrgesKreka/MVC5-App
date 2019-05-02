using System;
using System.ComponentModel.DataAnnotations;

namespace MVCApplication.Web.Models
{
    public class Category
    {
        [Display(AutoGenerateField = false)]
        public int CategoryID { get; set; }

        [Display(Name = "Category Name")]
        [Required]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        [Display(AutoGenerateField = false)]
        public byte[] Picture { get; set; }

        [Display(Name = "Category Image")]
        public string PictureAsString64 => Convert.ToBase64String(Picture ?? new byte[] { });

    }
}