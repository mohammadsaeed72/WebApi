using System.ComponentModel.DataAnnotations;

namespace SampleWebApi._4.ViewModels.Items
{
    public class ItemFeatureViewModel 
    {
        [Required]
        public int FeatureId { get; set; }
        [Required]
        public string FeatureName { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
