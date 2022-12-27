using System.ComponentModel.DataAnnotations;

namespace diplomski.Data.Models
{
    public class Template
    {
        [Key]
        public string Name { get; set; }
        public int Calories { get; set; }
        public List<Meal> Meals { get; set; }
    }
}
