using System.Text.Json;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace unite.radimaging.source.n2m2.CSVParsers {
    public class MRIFeaturesCSVParser : CSVParser {
        public static string CSVFiletoJSON(string Filename) {
            string csvstr = CSVFiletoString(Filename);
            return JsonSerializer.Serialize(csvstr);
        }
    }
}