using psw_ftn.Dtos.CheckUpDtos;

namespace psw_ftn.Dtos.FeedbackDtos
{
    public class AddFeedbackDto
    {   
        public GradeDto Grade { get; set; }
        public string Comment { get; set; }
        public bool isForDisplay { get; set; } = false;
        public bool incognito { get; set; } = false;
    }
}