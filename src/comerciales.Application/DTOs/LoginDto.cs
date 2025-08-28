
using System.ComponentModel.DataAnnotations;

namespace comerciales.Application.Aut;

public class LoginDto
{

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

}
