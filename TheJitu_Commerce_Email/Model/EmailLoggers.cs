using System.ComponentModel.DataAnnotations;

namespace TheJitu_Commerce_Email.Model
{
    public class EmailLoggers
    {
        [Key]
        public Guid EmailLoggerId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty; 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
