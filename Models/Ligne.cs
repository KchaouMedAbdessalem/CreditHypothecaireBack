using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculMetis.Models
{
    public class Ligne
    {
        public int Indice;
        public double Solde_Debut;
        public double Mensualite;
        public double Interet;
        public double Capital_Rembourse;
        public double Solde_Fin;
        private int Duree;

        public Ligne(int Indice, double Solde_Debut, double Taux_Interet_Mensuel, int Duree, double Montant_Emprunter_Net)
        {
            this.Duree = Duree;
            this.Indice = Indice;
            this.Solde_Debut = Solde_Debut;
            this.Calculer_Mensualite(Taux_Interet_Mensuel, Duree, Montant_Emprunter_Net);
            this.Calculer_Interet(Taux_Interet_Mensuel);
            this.Calculer_Capital_Rembourse();
            this.Calculer_Solde_Fin();
        }

        public Ligne()
        {
        }

        private void Calculer_Mensualite(double Taux_Interet_Mensuel, int Duree, double Montant_Emprunter_Net)
        {
            double x = Math.Pow((1 + Taux_Interet_Mensuel), Duree);
            Mensualite = Math.Round((Montant_Emprunter_Net * Taux_Interet_Mensuel * x) / (x - 1), 2);
        }

        private void Calculer_Interet(double Taux_Interet_Mensuel)
        {
            Interet = Math.Round(Solde_Debut * Taux_Interet_Mensuel, 2);
        }

        private void Calculer_Capital_Rembourse()
        {
            if (Indice == Duree)
            {
                Capital_Rembourse = Solde_Debut;
            }
            else
            {
                Capital_Rembourse = Math.Round(Mensualite - Interet, 2);
            }
        }

        private void Calculer_Solde_Fin()
        {
            if (Indice == Duree)
            {
                Solde_Fin = 0;
            }
            else
            {
                Solde_Fin = Math.Round(Solde_Debut - Capital_Rembourse, 2);
            }
        }
    }
}