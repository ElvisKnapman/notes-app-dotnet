using System;
using System.Collections.Generic;
using System.Text;

namespace NotesApp.Application.DTOs;

public record UpdateUserDto(string? Username, string? Email);