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

        [TestMethod]
        public void Creation_10_vs_10()
        {

            List<Joueur> joueurs = ExcelData.JoueursBuilder(20);

            Session session = new Session();

            Partie partie = session.CreatePartie(joueurs);

            List<Equipe> equipes = session.GetEquipes();

            Assert.AreEqual(2, equipes.Count);

            Joueur j1 = joueurs.First();
            Joueur j2 = joueurs.Last();
            Assert.AreNotSame(j1, j2);


        }
    }
}
