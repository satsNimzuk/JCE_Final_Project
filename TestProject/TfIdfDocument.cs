using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FinalProject
{
    class TfIdfDocument
    {
        private String _name;
        private Dictionary<String, TfIdfScore> terms;
        public TfIdfScore score;
        

        public TfIdfDocument()
        {
            this._name = "";
            this.terms = new Dictionary<String, TfIdfScore>();
            this.score = new TfIdfScore();
        }
        public TfIdfDocument(String articleName)
        {
            this._name = articleName;
            this.terms = new Dictionary<String, TfIdfScore>();
            this.score = new TfIdfScore();

            calcTf(articleName);
        }

        public TfIdfDocument(Path path)
        {
            this._name = path.getName();
            this.terms = new Dictionary<String, TfIdfScore>();
            this.score = new TfIdfScore();

            calcTf(path);
        }

        public TfIdfDocument(List<String> tokens)
        {
            this.terms = new Dictionary<String, TfIdfScore>();
            this.score = new TfIdfScore();

            calcTf(tokens);
        }

        public Dictionary<String, TfIdfScore> getTerms()
        {
            return terms;
        }

        private void calcTf(List<String> tokens)
        {
            calcRawTf(tokens);
            normalize(tokens);
        }
        
        private void calcTf(String articleName)
        {
            String rawText = DataLayerManager.getArticleByNameNew(articleName);
            rawText = getContent(rawText);

            List<String> tokens = Tokenizer.prepareTokensForTfIdf(rawText);
            
            calcRawTf(tokens);
            normalize(tokens);
        }

        private void calcTf(Path path)
        {
            String pathRawText = "";

            for (int i = 1; i < path.getPath().Count; i++)
            {
                String articleRawText = DataLayerManager.getArticleByNameNew(path.getPath()[i].name);
                pathRawText += getContent(articleRawText) + "\n";
            }

            List<String> tokens = Tokenizer.prepareTokensForTfIdf(pathRawText);

            calcRawTf(tokens);
            normalize(tokens);
        }

        private void calcRawTf(List<String> tokens)
        {
            foreach (String token in tokens)
            {
                if (!terms.ContainsKey(token))
                {
                    TfIdfScore score = new TfIdfScore();
                    score.tfScore = 1;
                    this.terms.Add(token, score);
                }
                else
                {
                    this.terms[token].tfScore++;
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
