using Stop_nShop.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stop_nShop.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productId { get; set; }

        public string productName { get; set; }

        public string category { get; set; }

        public string brand { get; set; }

        public int quantity { get; set; }

        public int price { get; set; }

        //public int size { get; set; }
        public ProductSize productSize { get; set; }

        [ForeignKey("Seller")]
        public int sellerId { get; set; }
       // public Seller? seller { get; set; }

        //[ForeignKey("Orders")]
       // public int? orderId { get; set; }
       // public Orders orders { get; set; }


    }
}
