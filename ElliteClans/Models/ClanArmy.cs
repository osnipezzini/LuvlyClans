using System.Collections.Generic;

namespace ElliteClans.Models
{
    public class ClanArmy
    {
        public long Id { get; set; }
        public long ClanId { get; set; }
        public virtual Clan Clan { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<ClanPermission> ClanPermissions { get; set; }
    }
}
