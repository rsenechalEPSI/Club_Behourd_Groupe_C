using BehourdApp.ConsoleApp.Classes;
using BehourdApp.Test.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BehourdApp.Test
{
    [TestClass]
    public class TestSession
    {
        [TestMethod]
        public void Creation_Joueur()
        {
            Joueur joueur = new Joueur();
            Assert.IsNotNull(joueur);
        }

        [TestMethod]
        public void Creation_Duel()
        {

            List<Joueur> joueurs = ExcelData.JoueursBuilder(2);

            Session session = new Session();

            Partie partie = session.CreatePartie(joueurs);

            List<Equipe> equipes = session.GetEquipes();

            Assert.AreEqual(2, equipes.Count);

            Joueur j1 = joueurs.First();
            Joueur j2 = joueurs.Last();
            Assert.AreNotSame(j1, j2);


        }

        public static List<List<List<T>>> GetAllCombos<T>(List<T> list)
        {
            int comboCount = (int)Math.Pow(2, list.Count) - 1;
            List<List<T>> result = new List<List<T>>();
            for (int i = 1; i < comboCount + 1; i++)
            {
                // make each combo here
                result.Add(new List<T>());
                for (int j = 0; j < list.Count; j++)
                {
                    if ((i >> j) % 2 != 0)
                        result.Last().Add(list[j]);
                }
            }

            List<List<List<T>>> vraiReturn = new List<List<List<T>>>();
            for (int i = 0; i < result.Count; i++)
            {
                List<List<T>> pair = new List<List<T>>();
                pair.Add(result[i]);
                pair.Add(list.Except(result[i]).ToList());
                vraiReturn.Add(pair);
            } 

            return vraiReturn;
        }

        [TestMethod]
        public void Verification_Equilibrage_Equipes()
        {
            List<Joueur> joueurs = ExcelData.JoueursBuilder(6);

            Session session = new Session();

            Partie partie = session.CreatePartie(joueurs);

            // Récupération de toutes les équipes possibles à partir de la liste des joueurs
            List<List<List<Joueur>>> toutesLesEquipesPossibles = GetAllCombos(joueurs);

            //Récupération des équipes crées avec l'algorithme
            List<Equipe> equipes = session.GetEquipes();

            List<List<Joueur>> listeDesJoueurs = TransformListEquipeInListJoueur(equipes);

            // Suppression des équipes dont l'écart de joueurs n'est pas le minimal
            // (si le nombre de joueurs est pair, l'écart est de 0, si il est impair, 1)
            toutesLesEquipesPossibles = toutesLesEquipesPossibles.Where(equipe => Math.Abs(equipe[0].Count() - equipe[1].Count()) <= 1).ToList();

            // Détermintion des ecarts les plus petits possibles pour les moyennes de poids sur les équipes restantes et filtrage
            float ecartMoyennePoidsMin = float.MaxValue;

            List<List<List<Joueur>>> equipesFiltreeParPoids = new List<List<List<Joueur>>>();

            for (int i = 0; i < toutesLesEquipesPossibles.Count(); i++)
            {
                int sommePoidsEquipe1 = toutesLesEquipesPossibles[i][0].Sum(joueur => joueur.Poids);
                int sommePoidsEquipe2 = toutesLesEquipesPossibles[i][1].Sum(joueur => joueur.Poids);

                float ecartMoyennePoids = Math.Abs((sommePoidsEquipe1 / toutesLesEquipesPossibles[i][0].Count()) - (sommePoidsEquipe2 / toutesLesEquipesPossibles[i][1].Count()));

                if (ecartMoyennePoids < ecartMoyennePoidsMin)
                {
                    ecartMoyennePoidsMin = ecartMoyennePoids;
                    equipesFiltreeParPoids.Clear();
                }

                if (ecartMoyennePoids == ecartMoyennePoidsMin)
                {
                    equipesFiltreeParPoids.Add(toutesLesEquipesPossibles[i]);
                }

            }

            // Détermintion des ecarts les plus petits possibles pour les moyennes d'année d'adésion sur les équipes restantes et filtrage
            float ecartMoyenneAnneeAdhesionMin = float.MaxValue;

            List<List<List<Joueur>>> equipesFiltreeParAnnee = new List<List<List<Joueur>>>();

            for (int i = 0; i < equipesFiltreeParPoids.Count(); i++)
            {
                int sommeAnneeAdhesionEquipe1 = equipesFiltreeParPoids[i][0].Sum(joueur => joueur.AnneeAdhesion);
                int sommeAnneeAdhesionEquipe2 = equipesFiltreeParPoids[i][1].Sum(joueur => joueur.AnneeAdhesion);

                float ecartMoyenneAnneeAdesion = Math.Abs((sommeAnneeAdhesionEquipe1 / toutesLesEquipesPossibles[i][0].Count()) - (sommeAnneeAdhesionEquipe2 / toutesLesEquipesPossibles[i][1].Count()));

                if (ecartMoyenneAnneeAdesion < ecartMoyenneAnneeAdhesionMin)
                {
                    ecartMoyenneAnneeAdhesionMin = ecartMoyenneAnneeAdesion;
                    equipesFiltreeParAnnee.Clear();
                }

                if (ecartMoyenneAnneeAdesion == ecartMoyenneAnneeAdhesionMin)
                {
                    equipesFiltreeParAnnee.Add(toutesLesEquipesPossibles[i]);
                }
            }

            // Tester si l'équipe produite est dans equipesFiltreeParAnnee

            Assert.IsNotNull(equipesFiltreeParAnnee);

            bool contiensUneEquipeIdentique = false;
            
            foreach(List<List<Joueur>> equipesPossiblesATester in equipesFiltreeParAnnee)
            {
                var test = equipesPossiblesATester.All(listeDesJoueurs.Contains);
                if (equipesPossiblesATester.SequenceEqual(listeDesJoueurs))
                {
                    contiensUneEquipeIdentique = true;
                }

            }

            Assert.IsTrue(contiensUneEquipeIdentique);

        }

        private List<List<Joueur>> TransformListEquipeInListJoueur(List<Equipe> equipes)
        {
            List<List<Joueur>> listeDesJoueurs = new List<List<Joueur>>();

            foreach(Equipe equipe in equipes)
            {
                listeDesJoueurs.Add(equipe.Joueurs);
            }

            return listeDesJoueurs;

        }

        [TestMethod]
        public void Creation_16_vs_16()
        {
            List<Joueur> joueurs = ExcelData.JoueursBuilder(32);

            Session session = new Session();

            Partie partie = session.CreatePartie(joueurs);
            List<Equipe> equipes = session.GetEquipes();
            
            Assert.IsNotNull(session);
            Assert.IsNotNull(partie);

            Assert.AreEqual(2, equipes.Count);

            Assert.AreEqual(16, equipes[0].Joueurs.Count);
            Assert.AreEqual(16, equipes[1].Joueurs.Count);

            foreach(Joueur joueurEquipe1 in equipes[0].Joueurs)
            {
                foreach (Joueur joueurEquipe2 in equipes[1].Joueurs)
                {
                    Assert.AreNotSame(joueurEquipe1, joueurEquipe2);
                }
            }

        }

        [TestMethod]
        public void Creation_150_vs_150()
        {
            List<Joueur> joueurs = ExcelData.JoueursBuilder(300);

            Session session = new Session();

            Partie partie = session.CreatePartie(joueurs);
            List<Equipe> equipes = session.GetEquipes();

            Assert.IsNotNull(session);
            Assert.IsNotNull(partie);

            Assert.AreEqual(2, equipes.Count);

            Assert.AreEqual(16, equipes[0].Joueurs.Count);
            Assert.AreEqual(16, equipes[1].Joueurs.Count);

            foreach (Joueur joueurEquipe1 in equipes[0].Joueurs)
            {
                foreach (Joueur joueurEquipe2 in equipes[1].Joueurs)
                {
                    Assert.AreNotSame(joueurEquipe1, joueurEquipe2);
                }
            }

        }
    }
}
