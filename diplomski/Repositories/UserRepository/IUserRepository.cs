using diplomski.Data.Dtos;

namespace diplomski.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task <StatusDto> AddUserData(UserInputDataDto data);
        Task<List<UserTableData>> GetDataForTableRows();
        Task<(StatusDto, UserInputDataDto)> GetUserDataByMail(string email);
        Task<StatusDto> DeleteUser(string email);
        Task<StatusDto> UpdateAdminDefaults(AdminDefaultDto adminDefaultDto);
        Task<(StatusDto, AdminDefaultDto)> GetAdminDefaults();
    }
}
