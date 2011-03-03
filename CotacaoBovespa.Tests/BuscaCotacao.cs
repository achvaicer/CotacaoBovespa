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
            var preco = decimal.Parse(Cotacao("PETR4", null));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_passing_funcao_as_empty()
        {
            var preco = decimal.Parse(Cotacao("PETR4", ""));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_preco()
        {
            var preco = decimal.Parse(Cotacao("PETR4", "preco"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void smallcap_preco()
        {
            var preco = decimal.Parse(Cotacao("NATU3", "preco"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_maxima()
        {
            var preco = decimal.Parse(Cotacao("PETR4", "maxima"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void smallcap_maxima()
        {
            var preco = decimal.Parse(Cotacao("NATU3", "maxima"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_minima()
        {
            var preco = decimal.Parse(Cotacao("PETR4", "minima"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void smallcap_minima()
        {
            var preco = decimal.Parse(Cotacao("NATU3", "minima"));
            Assert.IsInstanceOf<decimal>(preco);
        }


        [Test]
        public void bluechip_abertura()
        {
            var preco = decimal.Parse(Cotacao("PETR4", "abertura"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void smallcap_abertura()
        {
            var preco = decimal.Parse(Cotacao("NATU3", "abertura"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_volume()
        {
            var volume = Cotacao("PETR4", "volume");
            Assert.IsTrue(Regex.IsMatch(volume, @"^(\d{1,3}.(\d{3}.)*\d{3}(\,\d{1,3})?|\d{1,3}(\,\d{3})?)$"));
        }

        [Test]
        public void smallcap_volume()
        {
            var volume = Cotacao("NATU3", "volume");
            Assert.IsTrue(Regex.IsMatch(volume, @"^(\d{1,3}.(\d{3}.)*\d{3}(\,\d{1,3})?|\d{1,3}(\,\d{3})?)$"));
        }

        [Test]
        public void bluechip_variacao()
        {
            var variacao = Cotacao("PETR4", "variacao");
            Assert.IsTrue(Regex.IsMatch(variacao, @"^((\d{1,2})?([,][\d]{1,2})?){1}[%]{1}$"));
        }

        [Test]
        public void smallcap_variacao()
        {
            var variacao = Cotacao("NATU3", "variacao");
            Assert.IsTrue(Regex.IsMatch(variacao, @"^((\d{1,2})?([,][\d]{1,2})?){1}[%]{1}$"));
        }

        [Test]
        public void bluechip_hora()
        {
            var hora = Cotacao("PETR4", "hora");
            Assert.IsTrue(Regex.IsMatch(hora, @"^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])?$"));
        }

        [Test]
        public void smallcap_hora()
        {
            var hora = Cotacao("NATU3", "hora");
            Assert.IsTrue(Regex.IsMatch(hora, @"^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])?$"));
        }
    }
}
