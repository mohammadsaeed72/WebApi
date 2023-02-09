using SampleWebApi._1.Entities.Base;

namespace SampleWebApi._1.Entities
{
    public class Feature : BaseEntity, IEntity
    {
        public string Name { get; set; }
    }
}
