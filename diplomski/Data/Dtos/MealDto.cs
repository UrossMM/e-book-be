using diplomski.Data.Models;

namespace diplomski.Data.Dtos
{
    public class MealDto
    {
        public string Name { get; set; } //
        public string? Ingredient { get; set; } //
        public int? Calories { get; set; } //
        public string Recipe { get; set; } //
        //public bool IsVegeterian { get; set; } 
        public float? Carbohydrates { get; set; }//
        public float? Proteins { get; set; }//
        public float? Fats { get; set; }//
        //public string Image { get; set; }//
        //public int NumberOfRepeatsPerWeek { get; set; }
        public int? Mass { get; set; } //


        public TimeToEat TimeToEat { get; set; }//ovo se unosi kada admin kreira template, ne koristi se za Meal model nego u spojnoj tabeli MealTemplate
        public Day Day { get; set; } // i ovo isto
        public List<EatIngredient> EatIngredients { get; set; } = new List<EatIngredient>();

        public List<FoodstuffDto> Foodstuffs { get; set; } 
    }

    public enum Day
    {
        Ponedeljak,
        Utorak,
        Sreda,
        Cetvrtak,
        Petak,
        Subota,
        Nedelja
    }
    public enum TimeToEat
    {
        Dorucak,
        Uzina_1,
        Rucak,
        Uzina_2,
        Vecera
    }
}
