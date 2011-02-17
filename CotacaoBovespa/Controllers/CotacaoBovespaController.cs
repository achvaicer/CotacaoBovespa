using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CotacaoBovespa.Models;

namespace CotacaoBovespa.Controllers
{
    public class CotacaoBovespaController : Controller
    {
        [HttpPost]
        public string Index(string codigoAcao, string funcao)
        {
            var acao = new Acao();
            return acao.Cotacao(codigoAcao, funcao);
        }

    }
}
