using BehourdApp.ConsoleApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehourdApp.Test.Mock
{
    public class ExcelData
    {
        public List<Joueur> Joueurs { get; set; }

        public ExcelData()
        {
            Joueur j1 = new Joueur()
            {
                Nom = "Sénéchal",
                Prenom = "Romain",
                AnneeAdhesion = 1999,
                Poids = 250,
            };
            Joueur j2 = new Joueur()
            {
                Nom = "Dauchez",
                Prenom = "Clément",
                AnneeAdhesion = 1999,
                Poids = 35,
            };

            this.Joueurs.Add(j1);
            this.Joueurs.Add(j2);
        }
    }
}
