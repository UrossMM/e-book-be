using System.ComponentModel.DataAnnotations;

namespace diplomski.Data.Models
{
    public class Foodstuff
    {
        [Key]
        public string Name { get; set; }
        public float Proteins { get; set; }
        public float Fats { get; set; }
        public float Carbohydrates { get; set; }
        public int Calories { get; set; }
        public int Grams { get; set; }

    }
}
