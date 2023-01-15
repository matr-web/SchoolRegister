using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Models.Dto_s.UserDto_s;

public class UserDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }
}
