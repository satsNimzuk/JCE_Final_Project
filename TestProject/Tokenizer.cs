using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FinalProject
{
    static class Tokenizer
    {
        
        private static Regex pattern = new Regex(@"\W|_");


        public static List<String> prepareTokensForTfIdf(String rawText)
        {
            List<String> tokens = tokenize(rawText);
            tokens = removeBadTokens(tokens);
            tokens = stemm(tokens);
            return tokens;
        }

        private static List<String> tokenize(String article)
        {
            String[] result = article.Split(Const.DELIMETERS, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = result[i].ToLower();
            }
            return result.ToList();
        }

        private static List<String> removeBadTokens(List<String> tokens)
        {
            List<String> result = new List<String>();
            foreach (String token in tokens)
            {
                if (isNonAlphaNumeric(token) || isNumber(token) || isStopWord(token, Const.STOP_WORDS) || isStopWord(token, Const.USER_STOP_WORDS))
                {
                    continue;
                }
                else
                {
                    result.Add(token);
                }
            }
            return result;
        }

        private static bool isNumber(String token)
        {
            int n;
            return int.TryParse(token, out n);
        }

        private static bool isNonAlphaNumeric(String token)
        {
            Match match = pattern.Match(token);
            return match.Success;
        }

        private static bool isStopWord(String token, Dictionary<String,bool> stopwords)
        {
            return stopwords.ContainsKey(token);
        }

        private static List<String> stemm(List<String> tokens)
        {
            List<String> result = new List<String>();
            foreach (String token in tokens)
            {
                String stemmedToken = stemmerWrap(token);
                result.Add(stemmedToken);
            }
            return result;
        }

        private static String stemmerWrap(String token)
        {
            foreach (char c in token)
            {
                Stemmer.add(c);
            }
            Stemmer.stem();
            return Stemmer.Print();
        }
    }
}
