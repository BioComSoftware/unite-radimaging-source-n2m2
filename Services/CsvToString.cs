using ChoETL;
using Serilog;
using System;
using System.IO;
using System.Text;

namespace unite.radimaging.source.n2m2.Services {
    public class CsvToString {

        public static string CSVFiletoString(string Filename, string delimiter = ";") {
            string csv;
            // Catch errors in main code
            csv = File.ReadAllText(Filename);
            StringBuilder sb = new StringBuilder();
            using (var p = ChoCSVReader
                .LoadText(csv)
                .WithDelimiter(delimiter)
                .WithFirstLineHeader()
                ) {
                using (var w = new ChoJSONWriter(sb))
                    w.Write(p);

                return sb.ToString();
            }
        }
    }
}