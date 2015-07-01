using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace FinalProject
{
    class TfIdfCalculator
    {
        public Dictionary<String, TfIdfDocument> collection;
        private Dictionary<String, double> idfDictionary;

        public TfIdfCalculator()
        {
            this.collection = new Dictionary<String, TfIdfDocument>();
            this.idfDictionary = new Dictionary<String, double>();
        }

        public TfIdfCalculator(Node tree)
        {
            this.collection = new Dictionary<String, TfIdfDocument>();
            this.idfDictionary = new Dictionary<String, double>();
            buildCollection(tree);            
        }

        public TfIdfCalculator(List<Path> paths)
        {
            this.collection = new Dictionary<String, TfIdfDocument>();
            this.idfDictionary = new Dictionary<String, double>();
            buildCollection(paths);
        }

        public TfIdfCalculator(List<String> articleNames)
        {
            this.collection = new Dictionary<String, TfIdfDocument>();
            this.idfDictionary = new Dictionary<String, double>();
            buildCollection(articleNames);
        }

        //-------------------------------------------------------------------------------------------------------------------------------

        private void buildCollection(Node tree)
        {
            calcTfRec(tree);
            calcIDf();
        }

        private void buildCollection(List<Path> paths)
        {
            calcTf(paths);
            calcIDf();
        }

        private void buildCollection(List<String> articleNames)
        {
            calcTf(articleNames);
            calcIDf();
        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public List<TfIdfDocument> calcTfIdfQuery(String fileName)
        {
            resetDocumentsScores();

            String query = queryFromFile(fileName);
            List<TfIdfDocument> result = new List<TfIdfDocument>();
            List<String> allQueryTokens = Tokenizer.prepareTokensForTfIdf(query);
            List<String> uniqQueryTokens = getUniqElements(allQueryTokens);            

            calcDocumentsTfIdfVector(uniqQueryTokens);
            TfIdfDocument queryDoc = calcQueryTfIdfVector(allQueryTokens);

            calcCousineScore(queryDoc);

            result = this.collection.Values.ToList();
            //result = result.OrderByDescending(doc => doc.score.cosineScore).ToList();

            return result;
        }

        private void calcCousineScore(TfIdfDocument queryDoc)
        {
            //Cosine Similarity(Query,Document1) = Dot product(Query, Document1) / ||Query|| * ||Document1||

            foreach (KeyValuePair<String, TfIdfDocument> doc in this.collection)
            {
                double cousineScore = dotProduct(queryDoc, doc.Value) / (abs(queryDoc) * abs(doc.Value));
                doc.Value.score.cosineScore = cousineScore;
            }
        }

        private double abs(TfIdfDocument doc)
        {
            double result = 0;
            foreach (KeyValuePair<String, double> tfidf in doc.score.tfIdfVector)
            {
                result += Math.Pow(tfidf.Value, 2);
            }
            result = Math.Sqrt(result);
            return result;
        }

        private double dotProduct(TfIdfDocument query, TfIdfDocument document)
        {
            double result = 0;
            foreach (KeyValuePair<String, double> tfidf in query.score.tfIdfVector)
            {
                result += tfidf.Value * document.score.tfIdfVector[tfidf.Key];
            }
            return result;
        }

        private TfIdfDocument calcQueryTfIdfVector(List<String> allQueryTokens)
        {
            TfIdfDocument queryDoc = new TfIdfDocument(allQueryTokens);
            Dictionary<String, TfIdfScore> queryTerms = queryDoc.getTerms();

            foreach (KeyValuePair<String, TfIdfScore> queryTerm in queryTerms)
            {
                
                if (this.idfDictionary.ContainsKey(queryTerm.Key))
                {
                    queryDoc.score.tfIdfVector.Add(queryTerm.Key, queryTerm.Value.tfScore * this.idfDictionary[queryTerm.Key]);
                }
                else
                {
                    double idf = 1 + Math.Log(this.collection.Count + 1);
                    queryDoc.score.tfIdfVector.Add(queryTerm.Key, queryTerm.Value.tfScore * idf);
                }
            }
            return queryDoc;        
        }

        private void calcDocumentsTfIdfVector(List<String> uniqQueryTokens)
        {
            foreach (KeyValuePair<String, TfIdfDocument> document in collection)
            {
                Dictionary<String, TfIdfScore> terms = document.Value.getTerms();
                foreach (String queryToken in uniqQueryTokens)
                {
                    if (terms.ContainsKey(queryToken))
                    {
                        double tfIdf = terms[queryToken].tfScore * idfDictionary[queryToken];
                        document.Value.score.tfIdfVector.Add(queryToken, tfIdf);
                    }
                    else
                    {
                        document.Value.score.tfIdfVector.Add(queryToken, 0);
                    }
                }
            }
        }

        private void resetDocumentsScores()
        {
            foreach (KeyValuePair<String, TfIdfDocument> document in collection)
            {
                document.Value.score.cosineScore = 0;
                document.Value.score.tfIdfVector.Clear();
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





        //-------------------------------------------------------------------------------------------------------------------------------

        private void calcIDf()
        {
            // DF calculation
            foreach (KeyValuePair<String, TfIdfDocument> document in this.collection)
            {
                Dictionary<String, TfIdfScore> terms = document.Value.getTerms();
                foreach (KeyValuePair<String, TfIdfScore> term in terms)
                {
                    if (!this.idfDictionary.ContainsKey(term.Key))
                    {
                        this.idfDictionary.Add(term.Key, 1);
                    }
                    else
                    {
                        this.idfDictionary[term.Key]++;
                    }
                }
            }
            // IDF calculation
            inverseDf();
        }

        private void inverseDf()
        {
            List<String> keys = new List<String>(this.idfDictionary.Keys);
            foreach (String term in keys)
            {
                //IDF(term) = 1 + log(Total Number Of Documents / Number Of Documents with term game in it)
                this.idfDictionary[term] = 1+ Math.Log(this.collection.Count / this.idfDictionary[term]) ;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------


        private void calcTf(List<Path> paths)
        {
            foreach (Path path in paths)
            {
                calcTfSinglePath(path);
            }
        }

        private void calcTf(List<String> articleNames)
        {
            foreach (String articleName in articleNames)
            {
                calcTfSingleArticle(articleName);
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
                TfIdfDocument doc = new TfIdfDocument(node.name);
                collection.Add(node.name, doc);
            }
        }

        private void calcTfSinglePath(Path path)
        {
            if (!collection.ContainsKey(path.getName()))
            {
                TfIdfDocument doc = new TfIdfDocument(path);
                collection.Add(path.getName(), doc);
            }
        }

        private void calcTfSingleArticle(String articleName)
        {
            if (!collection.ContainsKey(articleName))
            {
                TfIdfDocument doc = new TfIdfDocument(articleName);
                collection.Add(articleName, doc);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public void rankToFile(List<TfIdfDocument> rank, String fileName)
        {
            FileStream outFileStream = File.Open(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(outFileStream);

            String result = "";
            rank = rank.OrderByDescending(doc => doc.score.cosineScore).ToList();

            foreach (TfIdfDocument doc in rank)
            {
                result += doc.score.cosineScore + " | " + doc.name + Environment.NewLine;
            }

            fileWriter.Write(result);
            fileWriter.Flush();
            fileWriter.Close();
            outFileStream.Close();
        }

        public void rankToFile(List<TfIdfDocument> rank, Vector pageRank, String fileName)
        {
            FileStream outFileStream = File.Open(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(outFileStream);

            String result = "";
            rank = rank.OrderByDescending(doc => doc.score.cosineScore).ToList();

            foreach (TfIdfDocument doc in rank)
            {
                result += doc.score.cosineScore + " | " + pageRank.calcDecRank(doc.name) + " | " + doc.name + Environment.NewLine;
            }

            fileWriter.Write(result);
            fileWriter.Flush();
            fileWriter.Close();
            outFileStream.Close();
        }

        public void rankToFile(List<TfIdfDocument> rank, int resultLength, String fileName)
        {
            FileStream outFileStream = File.Open(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(outFileStream);

            String result = "";

            List<TfIdfDocument> sortedResult = rank.ToList();
            sortedResult = sortedResult.OrderByDescending(doc => doc.score.cosineScore).ToList();

            int idx = 1;
            double idfScoreSum = 0;
            foreach (TfIdfDocument doc in rank)
            {
                result += (sortedResult.IndexOf(doc) + 1) + "\t" + doc.score.cosineScore + " | " + doc.name + Environment.NewLine;
                idfScoreSum += doc.score.cosineScore;

                if (idx % resultLength == 0 && idx > 1)
                {
                    idfScoreSum = idfScoreSum / resultLength;
                    result += Environment.NewLine + "Group average: " + idfScoreSum + Environment.NewLine + Environment.NewLine;
                    idfScoreSum = 0;
                }

                idx++;
            }

            fileWriter.Write(result);
            fileWriter.Flush();
            fileWriter.Close();
            outFileStream.Close();
        }
    }
}
