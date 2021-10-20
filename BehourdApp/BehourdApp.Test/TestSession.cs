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

            Assert.IsNotNull(session);
            Assert.IsNotNull(partie);
            Assert.IsNotNull(session.GetEquipes());
            
        }

        [TestMethod]
        public void Creation_5_vs_5()
        {
            List<Joueur> joueurs5vs5 = ExcelData.JoueursBuilder(5);

            Session session = new Session();

            Partie partie = session.CreatePartie(joueurs5vs5);

            List<Equipe> equipes = session.GetEquipes();
            Assert.AreEqual(2, equipes.Count);

            Equipe equipe1 = equipes.First();
            Equipe equipe2 = equipes.Last();

            foreach(Joueur j1 in equipe1.Joueurs)
            {
                foreach(Joueur j2 in equipe2.Joueurs)
                {
                    Assert.AreNotSame(j1, j2);
                }
            }

        }
    }
}
