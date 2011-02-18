using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace CotacaoBovespa.Models
{
    public class Acao
    {
        private Dictionary<string, int> funcoes = new Dictionary<string, int>();

        public Acao()
        {
            funcoes.Add("preco", 0);
            funcoes.Add("variacao", 8);
            funcoes.Add("oscilacao", 8);
            funcoes.Add("volume", 6);
            funcoes.Add("abertura", 4);
            funcoes.Add("maxima", 2);
            funcoes.Add("maximo", 2);
            funcoes.Add("minima", 1);
            funcoes.Add("minimo", 1);
        }

        private StreamReader FazWebRequest(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII);
                return reader;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string Cotacao(string codigoAcao, string funcao)
        {
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlDocument();

            try
            {
                HtmlNodeCollection nodes;
                
                if (funcao == "negocios") 
                {
                    htmlDoc.Load(FazWebRequest("http://www.bmfbovespa.com.br/Cotacao-Rapida/ExecutaAcaoCotRapXSL.asp?gstrCA=&txtCodigo=" + codigoAcao + "&intIdiomaXsl=0"));
                    nodes = htmlDoc.DocumentNode.SelectNodes("//td[@class='tdValor']");
                    return nodes[2].InnerText.Trim();
                }

                DateTime data = new DateTime();
                if (DateTime.TryParse(funcao, out data) && data != DateTime.Today)
                {
                    StreamReader reader = FazWebRequest("http://au.rd.yahoo.com/finance/quotes/internal/historical/download/*http://ichart.finance.yahoo.com/table.csv?s=" + codigoAcao + ".SA&d=" + (data.Month - 1).ToString() + "&e=" + data.Day.ToString() + "&f=" + data.Year.ToString() + "&g=d&a=" + (data.Month - 1).ToString() + "&b=" + data.Day.ToString() + "&c=" + data.Year.ToString() + "&ignore=.csv");
                    reader.ReadLine();

                    var ar = reader.ReadLine().Split(',');
                    return ar[4].Replace(".", ",").Trim();
                }

                htmlDoc.Load(FazWebRequest("https://secure.apligraf.com.br/webfeed/viptrade/evolucao001.php?codpad=" + codigoAcao));
                if (funcao == "hora")
                    return (htmlDoc.DocumentNode.SelectSingleNode("//div")).ChildNodes[4].InnerText.Trim();
                if (funcao == "strike")
                {
                    var ar = (htmlDoc.DocumentNode.SelectSingleNode("//div")).ChildNodes[0].InnerText.Trim().Split(' ');
                    return ar[ar.Length - 1].Trim();
                }

                return htmlDoc.DocumentNode.SelectNodes("//td[@class='num'] | //td[@class='num pos'] | //td[@class='num neg']")[funcoes[funcao]].InnerText.Trim();
            }
            catch (Exception)
            {
                return "";
            }
            
        }
    }
}