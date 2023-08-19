using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TFIDFScore
{
    public class Program
    {
        static int Main(string[] args)
        {
            string doc1 = "I'd like an apple";
            string doc2 = "An apple a day keeps doctor away";
            string doc3 = "Never compare an apple to an orange";
            string doc4 = "I prefer scikit-learn to orange";

            List<string> docs = new List<string>{ doc1, doc2, doc3, doc4 };
            List<string> vocabulary = getWordVocabulary(docs);

            double[,] scoresMatrix = new double[4, vocabulary.Count];

            for (int i = 0; i < docs.Count; ++i)
            {
                for (int j = 0; j < vocabulary.Count; ++j)
                {
                    double score = calculateTFIDF(docs[i], docs, vocabulary[j]);
                    scoresMatrix[i, j] = score;
                }
            }

            // Print TFIDF
            //Console.WriteLine("TF-IDF Matrix:");
            for (int i = 0; i < docs.Count; i++)
            {
                for (int j = 0; j < vocabulary.Count; j++)
                {
                    //Console.Write(scoresMatrix[i, j].ToString("F2") + "\t");
                }
                //Console.WriteLine();
            }

            // Normalize scoresMatrix
            for (int i = 0; i < docs.Count; i++)
            {
                double rowMagnitude = CalculateRowMagnitude(scoresMatrix, i, vocabulary.Count);
                for (int j = 0; j < vocabulary.Count; j++)
                {
                    scoresMatrix[i, j] = scoresMatrix[i, j] / rowMagnitude;
                }
            }


            double[,] similarityMatrix = new double[4, 4];

            // Compute the product of scoresMatrix with its transpose
            for (int i = 0; i < docs.Count; i++)
            {
                for (int j = 0; j < docs.Count; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < vocabulary.Count; k++)
                    {
                        sum += scoresMatrix[i, k] * scoresMatrix[j, k];
                    }
                    similarityMatrix[i, j] = sum;
                }
            }

            // Print similarity matrix
            Console.WriteLine("Similarity Matrix:");
            for (int i = 0; i < docs.Count; i++)
            {
                for (int j = 0; j < docs.Count; j++)
                {
                    //Console.Write(similarityMatrix[i, j].ToString("F2") + "\t");
                }
                //Console.WriteLine();
            }

            // Blanks for better readability
            //Console.WriteLine();
            //Console.WriteLine();

            double maxSimilarity = 0;
            int closestDocIndex = 0;

            for (int i = 1; i < 4; ++i)
            {
                double similarity = similarityMatrix[0, i];
                if(similarity > maxSimilarity)
                {
                    maxSimilarity = similarity;
                    closestDocIndex = i + 1; //4th document have the index 3
                }
            }

            //Console.WriteLine("Most similar document to document 1: Document " + closestDocIndex.ToString());
            Console.WriteLine(closestDocIndex.ToString());
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            return closestDocIndex;
        }

        private static List<string> getWordVocabulary(List<string> docs)
        {
            HashSet<string> words = new HashSet<string>();

            foreach (string doc in docs)
            {
                string[] wordsInDoc = tokenizeWords(doc);

                foreach (string word in wordsInDoc)
                {
                    if (!words.Contains(word))
                    {
                        words.Add(word);
                    }
                }
            }

            return words.ToList();
        }

        private static string[] tokenizeWords(string doc)
        {
            string[] words = Regex.Split(doc.ToLower(), @"[\s']+");

            return words;
        }

        private static double calculateTFIDF(string doc, List<string> docs, string inputWord)
        {
            return calculateTF(doc, inputWord) * calculateIDF(docs, inputWord);
        }


        private static float calculateTF(string doc, string inputWord)
        {
            string[] words = tokenizeWords(doc);

            int frequency = 0;

            foreach (string word in words)
            {
                if (word == inputWord)
                {
                    frequency++;
                }
            }

            float TFScore = (float)frequency / words.Length;
            //Console.WriteLine("TF: " + TFScore.ToString());

            return TFScore;
        }

        private static double calculateIDF(List<string> docs, string inputWord)
        {
            int representedDocs = 0;

            foreach (string doc in docs)
            {
                string[] words = tokenizeWords(doc);

                foreach (string word in words)
                {
                    if (word == inputWord)
                    {
                        representedDocs++;
                        break;
                    }
                }
            }

            double IDFScore = Math.Log((double)docs.Count / representedDocs);
            //Console.WriteLine("IDF: " + IDFScore.ToString());

            return IDFScore;
        }

        private static double CalculateRowMagnitude(double[,] matrix, int rowIndex, int columnCount)
        {
            double sumOfSquares = 0;
            for (int i = 0; i < columnCount; ++i)
            {
                sumOfSquares += matrix[rowIndex, i] * matrix[rowIndex, i];
            }
            return Math.Sqrt(sumOfSquares);
        }
    }
}
