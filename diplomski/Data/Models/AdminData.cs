using System.ComponentModel.DataAnnotations;

namespace diplomski.Data.Models
{
    public class AdminData
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
