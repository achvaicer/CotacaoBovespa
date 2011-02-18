using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CotacaoBovespa.Models;

namespace CotacaoBovespa
{
    public partial class Cotacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var acao = new Acao();
            Response.Clear();
            var codigoAcao = Request.QueryString["codigoAcao"];
            var funcao = Request.QueryString["funcao"];
            if (!string.IsNullOrEmpty(codigoAcao))
                Response.Write(acao.Cotacao(codigoAcao, string.IsNullOrEmpty(funcao) ? "preco" : funcao));
        }
    }
}