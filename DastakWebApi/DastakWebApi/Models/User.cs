using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? UserCategory { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public string? CreatedBy { get; set; }

    public string? DeactivatedBy { get; set; }

    public short IsActive { get; set; }
}
