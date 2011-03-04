using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CotacaoBovespa.Models;
using GaDotNet.Common;
using GaDotNet.Common.Data;
using GaDotNet.Common.Helpers;

namespace CotacaoBovespa
{
    public partial class Cotacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime inicio = DateTime.Now;
            var acao = new Acao();
            Response.Clear();
            var codigoAcao = Request.QueryString["codigoAcao"];
            var funcao = Request.QueryString["funcao"] ?? "preco";
            if (!string.IsNullOrEmpty(codigoAcao))
                Response.Write(acao.Cotacao(codigoAcao, string.IsNullOrEmpty(funcao) ? "preco" : funcao));

            GoogleAnalytics(codigoAcao, funcao, inicio);
        }

        private static void GoogleAnalytics(string codigoAcao, string funcao, DateTime inicio)
        {
            GooglePageView pageView = new GooglePageView("Cotação", "www.boabolsa.com.br", "Cotacao.aspx");

            GoogleEvent googleEvent = new GoogleEvent("www.boabolsa.com.br", "Cotação", funcao, codigoAcao, (int)inicio.Subtract(DateTime.Now).TotalMilliseconds);

            TrackingRequest request = new RequestFactory().BuildRequest(pageView, HttpContext.Current);

            GoogleTracking.FireTrackingEvent(request);
        }
    }
}