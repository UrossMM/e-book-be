using diplomski.Data;
using diplomski.Data.Dtos;
using diplomski.Repositories.MealRepository;

namespace diplomski.Services.MealService
{
    public class MealService : IMealService
    {
        public IMealRepository _mealRepository;

        public MealService(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }
        //public async Task<StatusDto> AddMealAsync(MealDto meal)
        //{
        //    try
        //    {
        //        //ovde dodaj validacije 

        //        StatusDto status = await _mealRepository.AddMealAsync(meal);
        //        return status;
        //    }
        //    catch(Exception)
        //    {
        //        return new StatusDto(StatusCodes.Status500InternalServerError, "Server error");
        //    }
        //}

        //public async Task<StatusDto> DeleteMealAsync(int id)
        //{
        //    try
        //    {
        //        StatusDto status = await _mealRepository.DeleteMealAsync(id);
        //        return status;
        //    }
        //    catch (Exception)
        //    {
        //        return new StatusDto(StatusCodes.Status500InternalServerError, "Server error");
        //    }
        //}

        public async Task<List<MealDto>> GetAllMealsAsync()
        {
            try
            {
                var  meals = await _mealRepository.GetAllMealsAsync();
                return meals;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //public async Task<(StatusDto, MealDto)> GetMealAsync(int id)
        //{
        //    try
        //    {
        //        var (status, meal) = await _mealRepository.GetMealAsync(id);
        //        return (status, meal);
        //    }
        //    catch (Exception)
        //    {
        //        return (new StatusDto(StatusCodes.Status500InternalServerError, "Server error"), null);
        //    }
        //}

        //public async Task<StatusDto> UpdateMealAsnyc(int id, MealDto meal)
        //{
        //    try
        //    {
        //        StatusDto status = await _mealRepository.UpdateMealAsnyc(id, meal);
        //        return status;
        //    }
        //    catch (Exception)
        //    {
        //        return new StatusDto(StatusCodes.Status500InternalServerError, "Server error");
        //    }
        //}

        public async Task<(StatusDto, List<MealDto>)> GetPersonalizedMealsAsync(UserInputDataDto data)
        {
            try
            {
                var (status, meals) = await _mealRepository.GetPersonalizedMealsAsync(data);
                return (status, meals);
            }
            catch (Exception)
            {
                return (new StatusDto(StatusCodes.Status500InternalServerError, "Server error"), null);
            }
        }

        public async Task<(StatusDto, List<string>)> GetAdditionsAsync()
        {
            try
            {
                var (status, additions) = await _mealRepository.GetAdditionsAsync();
                return (status, additions);
            }
            catch(Exception)
            {
                return (new StatusDto(StatusCodes.Status500InternalServerError, "Server error"), null);
            }
        }

        public async Task<StatusDto> AddMenuTemplateAsync(TemplateDto templateDto)
        {
            try
            {
                StatusDto status = await _mealRepository.AddMenuTemplateAsync(templateDto);
                return status;
            }
            catch (Exception)
            {
                return new StatusDto(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        public async Task<StatusDto> DeleteMenuTemplateAsync(string name)
        {
            try
            {
                StatusDto status = await _mealRepository.DeleteMenuTemplateAsync(name);
                return status;
            }
            catch (Exception)
            {
                return new StatusDto(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        public async Task<(StatusDto, List<string>)> GetNamesOfTemplates()
        {
            try
            {
                var (status, names) = await _mealRepository.GetNamesOfTemplates();
                return (status, names);
            }
            catch (Exception)
            {
                return (new StatusDto(StatusCodes.Status500InternalServerError, "Server error"), null);
            }
        }

        public async Task<(StatusDto, List<MealDto>)> GetFilteredMeals(MealFilter mealFilter)
        {
            try
            {
                var (status, meals) = await _mealRepository.GetFilteredMeals(mealFilter);
                return (status, meals);
            }
            catch (Exception)
            {
                return (new StatusDto(StatusCodes.Status500InternalServerError, "Server error"), null);
            }
        }

        public async Task<StatusDto> AddFoodstuff(FoodstuffDto foodstuffDto)
        {
            try
            {
                StatusDto status = await _mealRepository.AddFoodstuff(foodstuffDto);
                return status;
            }
            catch (Exception)
            {
                return new StatusDto(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        public async Task<(StatusDto, List<string>)> GetFoodStuffsAsync()
        {
            try
            {
                var (status, foodStuffsNames) = await _mealRepository.GetFoodStuffsAsync();
                return (status, foodStuffsNames);
            }
            catch (Exception)
            {
                return (new StatusDto(StatusCodes.Status500InternalServerError, "Server error"), null);
            }
        }


    }
}
