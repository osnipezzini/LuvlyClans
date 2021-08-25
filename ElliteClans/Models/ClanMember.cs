using System;

namespace ElliteClans.Models
{
    public class ClanMember
    {
        public long Id { get; set; }
        public long ClanId { get; set; }
        public virtual Clan Clan { get; set; }
        public long MemberId { get; set; }
        public virtual Member Member { get; set; }
        public long ClanArmyId { get; set; }
        public virtual ClanArmy ClanArmy { get; set; }
        public DateTime JoinedDate { get; set; }
        public long? GodfatherId { get; set; }
        public virtual Member Godfather { get; set; }
    }
}
