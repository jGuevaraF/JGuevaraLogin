using System;
using System.Collections.Generic;

namespace DL;

public partial class Semestre
{
    public int IdSemestre { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Materium> Materia { get; set; } = new List<Materium>();
}
