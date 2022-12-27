using diplomski.Data.Dtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace diplomski.Data.Models
{
    public class Meal 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredient { get; set; } 
        public int Calories { get; set; }
        public string Recipe { get; set; } 
        public int Mass { get; set; } 
        public TimeToEat Period { get; set; }
        public Day Day { get; set; }
        public float Carbohydrates { get; set; }//
        public float Proteins { get; set; }//
        public float Fats { get; set; }//
        public Template Template { get; set; }
        [ForeignKey(nameof(Template))]
        public string TemplateName { get; set; }

    }
}
