using SampleWebApi._1.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleWebApi._1.Entities
{
    public class Address:BaseEntity,IEntity
    {
        [ForeignKey(nameof(AppUser))]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Name { get; set; }
        public string DeliverTo { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string DeliveryAddress { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
