using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalculMetis.Models;

namespace CalculMetis.Models
{
    public class ResultatCalcul
    {
        private double montantNet { get; set; }
        private double montantBrut { get; set; }
        private List<Ligne> tableau_Amortissement { get; set; }

        public ResultatCalcul()
        {
            this.montantNet = 0;
            this.montantBrut = 0;
            tableau_Amortissement = new List<Ligne>();
        }

        public double MontantNet
        {
            get { return montantNet; }
            set { montantNet = value; }
        }

        public double MontantBrut
        {
            get { return montantBrut; }
            set { montantBrut = value; }
        }

        public List<Ligne> Tableau_Amortissement
        {
            get { return tableau_Amortissement; }
            set { tableau_Amortissement = value; }
        }
    }
}