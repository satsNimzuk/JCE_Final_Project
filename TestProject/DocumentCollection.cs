using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace TestProject
{
    class DocumentCollection
    {
        public Dictionary<String, Document> collection;
        private Dictionary<String, double> idfDictionary;

        public DocumentCollection()
        {
            this.collection = new Dictionary<String, Document>();
            this.idfDictionary = new Dictionary<String, double>();
        }

        public DocumentCollection(Node tree)
        {
            this.collection = new Dictionary<String, Document>();
            this.idfDictionary = new Dictionary<String, double>();
            buildCollection(tree);            
        }

        public List<Document> calcRawTfQuery(String fileName)
        {
            String query = queryFromFile(fileName);
            List<Document> result = new List<Document>();
            List<String> allQueryTokens = Tokenizer.prepareTokensForTfIdf(query);
            List<String> uniqQueryTokens = getUniqElements(allQueryTokens);
            resetDocumentsScores();


            foreach (KeyValuePair<String, Document> document in collection)
            {
                Dictionary<String, TfIdfScore> terms = document.Value.getTerms();
                foreach (String queryToken in uniqQueryTokens)
                {
                    if (terms.ContainsKey(queryToken))
                    {
                        document.Value.score.finalScore += terms[queryToken].tfScore;
                    }

                }
                result.Add(document.Value);
            }

            result = result.OrderByDescending(doc => doc.score.finalScore).ToList();

            return result;
        }

        public List<Document> calcLogTfQuery(String fileName)
        {
            String query = queryFromFile(fileName);
            List<Document> result = new List<Document>();
            List<String> allQueryTokens = Tokenizer.prepareTokensForTfIdf(query);
            List<String> uniqQueryTokens = getUniqElements(allQueryTokens);
            resetDocumentsScores();


            foreach (KeyValuePair<String, Document> document in collection)
            {
                Dictionary<String, TfIdfScore> terms = document.Value.getTerms();
                foreach (String queryToken in uniqQueryTokens)
                {
                    if (terms.ContainsKey(queryToken))
                    {
                        if (terms[queryToken].tfScore > 0)
                        {
                            document.Value.score.finalScore += (1 + Math.Log10(terms[queryToken].tfScore));
                        }
                    }

                }
                result.Add(document.Value);
            }

            result = result.OrderByDescending(doc => doc.score.finalScore).ToList();

            return result;
        }

        public List<Document> calcIdfRank()
        {
            List<Document> result = new List<Document>();

            resetDocumentsScores();
            foreach (KeyValuePair<String, Document> document in collection)
            {
                Dictionary<String, TfIdfScore> terms = document.Value.getTerms();
                foreach (KeyValuePair<String, TfIdfScore> term in terms)
                {
                    document.Value.score.finalScore += idfDictionary[term.Key];
                }
                result.Add(document.Value);
            }

            result = result.OrderByDescending(doc => doc.score.finalScore).ToList();
            return result;
        }

        public List<Document> calcTfIdfQuery(String fileName)
        {
            String query = queryFromFile(fileName);
            List<Document> result = new List<Document>();
            List<String> allQueryTokens = Tokenizer.prepareTokensForTfIdf(query);
            List<String> uniqQueryTokens = getUniqElements(allQueryTokens);
            resetDocumentsScores();


            foreach (KeyValuePair<String, Document> document in collection)
            {
                Dictionary<String, TfIdfScore> terms = document.Value.getTerms();
                foreach (String queryToken in uniqQueryTokens)
                {
                    if (terms.ContainsKey(queryToken))
                    {
                        if (terms[queryToken].tfScore > 0)
                        {
                            document.Value.score.finalScore += ((1 + Math.Log10(terms[queryToken].tfScore)) * (idfDictionary[queryToken]));
                        }
                    }

                }
                result.Add(document.Value);
            }

            result = result.OrderByDescending(doc => doc.score.finalScore).ToList();

            return result;
        }

        public void rankToFile(List<Document> rank, String fileName)
        {
            FileStream outFileStream = File.Open(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(outFileStream);

            String result = "";

            foreach (Document doc in rank)
            {
                result += doc.score.finalScore + " | " + doc.name + Environment.NewLine;
            }

            fileWriter.Write(result);
            fileWriter.Flush();
            fileWriter.Close();
            outFileStream.Close();
        }

        private void resetDocumentsScores()
        {
            foreach (KeyValuePair<String, Document> document in collection)
            {
                document.Value.score.finalScore = 0;
            }
        }

        private String queryFromFile(String fileName)
        {
            string[] queryRawTextArr = File.ReadAllLines(fileName);
            String queryRawText = "";
            foreach (String line in queryRawTextArr)
            {
                queryRawText += line + Environment.NewLine;
            }
            return queryRawText;
        }

        private List<String> getUniqElements(List<String> list)
        {
            List<String> result = new List<String>();
            foreach (String str in list)
            {
                if (!result.Contains(str))
                {
                    result.Add(str);
                }
            }
            return result;
        }

        private void buildCollection(Node tree)
        {
            calcTfRec(tree);
            calcIDf();
        }

        private void calcIDf()
        {
            // DF calculation
            foreach (KeyValuePair<String, Document> document in collection)
            {
                Dictionary<String, TfIdfScore> terms = document.Value.getTerms();
                foreach (KeyValuePair<String, TfIdfScore> term in terms)
                {
                    if (!idfDictionary.ContainsKey(term.Key))
                    {
                        idfDictionary.Add(term.Key, 1);
                    }
                    else
                    {
                        idfDictionary[term.Key]++;
                    }
                }
            }

            inverseDf(); 

        }

        private void inverseDf()
        {
            List<String> keys = new List<String>(idfDictionary.Keys);
            foreach (String term in keys)
            {
                idfDictionary[term] = Math.Log10(collection.Count / idfDictionary[term]) ;
            }
        }

        private void calcTfRec(Node tree)
        {
            if (tree == null)
            {
                return;
            }

            calcTfSingleNode(tree);

            foreach (Node leaf in tree.links)
            {
                calcTfRec(leaf);
            }

        }


        private void calcTfSingleNode(Node node)
        {
            if (!collection.ContainsKey(node.name))
            {
                Document doc = new Document(node.name);
                collection.Add(node.name, doc);
            }
        }

    }
}
