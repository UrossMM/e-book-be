using diplomski.Data;
using diplomski.Data.Dtos;
using diplomski.Services.MealService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diplomski.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        public IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        //[HttpGet]
        //[ActionName("{id}")]
        //public async Task<IActionResult> GetMealById([FromRoute] int id)
        //{
        //    var (status, meal) = await _mealService.GetMealAsync(id);
        //    if (status.Status)
        //        return StatusCode(StatusCodes.Status200OK, meal);

        //    return StatusCode(status.StatusCode, new {Errors = status.Errors});

        //}

        //[HttpGet]
        //[ActionName("all")]
        //public async Task<IActionResult> GetAllMeals()
        //{
        //    var meals = await _mealService.GetAllMealsAsync();
                
        //    return StatusCode(StatusCodes.Status200OK, meals);
        //}

        //[HttpPost]
        //[ActionName("")]
        //public async Task<IActionResult> CreateMeal([FromForm] MealDto meal) //zbog slike je from form
        //{
        //    StatusDto status = await _mealService.AddMealAsync(meal);
        //    if (status.Status)
        //        return StatusCode(StatusCodes.Status200OK);

        //    return StatusCode(status.StatusCode, new { Errors = status.Errors });
        //}

        //[HttpPut]
        //[ActionName("{id}")]
        //public async Task<IActionResult> UpdateMeal([FromRoute] int id, [FromBody] MealDto meal)
        //{
        //    StatusDto status = await _mealService.UpdateMealAsnyc(id, meal);
        //    if (status.Status)
        //        return StatusCode(StatusCodes.Status200OK);

        //    return StatusCode(status.StatusCode, new { Errors = status.Errors });
        //}

        //[HttpDelete]
        //[ActionName("{id}")]
        //public async Task<IActionResult> DeleteMealById([FromRoute] int id)
        //{
        //    StatusDto status = await _mealService.DeleteMealAsync(id);
        //    if (status.Status)
        //        return StatusCode(StatusCodes.Status200OK);

        //    return StatusCode(status.StatusCode, new { Errors = status.Errors });
        //}

        [AllowAnonymous]
        [HttpGet]
        [ActionName("additions")]
        public async Task<IActionResult> GetAdditions()
        {
            var (status, additions) = await _mealService.GetAdditionsAsync();
            if (status.Status)
                return StatusCode(StatusCodes.Status200OK, additions);

            return StatusCode(status.StatusCode, new { Errors = status.Errors });
        }

        [Authorize]
        [HttpPost]
        [ActionName("template")]
        public async Task<IActionResult> AddMenuTemplateAsync(TemplateDto templateDto)
        {
            StatusDto status = await _mealService.AddMenuTemplateAsync(templateDto);
            if (status.Status)
                return StatusCode(StatusCodes.Status200OK);

            return StatusCode(status.StatusCode, new { Errors = status.Errors });
        }

        [Authorize]
        [HttpDelete]
        [ActionName("template/{name}")]
        public async Task<IActionResult> DeleteMenuTemplateAsync([FromRoute]string name)
        {
            StatusDto status = await _mealService.DeleteMenuTemplateAsync(name);
            if (status.Status)
                return StatusCode(StatusCodes.Status200OK);

            return StatusCode(status.StatusCode, new { Errors = status.Errors });
        }

        [Authorize]
        [HttpGet]
        [ActionName("template")]
        public async Task<IActionResult> GetNamesOfTemplates()
        {
            var (status, names) = await _mealService.GetNamesOfTemplates();
            if (status.Status)
                return StatusCode(StatusCodes.Status200OK, names);

            return StatusCode(status.StatusCode, new { Errors = status.Errors });
        }

        [Authorize]
        [HttpGet]
        [ActionName("filterMeals")]
        public async Task<IActionResult> GetFilteredMeals([FromQuery] MealFilter mealFilter)
        {
            var (status, meals) = await _mealService.GetFilteredMeals(mealFilter);
            if (status.Status)
                return StatusCode(StatusCodes.Status200OK, meals);

            return StatusCode(status.StatusCode, new { Errors = status.Errors });
        }

        [Authorize]
        [HttpPost]
        [ActionName("foodstuff")]
        public async Task<IActionResult> AddFoodstuff(FoodstuffDto foodstuffDto)
        {
            StatusDto status = await _mealService.AddFoodstuff(foodstuffDto);
            if (status.Status)
                return StatusCode(StatusCodes.Status200OK);

            return StatusCode(status.StatusCode, new { Errors = status.Errors });
        }

        [Authorize]
        [HttpGet]
        [ActionName("foodstuffsNames")]
        public async Task<IActionResult> GetFoodStuffsAsync()
        {
            var (status, foodstuffsNames) = await _mealService.GetFoodStuffsAsync();
            if (status.Status)
                return StatusCode(StatusCodes.Status200OK, foodstuffsNames);

            return StatusCode(status.StatusCode, new { Errors = status.Errors });
        }
    }
}
