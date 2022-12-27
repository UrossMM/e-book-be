using diplomski.Data.Context;
using diplomski.Data.Dtos;
using diplomski.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace diplomski.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        public ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<StatusDto> AddUserData(UserInputDataDto data) //probaj za sva polja da uneses razne vrednosti i cudne vrednosti i null da vidis kako ce da radi, a validacije dodaj u servisu
        {
            UserData forCreate = new UserData()
            {
                ActivityLevel = data.ActivityLevel,
                Additions = data.Additions,
                DailyNumberOfMeals = data.DailyNumberOfMeals,
                Age = data.Age,
                Gender = data.Gender,
                Goal = data.Goal,
                Height = data.Height,
                Mail = data.Mail,
                Number = data.Number,
                Weight = data.Weight,
                DateCreated = DateTime.UtcNow.AddHours(2)
            };

            _dbContext.Add(forCreate);

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

        public async Task<StatusDto> DeleteUser(string email)
        {
            UserData? data = await _dbContext.UserDatas.FirstOrDefaultAsync(x => x.Mail == email);
            if (data == null)
            {
                return new StatusDto(StatusCodes.Status404NotFound, "User not found.");
            }

            _dbContext.UserDatas.Remove(data);
            try
            {
                await _dbContext.SaveChangesAsync();
                return new StatusDto();
            }
            catch (Exception)
            {
                return new StatusDto(StatusCodes.Status500InternalServerError, "Server error while deleting user");
            }
        }



        public async Task<List<UserTableData>> GetDataForTableRows()
        {
            //List<UserTableData> userTableData =  _dbContext.UserDatas
            //    .Select(x => new UserTableData { DateCreated = x.DateCreated, Mail = x.Mail }).ToList();

            //return userTableData;
            //List<UserTableData> userTableData = new List<UserTableData>();

            //await Task.Run(async () =>
            //{
            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("sr-Latn-CS");
            List<UserTableData> userTableData = await _dbContext.UserDatas
                   .AsNoTracking()
                   .OrderByDescending(x => x.DateCreated)
                   .Select(x => new UserTableData
                   {
                       DateCreated = x.DateCreated.ToString("dddd, dd MMMM yyyy HH:mm", cultureinfo),
                       Mail = x.Mail,
                       Goal = x.Goal.ToString(),
                       Number = x.Number,
                       Additions = x.Additions
                   })
                   .ToListAsync();

            //});

            return userTableData;
        }

        public async Task<(StatusDto, UserInputDataDto)> GetUserDataByMail(string email)
        {
            UserInputDataDto? userInputData = await _dbContext.UserDatas.Where(x => x.Mail == email)
                .Select(x => new UserInputDataDto
                {
                    ActivityLevel = x.ActivityLevel,
                    Additions = x.Additions,
                    Age = x.Age,
                    DailyNumberOfMeals = x.DailyNumberOfMeals,
                    Gender = x.Gender,
                    Goal = x.Goal,
                    Height = x.Height,
                    Mail = x.Mail,
                    Number = x.Number,
                    Weight = x.Weight
                })
                .FirstOrDefaultAsync();

            if (userInputData == null)
                return (new StatusDto(StatusCodes.Status404NotFound, "User not found"), null);

            return (new StatusDto(), userInputData);
        }

        public async Task<StatusDto> UpdateAdminDefaults(AdminDefaultDto adminDefaultsDto)
        {
            AdminDefault? adminDefaults = await _dbContext.AdminDefaults.FirstOrDefaultAsync();

            adminDefaults.UnpersonalizedText = adminDefaultsDto.UnpersonalizedText;
            adminDefaults.EmailAddress = adminDefaultsDto.EmailAddress;
            adminDefaults.EmailSubject = adminDefaultsDto.EmailSubject;
            adminDefaults.DocumentName = adminDefaultsDto.DocumentName;
            adminDefaults.EmailBody = adminDefaultsDto.EmailBody;
            adminDefaults.WeightLossText = adminDefaultsDto.WeightLossText;
            adminDefaults.FatteningText = adminDefaultsDto.FatteningText;
            adminDefaults.KeepingFitText = adminDefaultsDto.KeepingFitText;

            _dbContext.AdminDefaults.Update(adminDefaults);

            try
            {
                await _dbContext.SaveChangesAsync();
                return new StatusDto();
            }
            catch(Exception)
            {
                return new StatusDto(StatusCodes.Status500InternalServerError, "Error while updating data");
            }
        }

        public async Task<(StatusDto, AdminDefaultDto)> GetAdminDefaults()
        {
            AdminDefaultDto? adminDefaultsDto = await _dbContext.AdminDefaults.Select(x => new AdminDefaultDto
            {
                EmailAddress = x.EmailAddress,
                EmailBody = x.EmailBody,
                EmailSubject = x.EmailSubject,
                DocumentName = x.DocumentName,
                UnpersonalizedText = x.UnpersonalizedText,
                FatteningText = x.FatteningText ?? "",
                WeightLossText = x.WeightLossText ?? "",
                KeepingFitText = x.KeepingFitText ?? ""
            }).FirstOrDefaultAsync();

            if (adminDefaultsDto == null)
            {
                AdminDefault adminDefault = new AdminDefault()
                {
                    UnpersonalizedText = "Pravilna, zdrava ishrana je veoma važna za stanje organizma i direktno utiče na radnu sposobnost i dužinu života. Nažalost, današnju ishranu karakterišu " +
                    "nepravilni i nedovoljno izbalansirani obroci. U njima ima previše masnoća, mesa, hleba, a malo mlečnih proizvoda, voća i povrća. Nepravilna ishrana je faktor rizika za nastanak " +
                    "različitih oboljenja. Zbog toga trpi čitav organizam, a veoma je lako izbeći mnoge komplikacije jednostavnom promenom ishrane. Zato je vreme da stavite tačku i promenite stvari. " +
                    "\n" + 
                    "Zdrava i pravilna ishrana ima veliki uticaj na zdrav život. Nije teško odrediti osnovne smernice zdravog života i pridržavati ih se da bi zadržali zdravlje i osećali se prijatno u " +
                    "svom telu. Hrana u tome svakako može pomoći. Koliko god komplikovano izgledalo, hraniti se zdravo je vrlo jednostavno." +
                    "U prilogu se nalazi vas jedinstveni plan ishrane kreiran po vasim podacima koje ste uneli. " +
                    "Uz obroke koji su preporuceni za Vasu ishranu, zelimo da napomenemo sta nikako ne biste trebali da unosite u svoj organizam ukoliko zelite da postujete principe zdrave ishrane. " +
                    "To su alkoholna i gazirana pica, seceri, izuzetno masne namernice, hrana przena sa puno masti, hrana przena na rafinisanom ulju kao i namernice koje su  veoma bogate vlaknima. " +
                    "Takodje, uz zdravu ishranu preporucujemo spavanje od 8 casova kako bi se vase telo u potpunosti odmorilo.",
                   
                    EmailAddress = "urossmm111@outlook.com",
                    DocumentName = "Plan.pdf",
                    EmailSubject = "Vas plan ishrane je stigao!",
                    EmailBody = "U prilogu se nalazi plan ishrane koji smo kreirali iskljucivo za vas."
                };

                _dbContext.AdminDefaults.Add(adminDefault);

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return (new StatusDto(StatusCodes.Status500InternalServerError, "Error while insert data."), adminDefaultsDto);
                }

                adminDefaultsDto = new AdminDefaultDto();

                adminDefaultsDto.UnpersonalizedText = adminDefault.UnpersonalizedText;
                adminDefaultsDto.EmailAddress = adminDefault.EmailAddress;
                adminDefaultsDto.EmailSubject = adminDefault.EmailSubject;
                adminDefaultsDto.DocumentName = adminDefault.DocumentName;
                adminDefaultsDto.EmailBody = adminDefault.EmailBody;

            }

            return (new StatusDto(), adminDefaultsDto);
        }
    }
}
