using System;
using System.Collections.Generic;

namespace L01_2021AA602.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string? NombreCliente { get; set; }

    public string? Direccion { get; set; }
}
