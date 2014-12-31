using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class TextManager
    {
        DataLayerManager dlm;

        public TextManager()
        {
            dlm = new DataLayerManager();
        }

        public List<String> getLinksFromArticle(String articleName)
        {
            String article = dlm.getArticleByName(articleName);
            //need to check the right article was retrieved
            String actualArticleName = getArticleName(article);
            if (!actualArticleName.Equals(articleName))
            {
                return new List<String>();
            }

            List<String> result = getLinksFromString(article);

            result = filterLinks(result);
            result = parseLinks(result);

            return result;
        }

        private List<String> filterLinks(List<String> links)
        {
            links.RemoveAll(link => link.Split(':')[0].Equals("File"));
            links.RemoveAll(link => link.Split(':')[0].Equals("Category"));
            links.RemoveAll(link => link.Split(':')[0].Equals("Wikipedia"));
            links.RemoveAll(link => link.Split(':')[0].Equals("Image"));
            links.RemoveAll(link => link.Split(':')[0].Equals("Template"));
            return links;
        }
        private List<String> parseLinks(List<String> links)
        {
            for (int i = 0; i < links.Count; i++)
            {
                if (links[i].Length > 1)
                {
                    try
                    {
                        links[i] = links[i].Split('|')[0].Trim();
                        links[i] = char.ToUpper(links[i][0]) + links[i].Substring(1);
                    }
                    catch{}
                }
            }
            return links;
        }
        
        private List<String> getLinksFromString(String article)
        {
            List<String> result = new List<String>();

            String text = getContent(article);

            for (int i = 0; i < text.Length-5; i++)  //looking for [[...]] pattern
            {
                if (text[i].Equals('[') && text[i+1].Equals('['))
                {
                    for (int j = i; j < text.Length - 1; j++)
                    {
                        if (text[j].Equals(']') && text[j + 1].Equals(']'))
                        {
                            String link = text.Substring(i+2,j-i-2);
                            result.Add(link);
                            i = j;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        private String getContent(String article)
        {
            String result = "";
            String start = @"<text xml";
            String end = @"</text>";

            int startIndex = article.IndexOf(start);
            int endIndex = article.IndexOf(end) + end.Length;

            result = article.Substring(startIndex, endIndex - startIndex);            
            return result;
        }

        private String getArticleName (String article)
        {
            String result = "";
            String firstLine = "";
            int i;

            for (i = 0; i < article.Length; i++)
            {
                if (article[i].Equals('\r') || article[i].Equals('\n'))
                {
                    break;
                }
            }

            firstLine = article.Substring(0, i).Trim();

            if (firstLine.Substring(0, 7).Equals(@"<title>") && firstLine.Substring(firstLine.Length - 8, 8).Equals(@"</title>"))
            {
                result = firstLine.Substring(7, firstLine.Length - 15).Trim();
            }
            return result;
        }

    }
}
