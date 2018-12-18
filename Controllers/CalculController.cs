using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CalculMetis.Models;
using System.Web.Http.Cors;

namespace CalculMetis.Controllers
{
    [EnableCors(origins: "*", headers: "Accept, Origin, Content-Type", methods: "*")]
    public class CalculController : ApiController
    {

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Welcome()
        {
            try
            {
                string message = "Welcome to Metis API *** " +
                    "Montant Brut: api/Calculer_Montant_Emprunter_Brut/{MontantAchat}/{Fondspropre} *** "+
                    "Montant Net: api/Calculer_Montant_Emprunter_Net/{MontantEmprunterBrut} *** " +
                    "Taux d'interet mensuel: api/Calculer_Taux_Interet_Mensuel/{TauxInteretAnnuel} *** " +
                    "Tableu d'amortissement: api/Remplir_Tableau_Amortissement/{MontantEmprunterNet}/{TauxInteretAnnuel}/{Duree} *** " +
                    "Resultat: api/Calcul_Resultat/{MontantAchat}/{FondsPropre}/{Duree}/{TauxInteretAnnuel}";
                return Request.CreateResponse<string>(HttpStatusCode.OK, message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [Route("api/Calculer_Montant_Emprunter_Brut/{MontantAchat}/{Fondspropre}")]
        [HttpGet]
        public HttpResponseMessage CalculerMontantEmprunterBrut(double MontantAchat, double Fondspropre)
        {
            try
            {
                double resultat = Data.Calculer_Montant_Emprunter_Brut(MontantAchat, Fondspropre);
                return Request.CreateResponse<double>(HttpStatusCode.OK, resultat);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }


        [Route("api/Calculer_Montant_Emprunter_Net/{MontantEmprunterBrut}")]
        [HttpGet]
        public HttpResponseMessage CalculerMontantEmprunterNet(double MontantEmprunterBrut)
        {
            try
            {
                double resultat = Data.Calculer_Montant_Emprunter_Net(MontantEmprunterBrut);
                return Request.CreateResponse<double>(HttpStatusCode.OK, resultat);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }


        [Route("api/Calculer_Taux_Interet_Mensuel/{TauxInteretAnnuel}")]
        [HttpGet]
        public HttpResponseMessage CalculerTauxInteretMensuel(double TauxInteretAnnuel)
        {
            try
            {
                double resultat = Data.Calculer_Taux_Interet_Mensuel(TauxInteretAnnuel);
                return Request.CreateResponse<double>(HttpStatusCode.OK, resultat);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [Route("api/Remplir_Tableau_Amortissement/{MontantEmprunterNet}/{TauxInteretAnnuel}/{Duree}")]
        [HttpGet]
        public HttpResponseMessage RemplirTableauAmortissement(double MontantEmprunterNet, double TauxInteretAnnuel, int Duree)
        {
            try
            {
                double TauxInteretMensuel = Data.Calculer_Taux_Interet_Mensuel(TauxInteretAnnuel);
                List<Ligne> resultat = Data.Remplir_Tableau_Amortissement(MontantEmprunterNet, Duree, TauxInteretMensuel);
                return Request.CreateResponse<List<Ligne>>(HttpStatusCode.OK, resultat);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [Route("api/Calcul_Resultat/{MontantAchat}/{FondsPropre}/{Duree}/{TauxInteretAnnuel}")]
        [HttpGet]
        public HttpResponseMessage CalculResultat (double MontantAchat, double FondsPropre, int Duree, double TauxInteretAnnuel)
        {
            try
            {
                ResultatCalcul resultat = new ResultatCalcul();
                resultat.MontantBrut = Data.Calculer_Montant_Emprunter_Brut(MontantAchat, FondsPropre);
                resultat.MontantNet = Data.Calculer_Montant_Emprunter_Net(resultat.MontantBrut);
                resultat.Tableau_Amortissement = Data.Remplir_Tableau_Amortissement(resultat.MontantNet, Duree, Data.Calculer_Taux_Interet_Mensuel(TauxInteretAnnuel));
                return Request.CreateResponse<ResultatCalcul>(HttpStatusCode.OK, resultat);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

    }
}
