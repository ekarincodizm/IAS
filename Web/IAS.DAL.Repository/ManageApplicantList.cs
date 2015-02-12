using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ManageApplicantList
    {

        public string RoomCode { get; set; }
        public string RoomName { get; set; }
        public int NumSeat { get; set; }

        public string IDCard { get; set; }
    }
}
