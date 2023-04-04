using System;
using System.Collections.Generic;

namespace WebDBApi.Models;

public partial class Employe
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public byte[] Email { get; set; } = null!;

    public DateTime Created { get; set; }
}
