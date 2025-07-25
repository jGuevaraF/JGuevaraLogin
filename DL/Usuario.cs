﻿using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int IdRol { get; set; }

    public virtual Rol IdRolNavigation { get; set; } = null!;
}
