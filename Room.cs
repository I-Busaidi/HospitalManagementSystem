using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class Room
    {
        public int RoomNumber;
        public enum RoomType
        {
            General,
            ICU,
            OperationTheatre
        }
        public RoomType roomType;

        public bool IsOccupied = false;

        public Room(int RoomNumber, RoomType roomType)
        {
            this.RoomNumber = RoomNumber;
            this.roomType = roomType;
        }

        public void OccupyRoom()
        {
            IsOccupied = true;
        }

        public void VacateRoom()
        {
            IsOccupied = false;
        }
    }
}
