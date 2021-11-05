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

        public void Equilibrage(List<Joueur> joueurs, Equipe e1, Equipe e2, int totalMoyenne = 0, bool firstFilter = true)
        {
            if (joueurs.Any())
            {
                List<Joueur> joueursEquipe1 = new List<Joueur>();
                List<Joueur> joueursEquipe2 = new List<Joueur>();

                if (firstFilter)
                {
                    joueurs = joueurs.OrderBy(j => j.Poids).ToList();
                }

                for (int i = 0; i < joueurs.Count; i++)
                {
                    if (joueursEquipe1.Count < (joueurs.Count / 2))
                    {
                        joueursEquipe1.Add(joueurs[i]);
                        if(joueursEquipe1.Count + 1 < (joueurs.Count / 2))
                        {
                            joueursEquipe1.Add(joueurs[(joueurs.Count - 1) - i]);

                        }
                    }
                    else
                    {
                        joueursEquipe2.Add(joueurs[i]);
                    }
                }

                int moyenneEquipe1 = 0;
                int moyenneEquipe2 = 0;
                foreach (Joueur joueur in joueursEquipe1)
                {
                    moyenneEquipe1 += joueur.Poids;
                }
                foreach (Joueur joueur in joueursEquipe2)
                {
                    moyenneEquipe2 += joueur.Poids;
                }

                moyenneEquipe1 = moyenneEquipe1 / joueursEquipe1.Count;
                moyenneEquipe2 = moyenneEquipe2 / joueursEquipe2.Count;

                int newTotalMoyenne = moyenneEquipe1 - moyenneEquipe2;


                List<Joueur> equipesConfondues = new List<Joueur>();
                joueursEquipe1.ForEach(j => equipesConfondues.Add(j));
                joueursEquipe2.ForEach(j => equipesConfondues.Add(j));

                if (totalMoyenne != 0)
                {
                    if (totalMoyenne < newTotalMoyenne)
                    {
                        e1.Joueurs = joueursEquipe1;
                        e2.Joueurs = joueursEquipe2;
                    }
                    else
                    {
                        if(newTotalMoyenne < -10 && newTotalMoyenne > 10)
                        {
                            e1.Joueurs = joueursEquipe1;
                            e2.Joueurs = joueursEquipe2;
                        }
                        else
                        {
                            Equilibrage(equipesConfondues, e1, e2, newTotalMoyenne, false);
                        }
                    }
                }


            }

        }


    }
}
