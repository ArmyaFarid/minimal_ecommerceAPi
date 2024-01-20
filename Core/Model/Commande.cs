using System;
using System.Collections.Generic;

namespace Core.Model;

public partial class Commande
{
    public string Numcommande { get; set; } = null!;

    public string Userid { get; set; } = null!;

    public DateTime? Datecommande { get; set; }

    public DateTime? Datelivraison { get; set; }

    public decimal? Prixttc { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Article> Codearticles { get; set; } = new List<Article>();
}
