using diplomski.Data.Context;
using diplomski.Data.Dtos;
using diplomski.Data.Models;
using diplomski.Repositories.UserRepository;

namespace diplomski.Services.UserService
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<StatusDto> AddUserData(UserInputDataDto data)
        {
            try
            {
                var (statusGet, userData) = await _userRepository.GetUserDataByMail(data.Mail);

                if (userData == null)
                {
                    var statusCreate = await _userRepository.AddUserData(data);

                    return statusCreate;
                }

                return new StatusDto(StatusCodes.Status409Conflict, "User with that email already exists");
            }
            catch (Exception)
            {
                return new StatusDto(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        public async Task<StatusDto> DeleteUser(string email)
        {
            try
            {
                var statusDelete = await _userRepository.DeleteUser(email);
                return statusDelete;
            }
            catch (Exception)
            {
                //neki error code
                return new StatusDto(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

     

        public async Task<List<UserTableData>> GetDataForTableRows()
        {
            try
            {
                return await _userRepository.GetDataForTableRows();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<(StatusDto, UserInputDataDto)> GetUserDataByMail(string email)
        {
            try
            {
                var (statusGet, userData) = await _userRepository.GetUserDataByMail(email);

                return (statusGet, userData);
            }
            catch (Exception)
            {
                return (new StatusDto(StatusCodes.Status500InternalServerError, "Server error"), null);
            }
        }

        public async Task<StatusDto> UpdateAdminDefaults(AdminDefaultDto adminDefaultDto)
        {
            try
            {
                var statusUpdate = await _userRepository.UpdateAdminDefaults(adminDefaultDto);
                return statusUpdate;
            }
            catch (Exception)
            {
                //neki error code
                return new StatusDto(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        public async Task<(StatusDto, AdminDefaultDto)> GetAdminDefaults()
        {
            try
            {
                var (statusGet, adminDefaults) = await _userRepository.GetAdminDefaults();

                return (statusGet, adminDefaults);
            }
            catch (Exception)
            {
                return (new StatusDto(StatusCodes.Status500InternalServerError, "Server error"), null);
            }
        }
    }
}
