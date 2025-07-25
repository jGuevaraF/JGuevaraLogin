using System;
using System.Collections.Generic;

namespace DL;

public partial class MateriaGetAllView
{
    public int IdMateria { get; set; }

    public string? NombreMateria { get; set; }

    public decimal? Creditos { get; set; }

    public decimal? Costo { get; set; }

    public string? Fecha { get; set; }

    public bool? Status { get; set; }

    public int? IdSemestre { get; set; }
}
