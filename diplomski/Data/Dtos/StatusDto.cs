namespace diplomski.Data.Dtos
{
    public class StatusDto
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public StatusDto()
        {
            Status = true;
            StatusCode = StatusCodes.Status200OK;
        }

        public StatusDto(int code, string error)
        {
            Status = false;
            StatusCode = code;
            Errors.Add(error);
        }

        public StatusDto(int code, List<string> errors)
        {
            Status = false;
            StatusCode = code;
            Errors.AddRange(errors);
        }
    }
}
