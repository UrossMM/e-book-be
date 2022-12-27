using diplomski.Data.Dtos;
using System.ComponentModel.DataAnnotations;

namespace diplomski.Data.Models
{
    public class UserData
    {
        [Key]
        public string Mail { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public ActivityLevel ActivityLevel { get; set; }
        public Goal Goal { get; set; }
        public int DailyNumberOfMeals { get; set; }  
        public string Additions { get; set; }
        public string Number { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
