using diplomski.Data;
using diplomski.Data.Context;
using diplomski.Data.Dtos;
using diplomski.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace diplomski.Repositories.MealRepository
{
    public class MealRepository : IMealRepository
    {
        public ApplicationDbContext _dbContext;
        public MealRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        //public async Task<StatusDto> AddMealAsync(MealDto meal)
        //{
        //    Meal forCreate = new Meal()
        //    {
        //        Name = meal.Name,
        //        Calories = meal.Calories,
        //        //Carbohydrates = meal.Carbohydrates,
        //        // Fats = meal.Fats,
        //        //Image = meal.Image,
        //        //NumberOfRepeatsPerWeek = meal.NumberOfRepeatsPerWeek,
        //        //Proteins = meal.Proteins,
        //        Ingredient = meal.Ingredient,
        //        //TimeToEat = meal.TimeToEat,
        //        //IsVegeterian = meal.IsVegeterian,
        //        Mass = meal.Mass,
        //        Recipe = meal.Recipe
        //    };

        //    _dbContext.Meals.Add(forCreate);

        //    try
        //    {
        //        await _dbContext.SaveChangesAsync();
        //        return new StatusDto();
        //    }
        //    catch (Exception)
        //    {
        //        return new StatusDto(StatusCodes.Status500InternalServerError, "Server error");
        //    }

        //}

        //public async Task<StatusDto> DeleteMealAsync(int id)
        //{
        //    Meal? forDelete = await _dbContext.Meals.FirstOrDefaultAsync(x => x.Id == id);
        //    if (forDelete == null)
        //        return new StatusDto(StatusCodes.Status404NotFound, "Meal doesn't exitst");

        //    _dbContext.Meals.Remove(forDelete);

        //    try
        //    {
        //        await _dbContext.SaveChangesAsync();
        //        return new StatusDto();
        //    }
        //    catch (Exception)
        //    {
        //        return new StatusDto(StatusCodes.Status500InternalServerError, "Server error");
        //    }

        //}

        public async Task<List<MealDto>> GetAllMealsAsync()
        {
            List<MealDto> mealsDto = await _dbContext.Meals.Select(x => new MealDto
            {
                Calories = x.Calories,
                Carbohydrates = x.Carbohydrates,
                Fats = x.Fats,
                // Image = x.Image,
                Ingredient = x.Ingredient,
                //IsVegeterian = x.IsVegeterian,
                Mass = x.Mass,
                Name = x.Name,
                Proteins = x.Proteins,
                //NumberOfRepeatsPerWeek = x.NumberOfRepeatsPerWeek,
                Recipe = x.Recipe,
                // TimeToEat = x.TimeToEat
            }).ToListAsync();

            return (mealsDto);
        }

        //public async Task<(StatusDto, MealDto)> GetMealAsync(int id)
        //{
        //    MealDto? mealDto = await _dbContext.Meals.Where(x => x.Id == id).Select(x => new MealDto
        //    {
        //        Calories = x.Calories,
        //        Carbohydrates = x.Carbohydrates,
        //        Fats = x.Fats,
        //        // Image = x.Image,
        //        Ingredient = x.Ingredient,
        //        //IsVegeterian = x.IsVegeterian,
        //        Mass = x.Mass,
        //        Name = x.Name,
        //        Proteins = x.Proteins,
        //        // NumberOfRepeatsPerWeek = x.NumberOfRepeatsPerWeek,
        //        Recipe = x.Recipe,
                
        //        //TimeToEat = x.TimeToEat,
        //    }).FirstOrDefaultAsync();

        //    if (mealDto == null)
        //        return (new StatusDto(StatusCodes.Status404NotFound, "Meal not found"), null);

        //    return (new StatusDto(), mealDto);
        //}

        //public async Task<StatusDto> UpdateMealAsnyc(int id, MealDto meal)
        //{
        //    Meal? forChange = await _dbContext.Meals.FirstOrDefaultAsync(x => x.Id == id);
        //    if (forChange == null)
        //        return new StatusDto(StatusCodes.Status404NotFound, "Meal doesn't exitst");

        //    forChange.Name = meal.Name;
        //    forChange.Mass = meal.Mass;
        //    //forChange.TimeToEat = meal.TimeToEat;
        //    forChange.Recipe = meal.Recipe;
        //    //forChange.IsVegeterian = meal.IsVegeterian;
        //    // forChange.Image = meal.Image;
        //    //forChange.Fats = meal.Fats;
        //    // forChange.Carbohydrates = meal.Carbohydrates;
        //    forChange.Calories = meal.Calories;
        //    //forChange.NumberOfRepeatsPerWeek = meal.NumberOfRepeatsPerWeek;
        //    //forChange.Proteins = meal.Proteins;
        //    forChange.Ingredient = meal.Ingredient;

        //    _dbContext.Meals.Update(forChange);

        //    try
        //    {
        //        await _dbContext.SaveChangesAsync();
        //        return new StatusDto();
        //    }
        //    catch (Exception)
        //    {
        //        return new StatusDto(StatusCodes.Status500InternalServerError, "Server error");
        //    }
        //}
        private async Task<List<MealDto>> FindInedibleIngredients(List<MealDto> meals, List<string> additions, Goal goal)
        {
            foreach (MealDto meal in meals)
            {
                List<string> mealAdditions = meal.Ingredient.Split(',').ToList();

                List<string> InedibleIngredients = mealAdditions.Intersect(additions)
                    .ToList();

                if(InedibleIngredients.Count > 0)
                {
                    MealAutoFilter filter = new MealAutoFilter();
                    filter.Additions = additions;
                    filter.Calories = meal.Calories.GetValueOrDefault();
                    filter.Goal = goal; 

                    var replacementMeal = await GetFilteredMealAutomatic(filter);
                    if(replacementMeal != null)
                    {
                        mealAdditions = replacementMeal.Ingredient.Split(',').ToList();
                        InedibleIngredients = mealAdditions.Intersect(additions).ToList();
                    }
                }

                foreach (string inedibleIngredient in InedibleIngredients)
                {
                    EatIngredient forCreate = new EatIngredient()
                    {
                        Name = inedibleIngredient,
                        Eat = false
                    };
                    meal.EatIngredients.Add(forCreate);
                }
                List<string> EdibleIngredients = mealAdditions.Except(InedibleIngredients).ToList();
                foreach (string edibleIngredient in EdibleIngredients)
                {
                    EatIngredient forCreate = new EatIngredient()
                    {
                        Name = edibleIngredient,
                        Eat = true
                    };
                    meal.EatIngredients.Add(forCreate);
                }
            }

            return meals;
        }
        public async Task<(StatusDto, List<MealDto>)> GetPersonalizedMealsAsync(UserInputDataDto data)
        {
            float Bmr = 0;
            float Tdee = 0;
            float Kcal = 0;
            if (data.Gender == Gender.M)
            {
                Bmr = 66 + (13.7f * data.Weight) + (5 * data.Height) - (6.8f * data.Age);
            }
            else
            {
                Bmr = 655 + (9.6f * data.Weight) + (1.8f * data.Height) - (4.7f * data.Age);
            }

            if (data.ActivityLevel == ActivityLevel.MinimallyActive)
                Tdee = Bmr * 1.2f;
            else if (data.ActivityLevel == ActivityLevel.LittleActive)
                Tdee = Bmr * 1.375f;
            else if (data.ActivityLevel == ActivityLevel.ModeratelyActive)
                Tdee = Bmr * 1.55f;
            else if (data.ActivityLevel == ActivityLevel.VeryActive)
                Tdee = Bmr * 1.725f;
            else
                Tdee = Bmr * 1.9f;

            Template? template = null;

            if (data.Goal == Goal.KeepingFit)
            {
                Kcal = Tdee;
                

                Template? templatePlus = await _dbContext.Templates
                    .OrderBy(x => x.Calories)
                    .FirstOrDefaultAsync(x => x.Calories >= Kcal && (x.Meals.Count / 7) == data.DailyNumberOfMeals);

                Template? templateMinus = await _dbContext.Templates
                    .OrderByDescending(x => x.Calories)
                    .FirstOrDefaultAsync(x => x.Calories <= Kcal && (x.Meals.Count / 7) == data.DailyNumberOfMeals);

                if (templatePlus != null && templateMinus != null)
                {
                    if ((templatePlus.Calories - Kcal) > (Kcal - templateMinus.Calories))
                        template = templatePlus;
                    else
                        template = templateMinus;
                }
                else if (templatePlus == null && templateMinus != null)
                    template = templateMinus;
                else if (templatePlus != null && templateMinus == null)
                    template = templatePlus;

            }
            else if (data.Goal == Goal.Fattening)
            {
                Kcal = Tdee * 1.1f;
                
                template = await _dbContext.Templates
                    .OrderBy(x => x.Calories)
                    .FirstOrDefaultAsync(x => x.Calories >= Kcal && (x.Meals.Count / 7) == data.DailyNumberOfMeals);
            }
            else
            {
                Kcal = Tdee * 0.9f;
                template = await _dbContext.Templates
                    .OrderByDescending(x => x.Calories)
                    .FirstOrDefaultAsync(x => x.Calories <= Kcal && (x.Meals.Count / 7) == data.DailyNumberOfMeals);
            }

            if (template != null)
            {
                List<MealDto> result = await _dbContext.Meals
                .Where(x => x.TemplateName == template.Name)
                .OrderBy(x => x.Day)
                .ThenBy(x => x.Period)
                .Select(x => new MealDto
                {
                    Calories = x.Calories,
                    Ingredient = x.Ingredient,
                    Mass = x.Mass,
                    Name = x.Name,
                    Recipe = x.Recipe,
                    Day = x.Day,
                    TimeToEat = x.Period,
                    Carbohydrates = x.Carbohydrates,
                    Fats = x.Fats,
                    Proteins = x.Proteins
                })
                .ToListAsync();

                result = await FindInedibleIngredients(result, data.Additions.Split(',').ToList(), data.Goal);

                return (new StatusDto(), result);
            }

            return (new StatusDto(), new List<MealDto>());
        }

        public async Task<(StatusDto, List<string>)> GetAdditionsAsync() // dodati tolower 
        {
            List<string> allAdditions = await _dbContext.Meals.Select(x => x.Ingredient).ToListAsync();

            List<string> resultAdditions = new List<string>();

            foreach (var item in allAdditions)
            {
                List<string> mealAdditions = item.Split(',').ToList();
                foreach (string addition in mealAdditions)
                {
                    if (!resultAdditions.Contains(addition))
                    {
                        resultAdditions.Add(addition);
                    }
                }
            }

            resultAdditions = resultAdditions.OrderBy(x => x).ToList();
            return (new StatusDto(), resultAdditions);
        }

        public async Task<StatusDto> AddMenuTemplateAsync(TemplateDto templateDto)
        {
            Template? templateExists = await _dbContext.Templates.FirstOrDefaultAsync(x => x.Name == templateDto.Name);
            if (templateExists != null)
                return new StatusDto(StatusCodes.Status400BadRequest, "Template with that name already exists");
            
            Template template = new Template()
            {
                Name = templateDto.Name,
            };

            int totalKcal = 0;
            foreach (MealDto mealDto in templateDto.MealsPerDay)
            {
                //totalKcal += mealDto.Calories;
                
                Meal meal = new Meal()
                {
                    Name = mealDto.Name,
                   // Calories = mealDto.Calories,
                    //Ingredient = mealDto.Ingredient,
                    //Mass = mealDto.Mass,
                    //Recipe = mealDto.Recipe,
                    Template = template,
                    Day = mealDto.Day,
                    Period = mealDto.TimeToEat
                };

                meal.Recipe = "Za pripremu jela potrebno je: ";
                foreach(FoodstuffDto foodStuffDto in mealDto.Foodstuffs)
                {
                    Foodstuff? foodStuff = await _dbContext.Foodstuffs.FirstOrDefaultAsync(x => x.Name == foodStuffDto.Name);
                    if(foodStuff ==null)
                        return new StatusDto(StatusCodes.Status400BadRequest, "Ingredient not found");

                    var scaleGrams = (float)foodStuffDto.Mass / (float)foodStuff.Grams;

                    meal.Mass += foodStuffDto.Mass;

                    meal.Proteins +=  (scaleGrams * (float)foodStuff.Proteins);
                    meal.Carbohydrates += (scaleGrams * (float)foodStuff.Carbohydrates);
                    meal.Fats += (scaleGrams * (float)foodStuff.Fats);

                    meal.Calories += (int)(scaleGrams * foodStuff.Calories);

                    meal.Ingredient += foodStuff.Name + ",";
                    meal.Recipe += foodStuff.Name + " " + foodStuffDto.Mass + "g, ";
                }
                //meal.Recipe = meal.Recipe.Remove(meal.Recipe.Length - 1, 1);
                //meal.Recipe += ". ";
                meal.Recipe += mealDto.Recipe;
                totalKcal += meal.Calories;

                _dbContext.Meals.Add(meal);
            }

            template.Calories = totalKcal / 7;
            template.Name += " (" + template.Calories + " kcal)";
            try
            {
                await _dbContext.SaveChangesAsync();
                return new StatusDto();
            }
            catch (Exception)
            {
                return new StatusDto(StatusCodes.Status500InternalServerError, "Server error while writing to database");
            }
        }

        public async Task<StatusDto> DeleteMenuTemplateAsync(string name)
        {
            ////List<Meal> mealsFromGivenTemplate = await _dbContext.MealTemplates
            ////    .Include(x => x.Template)
            ////    .Include(x => x.Meal)
            ////    .Where(x => x.Template.Name == name)
            ////    .Select(x => x.Meal)
            ////    .ToListAsync();

            // foreach (Meal meal in meals)
            //_dbContext.Meals.Remove(meal);  //ovo ce da obrise meal iz svih jelovnika , treba da obrises redove u referencirajucoj tabeli 
            //i jelo ukoliko ga nema vise u referencirajucoj tabeli


            //List<MealTemplate> mealTemplates = await _dbContext.MealTemplates
            //     .Include(x => x.Template)
            //     .Include(x => x.Meal)
            //     .Where(x => x.Template.Name == name)
            //     .ToListAsync();
            //foreach (MealTemplate mealTemplate in mealTemplates)
            //    _dbContext.MealTemplates.Remove(mealTemplate);


            //sva jela koja se nalaze samo u templejtu koji se brise a ne nalaze se u ostalim templejtima
            //List<Meal> allMeals = await _dbContext.MealTemplates //mealtemplates ima 56 reda
            //    .Include(x => x.Meal)
            //    .Where(x => mealsFromGivenTemplate.Contains(x.Meal) == false) //meals from given template ima 28 jela. 27 istih kao i drugi template i 1 novo
            //    .Select(x => x.Meal)
            //    .ToListAsync();

            ////List<Meal> allMeals = await _dbContext.MealTemplates //mealtemplates ima 56 reda
            ////    .Include(x => x.Meal)
            ////    .Where(x => x.TemplateId.ToLower() != name.ToLower()) //meals from given template ima 28 jela. 27 istih kao i drugi template i 1 novo
            ////    .Select(x => x.Meal)
            ////    .ToListAsync();

            //nadji sve one koji postoje u mealsFromGivenTemplate a ne postoje u allMeals i njih obrisi
            ////List<Meal> mealsForDelete = mealsFromGivenTemplate.Except(allMeals).ToList();
            ////foreach (Meal meal in mealsForDelete)
            ////    _dbContext.Meals.Remove(meal);

            List<Meal> mealsForDelete = await _dbContext.Meals.Where(x => x.TemplateName == name).ToListAsync();
            _dbContext.Meals.RemoveRange(mealsForDelete);

            Template t = await _dbContext.Templates.FirstAsync(x => x.Name == name);
            _dbContext.Templates.Remove(t);

            try
            {
                await _dbContext.SaveChangesAsync();
                return new StatusDto();
            }
            catch (Exception)
            {
                return new StatusDto(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        public async Task<(StatusDto, List<string>)> GetNamesOfTemplates()
        {
            List<string> names = await _dbContext.Templates.Select(x => x.Name).ToListAsync();

            return (new StatusDto(), names);
        }

        public async Task<(StatusDto, List<MealDto>)> GetFilteredMeals(MealFilter mealFilter)
        {
            List<MealDto> meals = null;
            IQueryable<Meal> querymeals;

            if(string.IsNullOrWhiteSpace(mealFilter.TemplateName) == false)
            {
                querymeals = _dbContext.Meals.Where(x => x.TemplateName == mealFilter.TemplateName);
            }
            else
                querymeals = _dbContext.Meals;

            if (mealFilter.CaloriesPlus)
                meals = await querymeals.Where(x => x.Calories >= mealFilter.Calories).Select(x => new MealDto
                {
                    Calories = x.Calories,
                    Mass = x.Mass,
                    Name = x.Name,
                    Recipe = x.Recipe,
                    Ingredient = x.Ingredient,
                    Carbohydrates = x.Carbohydrates,
                    Proteins=x.Proteins,
                    Fats=x.Fats
                }).OrderBy(x => x.Calories)
                .ToListAsync();
            else
                meals = await querymeals.Where(x => x.Calories <= mealFilter.Calories).Select(x => new MealDto
                {
                    Calories = x.Calories,
                    Mass = x.Mass,
                    Name = x.Name,
                    Recipe = x.Recipe,
                    Ingredient = x.Ingredient,
                    Carbohydrates = x.Carbohydrates,
                    Proteins = x.Proteins,
                    Fats = x.Fats
                }).OrderByDescending(x => x.Calories)
                    .ToListAsync();

            List<MealDto> result = new List<MealDto>();

            if (string.IsNullOrWhiteSpace(mealFilter.Additions) == false)
            {
                List<string> additionsFromFilter = mealFilter.Additions.Split(',').ToList();

                foreach (MealDto meal in meals)
                {
                    List<string> mealAdditions = meal.Ingredient.Split(',').ToList();

                    if (additionsFromFilter.Intersect(mealAdditions).ToList().Count == 0)
                        result.Add(meal);
                }
            }
            else
                result = meals;

            return (new StatusDto(), result);
        }

        private async Task<MealDto> GetFilteredMealAutomatic(MealAutoFilter mealFilter)
        {
            MealDto? meal = null;
            List<Meal> filteredMealsByGoal = new List<Meal>();
            IQueryable<Meal> querymeals = _dbContext.Meals;

            if (mealFilter.Goal == Goal.Fattening)
                filteredMealsByGoal = await querymeals.Where(x => x.Calories >= mealFilter.Calories)
                    .OrderByDescending(x => x.Calories)
                    .ToListAsync();
            
            else if (mealFilter.Goal == Goal.WeightLoss)
                filteredMealsByGoal = await querymeals.Where(x => x.Calories <= mealFilter.Calories)
                    .OrderByDescending(x => x.Calories)
                    .ToListAsync();
                
            else {
                var minCalories = mealFilter.Calories - mealFilter.Calories * 0.2;
                var maxCalories = mealFilter.Calories + mealFilter.Calories * 0.2;
                filteredMealsByGoal = await querymeals.Where(x => x.Calories >= minCalories && x.Calories <= maxCalories)
                    .OrderByDescending(x => x.Calories)
                    .ToListAsync();
            }

            foreach(Meal filteredMealByGoal in filteredMealsByGoal)
            {
                List<string> mealIngredients = filteredMealByGoal.Ingredient.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                bool containsAdditionsFromFilter = mealIngredients.Any(x => mealFilter.Additions.Contains(x));

                if (containsAdditionsFromFilter)
                    continue;
                else
                {
                    meal = new MealDto()
                    {
                        Calories = filteredMealByGoal.Calories,
                        Mass = filteredMealByGoal.Mass,
                        Name = filteredMealByGoal.Name,
                        Recipe = filteredMealByGoal.Recipe,
                        Ingredient = filteredMealByGoal.Ingredient,
                        Carbohydrates = filteredMealByGoal.Carbohydrates,
                        Proteins = filteredMealByGoal.Proteins,
                        Fats = filteredMealByGoal.Fats
                    };

                    return meal;
                }
            }

            return meal;
        }

        public async Task<StatusDto> AddFoodstuff(FoodstuffDto foodstuffDto)
        {
            Foodstuff? foodstuff = await _dbContext.Foodstuffs.FirstOrDefaultAsync(x => x.Name == foodstuffDto.Name);
            if (foodstuff != null)
                return new StatusDto(StatusCodes.Status400BadRequest, "Bad request");

            Foodstuff forCreate = new Foodstuff
            {
                Name = foodstuffDto.Name,
                Calories = foodstuffDto.Calories ?? 0,
                Carbohydrates = foodstuffDto.Carbohydrates ?? 0,
                Fats = foodstuffDto.Fats ?? 0,
                Proteins = foodstuffDto.Proteins ?? 0,
                Grams = foodstuffDto.Mass
            };

            _dbContext.Foodstuffs.Add(forCreate);

            try
            {
                await _dbContext.SaveChangesAsync();
                return new StatusDto();
            }
            catch(Exception)
            {
                return new StatusDto(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        public async Task<(StatusDto, List<string>)> GetFoodStuffsAsync()
        {
            List<string> foodStuffsNames = await _dbContext.Foodstuffs.Select(x => x.Name)
                .OrderBy(x => x)
                .ToListAsync();

            return (new StatusDto(), foodStuffsNames);
        }

    }
}



