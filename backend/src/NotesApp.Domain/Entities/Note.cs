using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities;

public class Note
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
