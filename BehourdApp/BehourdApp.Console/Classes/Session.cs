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
            Equipe e1 = new Equipe();
            Equipe e2 = new Equipe();

            Equilibrage(joueurs, e1, e2);

            Partie partie = new Partie();
            partie.Equipe1 = e1;
            partie.Equipe2 = e2;

            Parties.Add(partie);
            return partie;

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

        //Fonction récursive, qui va être appellée jusqu'a que les deux équipes soient équilibrées par poids et par ancienneté
        public void Equilibrage(List<Joueur> joueurs, Equipe e1, Equipe e2, float ecartMoyennePoidsOrigin = 0, float ecartMoyenneAnneeOrigin = 0,  bool firstFilter = true, List<Joueur> joueursEquipe1Save = null, List<Joueur> joueursEquipe2Save = null)
        {
            if (joueurs.Any())
            {
                List<Joueur> joueursEquipe1 = new List<Joueur>();
                List<Joueur> joueursEquipe2 = new List<Joueur>();

                //Si c'est le premier appel de la fonction, permet de trier la liste des joueurs par poids, puis par année d'adhésion
                if (firstFilter)
                {
                    joueurs = joueurs.OrderBy(j => j.Poids).ThenBy(j => j.AnneeAdhesion).ToList();

                }

                int totalJoueurs = joueurs.Count;

                if (totalJoueurs == 2)
                {
                    e1.Joueurs.Add(joueurs.First());
                    e2.Joueurs.Add(joueurs.Last());
                    return;
                }

                //Les joueurs étant triés par poids et par année d'adhésion,
                //nous allons prendre les plus légers et les moins expérimentés, pour les mettre avec les plus lourds et plus expérimentés
                while (joueurs.Any())
                {
                    if (joueursEquipe1.Count < (totalJoueurs / 2))
                    {
                        joueursEquipe1.Add(joueurs.First());
                        joueurs.Remove(joueurs.First());
                        if (joueursEquipe1.Count + 1 < (totalJoueurs / 2))
                        {
                            joueursEquipe1.Add(joueurs.Last());
                            joueurs.Remove(joueurs.Last());
                        }
                    }
                    else
                    {
                        joueursEquipe2.Add(joueurs.First());
                        joueurs.Remove(joueurs.First());

                    }
                }

                int sommePoidsEquipe1 = joueursEquipe1.Sum(joueur => joueur.Poids);
                int sommePoidsEquipe2 = joueursEquipe2.Sum(joueur => joueur.Poids);

                int sommeAdhesionEquipe1 = joueursEquipe1.Sum(joueur => joueur.AnneeAdhesion);
                int sommeAdhesionEquipe2 = joueursEquipe2.Sum(joueur => joueur.AnneeAdhesion);

                float ecartMoyennePoids = Math.Abs((sommePoidsEquipe1 / joueursEquipe1.Count) - (sommePoidsEquipe2 / joueursEquipe2.Count));
                float ecartMoyenneAdhesion = Math.Abs((sommeAdhesionEquipe1 / joueursEquipe1.Count) - (sommeAdhesionEquipe2 / joueursEquipe2.Count));

                //Nous vérifions ici que les écarts de moyenne sont moins importants que les précédents, si ce n'est pas le cas, nous rappellons la fonction
                if (ecartMoyennePoidsOrigin != 0 && ecartMoyennePoids < ecartMoyennePoidsOrigin && ecartMoyenneAnneeOrigin != 0 && ecartMoyenneAdhesion < ecartMoyenneAnneeOrigin)
                {
                    e1.Joueurs = joueursEquipe1;
                    e2.Joueurs = joueursEquipe2;
                }
                else
                {
                    List<Joueur> equipesConfondues = new List<Joueur>();
                    joueursEquipe1.ForEach(j => equipesConfondues.Add(j));
                    joueursEquipe2.ForEach(j => equipesConfondues.Add(j));

                    Equilibrage(equipesConfondues, e1, e2, ecartMoyennePoids, ecartMoyenneAdhesion, false, joueursEquipe1, joueursEquipe2);
                }
            }
        }
    }
}
