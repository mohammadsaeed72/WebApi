using System.ComponentModel.DataAnnotations;

namespace SampleWebApi._4.ViewModels.Items
{
    public class UpdateItemFeature
    {
        [Required]
        public string ItemId { get; set; }
        [Required]
        public int FeatureId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Value { get; set; }
    }
}
