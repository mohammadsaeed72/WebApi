using SampleWebApi._1.Entities.Base;

namespace SampleWebApi._1.Entities
{
    public class ItemGroup:BaseEntity,IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Item> Items { get; set; }
    }
}
