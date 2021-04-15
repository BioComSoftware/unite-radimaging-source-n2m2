using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unite.radimaging.source.n2m2.Entities {
    public class MRIFeatures {
        // Variable capitalization intentionally nonstandard to match strings from CSV files
        // Decimal used because numbers need to remain exact, real world
        public string  Patient_ID { get; set; }
        public decimal Whole_Tumor { get; set; }
        public decimal Contrast_Enhancing { get; set; }
        public decimal Noncontrast_Enhancing { get; set; }
        public decimal Median_ADC_Tumor { get; set; }
        public decimal Median_ADC_CE { get; set; }
        public decimal Median_ADC_Edema { get; set; }
        public decimal Median_CBF_Tumor { get; set; }
        public decimal Median_CBF_CE { get; set; }
        public decimal Median_CBF_Edema { get; set; }
        public decimal Median_CBV_Tumor { get; set; }
        public decimal Median_CBV_CE { get; set; }
        public decimal Median_CBV_Edema { get; set; }
        public decimal Median_MTT_Tumor { get; set; }
        public decimal Median_MTT_CE { get; set; }
        public decimal Median_MTT_Edema { get; set; }
    }
}
