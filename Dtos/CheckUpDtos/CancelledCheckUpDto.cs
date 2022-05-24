using System;

namespace psw_ftn.Dtos.CheckUpDtos
{
    public class CancelledCheckUpDto
    {
        public int CancelledCheckUpId { get; set; }
        public DateTime CancelationDate { get; set; }
        public string Comment { get; set; }
    }
}