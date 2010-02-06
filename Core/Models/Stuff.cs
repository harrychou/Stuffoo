namespace Stuffoo.Core.Models
{
    public interface IIdentifiable
    {
        int ID { get; }
    }

    public class Stuff: IIdentifiable
    {
        public virtual int ID { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
    }
}