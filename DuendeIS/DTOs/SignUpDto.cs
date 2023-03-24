using System.ComponentModel.DataAnnotations;

namespace FinalMS.DuendeIS.DTOs;

public class SignupDto
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string City { get; set; }
}
