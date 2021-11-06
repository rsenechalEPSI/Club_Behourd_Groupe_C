using BehourdApp.ConsoleApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehourdApp.Test.Mock
{
    public static class ExcelData
    {
        public static List<Joueur> JoueursBuilder(int nbJoueur)
        {
            List<Joueur> joueurs = new List<Joueur>();
            Random r = new Random();
            for (int i = 0; i<nbJoueur; i++)
            {

                int anneeAleatoire = r.Next(1990, 2021);
                int poidsAleatoire = r.Next(60, 130);

                joueurs.Add(new Joueur()
                {
                    Prenom = "Joueur",
                    Nom = i.ToString(),
                    AnneeAdhesion = anneeAleatoire,
                    Poids = poidsAleatoire
                });
            }

            return joueurs;
        }
    }
}
