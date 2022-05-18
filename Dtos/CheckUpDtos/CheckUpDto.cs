using System;
using psw_ftn.Dtos.UserDtos;

namespace psw_ftn.Dtos.CheckUpDtos
{
    public class CheckUpDto
    {
        public int ChechUpId { get; set; }
        public PatientDto Patient { get; set; }
        public DoctorDto Doctor { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Priority { get; set; }
        public DateTime CancellationTime { get; set; }
    }
}