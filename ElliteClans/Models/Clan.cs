namespace ElliteClans.Models
{
    public class Clan
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CreatedAt { get; set; }
        public long LeaderId { get; set; }
        public Member Leader { get; set; }
    }
}
