
using MarkovSharp.TokenisationStrategies;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovGen
{
    public class Program
    {
        static void Main(string[] args)
        {
            var model = new StringMarkov(2);
            var jArray = JArray.Parse(File.ReadAllText(@"pokemon.json"));
            var source = jArray.Select(x => x.Value<string>()).ToList();
            var names = source;
            for(int i=0; i< names.Count; i++) {
                var name = names[i];
                int insertIndex = name.Length / 2;
                var charArray = name.ToCharArray().ToList();
                //charArray.Insert(insertIndex, ' ');
                names[i] = string.Join(" ", charArray);
            }
            model.Learn(names);

            //GeneratorFacade gen = new GeneratorFacade(new MarkovGenerator(File.ReadAllText("book.txt")));

            while (true)
            {
                Console.ReadLine();
                var output = model.Walk().First();
                output = output.Replace(" ", "");
                //var output = gen.GenerateSentence(10);
                var result = output;
                if (source.Any(x => x == output))
                {
                    result = "did not change";
                }
                Console.WriteLine(result);
            }
        }
    }
}
