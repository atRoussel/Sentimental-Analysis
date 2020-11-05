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