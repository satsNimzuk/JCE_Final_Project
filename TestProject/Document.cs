using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TestProject
{
    class Document
    {
        private String _name;
        private Dictionary<String, TfIdfScore> terms;
        public TfIdfScore score;
        

        public Document()
        {
            this._name = "";
            this.terms = new Dictionary<String, TfIdfScore>();
            this.score = new TfIdfScore();
        }
        public Document(String articleName)
        {
            this._name = articleName;
            this.terms = new Dictionary<String, TfIdfScore>();
            this.score = new TfIdfScore();

            calcTf(articleName);
        }

        public Dictionary<String, TfIdfScore> getTerms()
        {
            return terms;
        }

        private void calcTf(String articleName)
        {
            String rawText = DataLayerManager.getArticleByNameNew(articleName);
            rawText = getContent(rawText);

            List<String> tokens = Tokenizer.prepareTokensForTfIdf(rawText);
            
            calcRawTf(tokens);
            //normalize(tokens);
        }

        private void calcRawTf(List<String> tokens)
        {
            foreach (String token in tokens)
            {
                if (!terms.ContainsKey(token))
                {
                    TfIdfScore score = new TfIdfScore();
                    score.tfScore = 1;
                    terms.Add(token, score);
                }
                else
                {
                    terms[token].tfScore++;
                }

            }
        }

        private void normalize(List<String> tokens)
        {
            int numOfTokens = tokens.Count;
            foreach (KeyValuePair<String, TfIdfScore> entry in terms)
            {
                entry.Value.tfScore = entry.Value.tfScore / numOfTokens;
            }
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

        public String name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
