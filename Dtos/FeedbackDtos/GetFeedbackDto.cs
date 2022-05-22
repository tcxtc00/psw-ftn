using psw_ftn.Dtos.CheckUpDtos;

namespace psw_ftn.Dtos.FeedbackDtos
{
    public class GetFeedbackDto
    {
        public int FeedbackId { get; set; }
        public GradeDto Grade { get; set; }
        public string Comment { get; set; }
        public bool IsForDisplay { get; set; } = false;
        public bool Incognito { get; set; } = false;
        public PatientDto Patient { get; set; }
    }
}