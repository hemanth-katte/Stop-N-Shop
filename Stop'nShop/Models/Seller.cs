using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stop_nShop.Models
{
    public class Seller
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int sellerId { get; set; }

        [Required]
        public string sellerName { get; set; }


        public string sellerPassword { get; set; }


        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string sellerMailId { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number")]
        public string sellerPhone { get; set; }

        public string sellerAddress { get; set;}

        public int sellerRatings { get; set; }

       // public ICollection<Product> products { get; set; }

    }
}

