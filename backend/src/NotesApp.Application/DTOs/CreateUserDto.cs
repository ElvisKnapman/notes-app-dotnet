using System;
using System.Collections.Generic;
using System.Text;

namespace NotesApp.Application.DTOs;

public class CreateUserDto
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
