using System;
using System.Collections.Generic;

namespace Core.Model;

public partial class User
{
    public string Userid { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Img { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
}
