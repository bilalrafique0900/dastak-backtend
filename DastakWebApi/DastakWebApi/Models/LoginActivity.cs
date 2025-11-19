using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class LoginActivity
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Passcode { get; set; }

    public DateTime? CreatedAt { get; set; }
}
