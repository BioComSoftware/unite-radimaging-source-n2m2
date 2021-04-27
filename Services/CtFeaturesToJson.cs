using System;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace unite.radimaging.source.n2m2.Services {
    public class CtFeaturesToJson : CsvToString {
        public static string CtFeaturestoJSON(string Filename) {
            string csvstr = CSVFiletoString(Filename);
            // FUTURE: Create a Data.CtFeatures object here for manipulation if needed. 
            return Regex.Unescape(JsonSerializer.Serialize(csvstr));
        }
    }
}