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
        private static Dictionary<string, int> funcoes = new Dictionary<string, int>
        {
            { "preco", 0 },
            { "variacao", 8 },
            { "oscilacao", 8 },
            { "volume", 6 },
            { "abertura", 4 },
            { "maxima", 2 },
            { "maximo", 2 },
            { "minima", 1 },
            { "minimo", 1 },
        };

        private static StreamReader FazWebRequest(string url)
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
        
        public static string Cotacao(string codigoAcao, string funcao)
        {
            if (string.IsNullOrEmpty(codigoAcao))
                return "";

            if (string.IsNullOrEmpty(funcao))
                funcao = "preco";

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
                    var url = string.Format("http://ichart.finance.yahoo.com/table.csv?s={0}.SA&a={2}&b={1}&c={3}&d={2}&e={1}&f={3}&g=d", codigoAcao, data.Day, (data.Month - 1).ToString("00"), data.Year);

                    StreamReader reader = FazWebRequest(url);
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
                    return ar[ar.Length - 3].Trim();
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