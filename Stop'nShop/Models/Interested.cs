using Stop_nShop.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stop_nShop.Models
{
    public class Interested
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int interestedID { get; set; }

        public int userId { get; set; }

        public int sellerId { get; set; }

        public int productId { get; set; }

        public Product product { get; set; }

        public InterestedStatus status { get; set; }

    }
}
