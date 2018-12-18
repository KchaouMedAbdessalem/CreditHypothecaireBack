using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculMetis.Models
{
    public static class Data
    {

        public static double Calculer_Montant_Emprunter_Brut(double Montant_Achat, double Fonds_propre)
        {
            return  Montant_Achat + Calculer_Frais_Achat_Standard(Montant_Achat) - Fonds_propre;
        }

        public static double Calculer_Frais_Achat_Standard(double Montant_Achat)
        {
            if (Montant_Achat <= 50000)
            {
                return 0;
            }
            else
            {
                return Montant_Achat * 0.1;
            }
        }

        public static double Calculer_Montant_Emprunter_Net(double Montant_Emprunter_Brut)
        {
            return Montant_Emprunter_Brut * 1.02;
        }

        public static double Calculer_Taux_Interet_Mensuel(double Taux_Interet_Annuel)
        {
            return  Math.Round((Math.Pow((1 + Taux_Interet_Annuel), ((double)1) / 12) - 1)*100, 3)/100;
        }

        public static List<Ligne> Remplir_Tableau_Amortissement(double Montant_Emprunter_Net, int Duree, double Taux_Interet_Mensuel)
        {
            List<Ligne> Tableau_Amortissement = new List<Ligne>();
            double Solde_Debut = Montant_Emprunter_Net;
            int i;
            for (i = 0; i < Duree; i++)
            {
                Ligne ligne = new Ligne(i + 1, Solde_Debut, Taux_Interet_Mensuel, Duree, Montant_Emprunter_Net);
                Tableau_Amortissement.Add(ligne);
                Solde_Debut = ligne.Solde_Fin;
            }
            return Tableau_Amortissement;
        }
    }
}