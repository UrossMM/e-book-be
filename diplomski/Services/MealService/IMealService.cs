using diplomski.Data;
using diplomski.Data.Dtos;

namespace diplomski.Services.MealService
{
    public interface IMealService
    {
        //Task<StatusDto> AddMealAsync(MealDto meal);
        //Task<(StatusDto, MealDto)> GetMealAsync(int id);
        Task<List<MealDto>> GetAllMealsAsync();
        //Task<StatusDto> UpdateMealAsnyc(int id, MealDto meal);
        //Task<StatusDto> DeleteMealAsync(int id);
        Task<(StatusDto, List<MealDto>)> GetPersonalizedMealsAsync(UserInputDataDto data);
        Task<(StatusDto, List<string>)> GetAdditionsAsync();
        Task<StatusDto> AddMenuTemplateAsync(TemplateDto templateDto);
        Task<StatusDto> DeleteMenuTemplateAsync(string name);
        Task<(StatusDto, List<string>)> GetNamesOfTemplates();
        Task<(StatusDto, List<MealDto>)> GetFilteredMeals(MealFilter mealFilter);
        Task<StatusDto> AddFoodstuff(FoodstuffDto foodstuffDto);
        Task<(StatusDto, List<string>)> GetFoodStuffsAsync();

    }
}
