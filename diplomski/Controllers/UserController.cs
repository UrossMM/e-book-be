using diplomski.Data.Dtos;
using diplomski.Services.EmailService;
using diplomski.Services.MealService;
using diplomski.Services.UserService;
using diplomski.Services.WordGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diplomski.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IMealService _mealService;
        public readonly IHtmlGeneratorService _htmlGeneratorService;
        public readonly IEmailService _emailService;
        public readonly IUserService _userService;
        public UserController(IMealService mealService, IHtmlGeneratorService htmlGeneratorService, IEmailService emailService, IUserService userService)
        {
            _mealService = mealService;
            _htmlGeneratorService = htmlGeneratorService;
            _emailService = emailService;
            _userService = userService;
        }

        [HttpPost]
        [ActionName("create")]
        public async Task<IActionResult> AddUserData(UserInputDataDto userInputData)
        {
            var statusCreate = await _userService.AddUserData(userInputData);
            if (statusCreate.Status)
                return StatusCode(StatusCodes.Status200OK);

            return StatusCode(statusCreate.StatusCode, new { Errors = statusCreate.Errors });
        }

        [Authorize]
        [HttpGet]
        [ActionName("rows")]
        public async Task<IActionResult> GetDataForTableRows()
        { //da vratis i cilj korisnika da bi admin znao da li da pretrazuje u plus ili minus ako treba da menja neko jelo
            var tableData = await _userService.GetDataForTableRows();

            return StatusCode(StatusCodes.Status200OK, tableData);
        }

        [Authorize]
        [HttpGet]
        [ActionName("html/{email}")]
        public async Task<IActionResult> GetHtmlForConcreteRow([FromRoute] string email)
        {
            var (statusGetUserData, userInputData) = await _userService.GetUserDataByMail(email);
            if(statusGetUserData.Status)
            {
                var (statusGetMeals, meals) = await _mealService.GetPersonalizedMealsAsync(userInputData);
                if(statusGetMeals.Status)
                {
                    var (statusGetAdminDefaults, adminDefaultsDto) = await _userService.GetAdminDefaults();

                    if (!statusGetAdminDefaults.Status)
                        return StatusCode(statusGetAdminDefaults.StatusCode, new { Errors = statusGetAdminDefaults.Errors });

                    string html = _htmlGeneratorService.GenerateHtmlCode(meals, adminDefaultsDto, userInputData.Goal);

                    return StatusCode(StatusCodes.Status200OK, html);
                }

                return StatusCode(statusGetMeals.StatusCode, new { Errors = statusGetMeals.Errors });
            }

            return StatusCode(statusGetUserData.StatusCode, new { Errors = statusGetUserData.Errors });
        }

        [Authorize]
        [HttpPost]
        [ActionName("sendMail")]
        public async Task<IActionResult> SendPersonalInfoOnEmail([FromBody] EmailDto email)
        {
            //ova funkcija se izvrsava kada admin ide na submit (save) dela sa tekstom koje je menjao ili nije menjao


            ////na osnovu parametra izvuce odredjene obroke - nova funkcija u mealservice
            //var (status, meals) = await _mealService.GetPersonalizedMealsAsync(userInputData);
            //// kreira pdf ili word preko wordGeneratorServie
            //MemoryStream document = _htmlGeneratorService.GeneratePersonalizedDocument(userInputData.Mail, meals);
            //string name = "trening.doc";

            ////return new FileStreamResult(document, "application/msword") { FileDownloadName = name };
            ////posalje ga na mejl - napraviti mailservice
            //string body = "Pripremili smo plan ishrane u odnosu na podatke koje ste uneli. Otvorite fajl i uzivajte u zdravoj hrani. Srecno!";
            //_emailService.SendEMail("urossmm111@outlook.com", userInputData.Mail, "Plan ishrane po Vasoj meri je stigao", body, document);
            ////vrati povratnu vrednost
            //return Ok();
            var (statusGet, adminDefaultsDto) = await _userService.GetAdminDefaults();
            if(!statusGet.Status)
                return StatusCode(statusGet.StatusCode, new {Errors = statusGet.Errors});

            Byte[] res = null;
            //using (MemoryStream ms = new MemoryStream())

            MemoryStream ms = new MemoryStream();
            var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(email.HtmlPage, PdfSharp.PageSize.A4);
            pdf.Save(ms);
            res = ms.ToArray();

            _emailService.SendEMail(adminDefaultsDto.EmailAddress, email.Email, adminDefaultsDto.EmailSubject, adminDefaultsDto.EmailBody, adminDefaultsDto.DocumentName, res);

            return StatusCode(StatusCodes.Status200OK);
        }
        //----------------------------------------------------------------------- ovo gore radi
        //sad pokusaj da taj word upamtis u bazu zajedno sa mejlom korisnika i posle da ga pribavis 

        [Authorize]
        [HttpDelete]
        [ActionName("userData/{email}")]
        public async Task<IActionResult> DeleteUserData([FromRoute] string email)
        {
            var statusDelete = await _userService.DeleteUser(email);
            if (statusDelete.Status)
                return StatusCode(StatusCodes.Status200OK);
            
            return StatusCode(statusDelete.StatusCode, new { Errors = statusDelete.Errors });
        }

        [Authorize]
        [HttpPut]
        [ActionName("adminDefaults")]
        public async Task<IActionResult> UpdateAdminDefaults(AdminDefaultDto adminDefaultDto)
        {
            var statusUpdate = await _userService.UpdateAdminDefaults(adminDefaultDto);
            if(statusUpdate.Status)
                return StatusCode(StatusCodes.Status200OK);

            return StatusCode(statusUpdate.StatusCode, new { Errors = statusUpdate.Errors });
        }

        [Authorize]
        [HttpGet]
        [ActionName("adminDefaults")]
        public async Task<IActionResult> GetAdminDefaults()
        {
            var (statusGet, adminDefaultsDto)  = await _userService.GetAdminDefaults();
            if (statusGet.Status)
                return StatusCode(StatusCodes.Status200OK, adminDefaultsDto);

            return StatusCode(statusGet.StatusCode, new {Errors = statusGet.Errors });
        }
    }
}