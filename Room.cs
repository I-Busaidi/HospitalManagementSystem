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
        public enum RoomType;

        public bool IsOccupied = false;

        public Room(int RoomNumber)
        {
            this.RoomNumber = RoomNumber;
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
