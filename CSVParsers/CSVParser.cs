using ChoETL;
using Serilog;
//using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace unite.radimaging.source.n2m2.CSVParsers {
    public class CSVParser {

        public static string CSVFiletoString(string Filename, string delimiter = ";" ) {
            string csv;
            try {
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
            } catch (Exception e) {
                string msg = $"CSVParser.CSVFiletoString: {e.Message}";
                Log.Error(msg);
                return msg; // repeats itself
            }
        }
    }
}