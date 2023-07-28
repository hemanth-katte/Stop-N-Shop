using Stop_nShop.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stop_nShop.Models
{
    public class Orders
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderId { get; set; }

        public DateTime orderDate { get; set; }

        public ICollection<Product> products { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }

        [ForeignKey("Seller")]
        public int sellerId { get; set; }

        public int quantity { get; set; }

        public OrderStatus orderStatus { get; set; }

        public int totalPrice { get; set; }
    
    }
}
