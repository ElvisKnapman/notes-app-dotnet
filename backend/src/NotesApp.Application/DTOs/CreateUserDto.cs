using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs;

public class CreateUserDto
{
    public string? Username { get; set; }
    public string? Email { get; set; }
}
