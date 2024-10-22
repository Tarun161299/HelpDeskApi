namespace HelpDeskAPI.Data.Abstractions.Models
{
    public class GetTicketByUserAndStatus
    {
        public string? userId { get; set; }
        public string? status { get; set; }
    }
}
