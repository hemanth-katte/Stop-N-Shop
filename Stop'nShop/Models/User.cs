using Stop_nShop.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stop_nShop.Models
{
    public class User
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }

        [Required]
        public string firstName { get; set; }

        public string lastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string userEmail { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number")]
        public string userPhone { get; set; }

        public string userPassword { get; set; }

        public string userAddress { get; set; }

        public UserStatus userStatus { get; set; }

        public int cartId { get; set; }

        public int wishListId { get; set; }

       // public ICollection<Orders>? orders { get; set; }

    }
}
