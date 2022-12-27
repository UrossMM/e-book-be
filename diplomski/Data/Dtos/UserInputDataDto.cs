namespace diplomski.Data.Dtos
{
    public class UserInputDataDto
    {
        public Gender Gender { get; set; }  
        public int Age { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public ActivityLevel ActivityLevel { get; set; }
        public Goal Goal { get; set; }
        public int DailyNumberOfMeals { get; set; }  // 3 do 6 
        //sastojci koje ne jede
        public string Additions { get; set; }
        public string Mail { get; set; }
        public string Number { get; set; }
    }

    public enum Goal
    {
        WeightLoss,
        Fattening,
        KeepingFit
    }

    public enum ActivityLevel
    {
        MinimallyActive,
        LittleActive,
        ModeratelyActive,
        VeryActive,
        Professional
    }
    public enum Gender
    {
        M,
        F
    }
}
