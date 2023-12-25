using System.ComponentModel.DataAnnotations;

namespace ReadAndAnalysis.Data.Entities
{
    public class SendSms
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string? Mobile { get; set; }
        [MaxLength(200)]
        public string? SendedText { get; set; }
        [MaxLength(200)]
        public string? Detail { get; set; }
        public bool IsSended { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? SendDate { get; set; }
        public long? NotOilNewsForSendingSmsId { get; set; }
    }
}
