using diplomski.Data.Dtos;

namespace diplomski.Data
{
    public class MealAutoFilter
    {
        public List<string> Additions { get; set; } = new List<string>();
        public int Calories { get; set; }
        public Goal Goal { get; set; }
    }
}
