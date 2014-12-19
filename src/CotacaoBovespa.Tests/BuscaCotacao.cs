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

        [Test]
        public void passing_codigoAcao_as_null()
        {
            var preco = Acao.Cotacao(null, null);
            Assert.IsEmpty(preco);
        }

        [Test]
        public void passing_codigoAcao_as_empty()
        {
            var preco = Acao.Cotacao("", null);
            Assert.IsEmpty(preco);
        }

        [Test]
        public void bluechip_passing_funcao_as_null()
        {
            var preco = decimal.Parse(Acao.Cotacao(bluechip, null));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_passing_funcao_as_empty()
        {
            var preco = decimal.Parse(Acao.Cotacao(bluechip, ""));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_preco()
        {
            var preco = decimal.Parse(Acao.Cotacao(bluechip, "preco"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void smallcap_preco()
        {
            var preco = decimal.Parse(Acao.Cotacao(smallcap, "preco"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_maxima()
        {
            var preco = decimal.Parse(Acao.Cotacao(bluechip, "maxima"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        

        [Test]
        public void smallcap_maxima()
        {
            var preco = decimal.Parse(Acao.Cotacao(smallcap, "maxima"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_historica()
        {
            var preco = decimal.Parse(Acao.Cotacao(bluechip, "2011-10-20"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void smallcap_historica()
        {
            var preco = decimal.Parse(Acao.Cotacao(smallcap, "2011-10-20"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_minima()
        {
            var preco = decimal.Parse(Acao.Cotacao(bluechip, "minima"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void smallcap_minima()
        {
            var preco = decimal.Parse(Acao.Cotacao(smallcap, "minima"));
            Assert.IsInstanceOf<decimal>(preco);
        }


        [Test]
        public void bluechip_abertura()
        {
            var preco = decimal.Parse(Acao.Cotacao(bluechip, "abertura"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void smallcap_abertura()
        {
            var preco = decimal.Parse(Acao.Cotacao(smallcap, "abertura"));
            Assert.IsInstanceOf<decimal>(preco);
        }

        [Test]
        public void bluechip_volume()
        {
            var volume = Acao.Cotacao(bluechip, "volume");
            Assert.IsTrue(Regex.IsMatch(volume, @"^(\d{1,3}.(\d{3}.)*\d{3}(\,\d{1,3})?|\d{1,3}(\,\d{3})?)$"));
        }

        [Test]
        public void smallcap_volume()
        {
            var volume = Acao.Cotacao(smallcap, "volume");
            Assert.IsTrue(Regex.IsMatch(volume, @"^((\d{1,3})*.*(\d{3}.)*\d{3}(\,\d{1,3})?|\d{1,3}(\,\d{3})?)$"));
        }

        [Test]
        public void bluechip_variacao()
        {
            var variacao = Acao.Cotacao(bluechip, "variacao");
            Assert.IsTrue(Regex.IsMatch(variacao, @"^(-?(\d{1,2})?([,][\d]{1,2})?){1}[%]{1}$"));
        }

        [Test]
        public void smallcap_variacao()
        {
            var variacao = Acao.Cotacao(smallcap, "variacao");
            Assert.IsTrue(Regex.IsMatch(variacao, @"^(-?(\d{1,2})?([,][\d]{1,2})?){1}[%]{1}$"));
        }

        [Test]
        public void bluechip_hora()
        {
            var hora = Acao.Cotacao(bluechip, "hora");
            Assert.IsTrue(Regex.IsMatch(hora, @"^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])?$"));
        }

        [Test]
        public void smallcap_hora()
        {
            var hora = Acao.Cotacao(smallcap, "hora");
            Assert.IsTrue(Regex.IsMatch(hora, @"^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])?$"));
        }
    }
}
