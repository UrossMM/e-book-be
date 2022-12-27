using diplomski.Data.Dtos;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

namespace diplomski.Services.WordGenerator
{
    public class HtmlGeneratorService : IHtmlGeneratorService
    {

        public string GenerateHtmlCode(List<MealDto> meals, AdminDefaultDto adminDefaultsDto, Goal goal)
        {
            string html = "<html><head></head><body>";

            string toAllUsers = " <div>" +
                                    "<h1 style = \"color: rgb(79, 238, 113); font-style: bold; text-align: center; font-size: 36px; padding-top: 25px;\">Personalizovani plan ishrane </h1>" +
                                    "<p>" + adminDefaultsDto.UnpersonalizedText + "</p>" +
                                "</div>";
            html += toAllUsers;

            string textBasedOnGoal = "";
            if (goal == Goal.WeightLoss && string.IsNullOrWhiteSpace(adminDefaultsDto.WeightLossText) == false)
                textBasedOnGoal += "<div><p>" + adminDefaultsDto.WeightLossText + "</p></div>";
            else if (goal == Goal.Fattening && string.IsNullOrWhiteSpace(adminDefaultsDto.FatteningText) == false)
                textBasedOnGoal += "<div><p>" + adminDefaultsDto.FatteningText + "</p></div>";
            else if (goal == Goal.KeepingFit && string.IsNullOrWhiteSpace(adminDefaultsDto.KeepingFitText) == false)
                textBasedOnGoal += "<div><p>" + adminDefaultsDto.KeepingFitText + "</p></div>";

            html += textBasedOnGoal;

            int i = 0;
            
            foreach (MealDto meal in meals)
            {
                string elementsForMeal = "<div style=\"display: flex; flex-direction: row; \"> <div>";
                string paragraph = "";
                TimeToEat mappedTime = MapTime( meal.TimeToEat, meals.Count / 7);
                if (i % (meals.Count / 7) == 0)
                    paragraph += "<h1>" + ((Day)meal.Day).ToString() + "</h1>";

                paragraph += "<h3>" + mappedTime + "</h2>" +
                             "<h4>" + meal.Name + "</h3>";
                paragraph += "<p> Sastojci:";             
                             foreach(EatIngredient eatIngredient in meal.EatIngredients)
                             {
                                    if (eatIngredient.Eat)
                                        paragraph += "<span>" + eatIngredient.Name + ", </span>";
                                    else
                                        paragraph += "<span  style = \"color: rgb(255, 0, 0);\">" + eatIngredient.Name + ", </span>";
                             }
                paragraph += "</p>";
                paragraph+= "<p> Masa: " + meal.Mass + "</p>" +
                             "<p> Proteini: " + meal.Proteins + "</p>" +
                             "<p> Masti: " + meal.Fats + "</p>" +
                             "<p> Ugljeni hidrati: " + meal.Carbohydrates + "</p>" +
                             "<p> Kalorije: " + meal.Calories + "</p>" +
                             "<p> Recept: " + meal.Recipe + "</p>" +
                         "</div>";

                paragraph += "<div> </div>"; 

                html += paragraph;
                ++i;
            }
            html += "</body></html>";

            return html;
        }

        private TimeToEat MapTime(TimeToEat timeToEat, int numberOfMealsPerDay)
        {
            TimeToEat mapped = timeToEat;

            if(numberOfMealsPerDay == 3)
            {
                if (timeToEat.ToString().Equals("Uzina_1"))
                    mapped = TimeToEat.Rucak;
                else if(timeToEat.ToString().Equals("Rucak"))
                    mapped = TimeToEat.Vecera;
            }
            else if(numberOfMealsPerDay == 4)
            {
                if (timeToEat.ToString().Equals("Uzina_2"))
                    mapped = TimeToEat.Vecera;
            }

            return mapped;
        }
    }
}
