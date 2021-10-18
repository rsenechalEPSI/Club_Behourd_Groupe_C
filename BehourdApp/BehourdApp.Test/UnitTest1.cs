using BehourdApp.ConsoleApp.Classes;
using BehourdApp.Test.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BehourdApp.Test
{
    [TestClass]
    public class UnitTest1
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
            ExcelData excel = new ExcelData();
            List<Joueur> joueurs = excel.Joueurs;

            Session session = new Session();

            session.CreatePartie(joueurs);

            Assert.IsNotNull(session);
            Assert.IsNotNull(session.GetParties());
            Assert.IsNotNull(session.GetEquipes());
            

            
        }
    }
}
