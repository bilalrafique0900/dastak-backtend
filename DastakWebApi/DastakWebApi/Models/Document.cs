using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class Document
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? ListOfDocuments { get; set; }

    public short? Photocopied { get; set; }
}
