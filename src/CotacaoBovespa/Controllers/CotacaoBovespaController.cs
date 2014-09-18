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
        //
        // GET: /CotacaoBovespa/

        public string Index(string codigoAcao, string funcao)
        {
            return Acao.Cotacao(codigoAcao, funcao);
        }

    }
}
