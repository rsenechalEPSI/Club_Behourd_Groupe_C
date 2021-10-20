using BehourdApp.Test.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Linq;
=======
using BehourdApp.ConsoleApp;
>>>>>>> 8075630 (test équilibrage rouge pas fini)

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

<<<<<<< HEAD
        [TestMethod]
        public void Creation_10_vs_10()
        {

            List<Joueur> joueurs = ExcelData.JoueursBuilder(20);
=======
        public static List<List<T>> GetAllCombos<T>(List<T> list)
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
            return result;
        }

        [TestMethod]
        public void Test_Equilibrage()
        {
            List<Joueur> joueurs = ExcelData.JoueursBuilder(11);
>>>>>>> 8075630 (test équilibrage rouge pas fini)

            Session session = new Session();

            Partie partie = session.CreatePartie(joueurs);

<<<<<<< HEAD
            List<Equipe> equipes = session.GetEquipes();

            Assert.AreEqual(2, equipes.Count);

            Joueur j1 = joueurs.First();
            Joueur j2 = joueurs.Last();
            Assert.AreNotSame(j1, j2);


=======
            List<List<Joueur>> test = GetAllCombos(joueurs);
            Assert.IsTrue(true);
>>>>>>> 8075630 (test équilibrage rouge pas fini)
        }
    }
}
