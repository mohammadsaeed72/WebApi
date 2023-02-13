using System.ComponentModel.DataAnnotations;

namespace SampleWebApi._1.Entities.Base
{
    public class Entity
    {
        public DateTime InsertedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
    }
    public class BaseEntity<TKey>: Entity
    {
        [Key]
        public TKey Id { get; set; }
        
    }
    public class BaseEntity: Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
