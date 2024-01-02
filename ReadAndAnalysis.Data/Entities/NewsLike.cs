
namespace ReadAndAnalysis.Data.Entities
{
    public class NewsLike
    {
        public long Id { get; set; }
        public long NewsId { get; set; }
        public long NewsLinkeId { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? LastModifiedDate { get; set; }
        public long? UserModifiedId { get; set; }
        public string? ModifiedIp { get; set; }

    }
}
