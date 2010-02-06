using FluentNHibernate.Mapping;
using Stuffoo.Core.Models;

namespace Stuffoo.Core.Mapping
{

    public class StuffMap : ClassMap<Stuff>
    {
        public StuffMap()
        {
            Table("Stuff");

            Id(x => x.ID);
            Map(x => x.Title);
            Map(x => x.Description);
        }
    }
}


