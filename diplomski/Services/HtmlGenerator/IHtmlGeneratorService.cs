using diplomski.Data.Dtos;

namespace diplomski.Services.WordGenerator
{
    public interface IHtmlGeneratorService
    {
        string GenerateHtmlCode(List<MealDto> meals, AdminDefaultDto adminDefaultsDto, Goal goal);
    }
}
