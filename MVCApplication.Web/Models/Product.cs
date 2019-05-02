using System.ComponentModel.DataAnnotations;

namespace MVCApplication.Web.Models
{
    public class Product
    {
        [Display(AutoGenerateField = false)]
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Supplier Id")]
        public int SupplierID { get; set; } = 0;

        [Required]
        [Display(Name = "Category Id")]
        public int CategoryID { get; set; }

        [Required]
        [Display(Name = "Quantity per unit")]
        public string QuantityPerUnit { get; set; }

        [Required]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Display(Name = "Units in Stock")]
        public int UnitsInStock { get; set; }

        [Required]
        [Display(Name = "Units On Order")]
        public int UnitsOnOrder { get; set; }

        [Required]
        [Display(Name = "Reorder Level")]
        public int ReorderLevel { get; set; }

        public bool Discounted { get; set; }
    }
}