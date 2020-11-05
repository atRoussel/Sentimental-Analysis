namespace CrowdsourcingWithWords
{
	using System;
	using System.Collections.Generic;
	using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Drawing;
    using System.Drawing.Imaging;
    using WordCloud;

    class CrowdsourcingWithWords
    {
        /// <summary>
        /// Main method to run the crowdsourcing experiments presented in Simpson et.al (WWW15).
        /// </summary>
        /// 


      
        public static void Main()
        {


            var wordCloud = new WordCloud(500, 500, true, Color.White, 50, 1);
            List<String> words = new List<string>
           {
               "Holiday", "Hotel Balfour", "Torrevieja", "disappointment", "letter",
                "compensation", "brochure", "complaints", "representative", "resort",
                "matter", "return home", "response", "problems", "distress", "result" };

            List<int> frequencies = new List<int> { 4,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1 };

            var myBitmap = new Bitmap(wordCloud.Draw(words, frequencies));
            myBitmap.Save("C:/Users/athen/OneDrive/Documents/EPF/5A/IA/TP2 - Sentimental Analysis/SentimentalAnalysis/Test.jpg");
            //.C:\Users\athen\OneDrive\Documents\EPF\5A\IA\TP2 - Sentimental Analysis\SentimentalAnalysis\ResultsWords.cs


            var data = Datum.LoadData(Path.Combine("Data", "weatherTweets.tsv.gz"));

			// Run model and get results
			var VocabularyOnSubData = ResultsWords.BuildVocabularyOnSubdata((List<Datum>)data);

            BCCWords model = new BCCWords();
            ResultsWords resultsWords = new ResultsWords(data, VocabularyOnSubData);
            DataMappingWords mapping = resultsWords.Mapping as DataMappingWords;

            if (mapping != null)
            {
                resultsWords = new ResultsWords(data, VocabularyOnSubData);
                resultsWords.RunBCCWords("BCCwords", data, data, model, Results.RunMode.ClearResults, true);
            }

            using (var writer = new StreamWriter(Console.OpenStandardOutput()))
            {
                resultsWords.WriteResults(writer, false, false, false, true);
            }

            Console.WriteLine("Done.  Press enter to exit.");
            Console.ReadLine();
        }
    }
}