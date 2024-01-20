using System;
using System.Collections.Generic;

namespace Core.Model;

public partial class Article
{
    public string Codearticle { get; set; } = null!;

    public string Numcommande { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal? Quantite { get; set; }

    public decimal? Prix { get; set; }

    public virtual ICollection<Commande> Numcommandes { get; set; } = new List<Commande>();
}
