using System;
using System.Collections.Generic;
using psw_ftn.Dtos.UserDtos;

namespace psw_ftn.Dtos.CheckUpDtos
{
    public class CheckUpDto
    {
        public int CheckUpId { get; set; }
        public PatientDto Patient { get; set; }
        public DoctorDto Doctor { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CancellationTime { get; set; }
        public HistoryCheckUpDto HistoryCheckUp { get; set; }
        public CancelledCheckUpDto CancelledCheckUp { get; set; }
    }
}