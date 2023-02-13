using System.ComponentModel.DataAnnotations;

namespace SampleWebApi._4.ViewModels.Items
{
    public class UpdateItemViewModel
    {
        [Required]
        public string ItemId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Code { get; set; }
        [Required]
        [MaxLength(200)]
        public string ShortDescription { get; set; }
        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }
        [Required]
        public int ItemGroupId { get; set; }
    }
}
