using System.ComponentModel.DataAnnotations;

namespace diplomski.Data.Models
{
    public class AdminDefault
    {
        [Key]
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string UnpersonalizedText { get; set; }
        public string DocumentName { get; set; }
        public string? WeightLossText { get; set; }
        public string? FatteningText { get; set; }
        public string? KeepingFitText { get; set; }
    }
}
