using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehourdApp.ConsoleApp.Classes
{
    public class Session
    {
        public List<Partie> Parties { get; set; }

        public Session()
        {
            Parties = new List<Partie>();
        }

        public Partie CreatePartie(List<Joueur> joueurs)
        {
            if (joueurs.Count == 2)
            {
                Equipe e1 = new Equipe();
                Equipe e2 = new Equipe();

                e1.Joueurs.Add(joueurs.First());
                e2.Joueurs.Add(joueurs.Last());

                Partie partie = new Partie();
                partie.Equipe1 = e1;
                partie.Equipe2 = e2;

                Parties.Add(partie);
                return partie;
            }
            else
            {
                throw new Exception("Le nombre de joueurs est supérieur a 2.");
            }
        }

        public List<Equipe> GetEquipes()
        {
            List<Equipe> equipes = new List<Equipe>();
            if (Parties != null && Parties.Any())
            {
                foreach (Partie partie in Parties)
                {
                    equipes.Add(partie.Equipe1);
                    equipes.Add(partie.Equipe2);
                }
            }

            return equipes;
        }

        public void Equilibrage(List<Joueur> joueurs, Equipe e1, Equipe e2)
        {
            if (joueurs.Any())
            {
                List<Joueur> joueursEquipe1 = new List<Joueur>();
                List<Joueur> joueursEquipe2 = new List<Joueur>();
                joueurs = joueurs.OrderByDescending(j => j.Poids).ToList();

                for (int i = 0; i < joueurs.Count; i++)
                {
                    //if(i < (joueurs.Count / 2))
                    //{

                    //}
                    joueursEquipe1.Add(joueurs[i]);
                    joueursEquipe1.Add(joueurs[joueurs.Count-i]);
                }

            }

        }


    }
}
