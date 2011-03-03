using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using CotacaoBovespa.Models;
using System.Text.RegularExpressions;

namespace CotacaoBovespa.Tests
{
    [TestFixture]
    public class BuscaCotacao
    {
        const string bluechip = "PETR4";
        const string smallcap = "TELB4";

        private static string Cotacao(string codigoAcao, string funcao)
        {
            var acao = new Acao();
            var retorno = acao.Cotacao(codigoAcao, funcao);
            return retorno;
        }

        [Test]
        public void passing_codigoAcao_as_null()
        {
            var preco = Cotacao(null, null);
            Assert.IsEmpty(preco);
        }

        [Test]
        public void passing_codigoAcao_as_empty()
        {
            var preco = Cotacao("", null);
            Assert.IsEmpty(preco);
        }

        [Test]
        public void bluechip_passing_funcao_as_null()
        {
            var preco = decimal.Parse(Cotacao(bluechip, null));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_passing_funcao_as_empty()
        {
            var preco = decimal.Parse(Cotacao(bluechip, ""));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_preco()
        {
            var preco = decimal.Parse(Cotacao(bluechip, "preco"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void smallcap_preco()
        {
            var preco = decimal.Parse(Cotacao(smallcap, "preco"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_maxima()
        {
            var preco = decimal.Parse(Cotacao(bluechip, "maxima"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void smallcap_maxima()
        {
            var preco = decimal.Parse(Cotacao(smallcap, "maxima"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_minima()
        {
            var preco = decimal.Parse(Cotacao(bluechip, "minima"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void smallcap_minima()
        {
            var preco = decimal.Parse(Cotacao(smallcap, "minima"));
            Assert.IsInstanceOf<decimal>(preco);
        }


        [Test]
        public void bluechip_abertura()
        {
            var preco = decimal.Parse(Cotacao(bluechip, "abertura"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void smallcap_abertura()
        {
            var preco = decimal.Parse(Cotacao(smallcap, "abertura"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_volume()
        {
            var volume = Cotacao(bluechip, "volume");
            Assert.IsTrue(Regex.IsMatch(volume, @"^(\d{1,3}.(\d{3}.)*\d{3}(\,\d{1,3})?|\d{1,3}(\,\d{3})?)$"));
        }

        [Test]
        public void smallcap_volume()
        {
            var volume = Cotacao(smallcap, "volume");
            Assert.IsTrue(Regex.IsMatch(volume, @"^(\d{1,3}.(\d{3}.)*\d{3}(\,\d{1,3})?|\d{1,3}(\,\d{3})?)$"));
        }

        [Test]
        public void bluechip_variacao()
        {
            var variacao = Cotacao(bluechip, "variacao");
            Assert.IsTrue(Regex.IsMatch(variacao, @"^((\d{1,2})?([,][\d]{1,2})?){1}[%]{1}$"));
        }

        [Test]
        public void smallcap_variacao()
        {
            var variacao = Cotacao(smallcap, "variacao");
            Assert.IsTrue(Regex.IsMatch(variacao, @"^((\d{1,2})?([,][\d]{1,2})?){1}[%]{1}$"));
        }

        [Test]
        public void bluechip_hora()
        {
            var hora = Cotacao(bluechip, "hora");
            Assert.IsTrue(Regex.IsMatch(hora, @"^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])?$"));
        }

        [Test]
        public void smallcap_hora()
        {
            var hora = Cotacao(smallcap, "hora");
            Assert.IsTrue(Regex.IsMatch(hora, @"^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])?$"));
        }
    }
}
