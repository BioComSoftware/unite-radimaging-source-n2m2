using System;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace unite.radimaging.source.n2m2.Services {
    public class MriFeaturesToJson : CsvToString {
        public static string MriFeaturestoJSON(string Filename) {
            string csvstr = CSVFiletoString(Filename);
            // FUTURE: Create a Data.MRIFeatures object here for manipulation if needed. 
            return Regex.Unescape(JsonSerializer.Serialize(csvstr));
        }
    }
}