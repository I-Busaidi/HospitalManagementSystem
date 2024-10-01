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
            IPR,
            OPR
        }
        public RoomType roomType;

        public bool IsOccupied;

        public Room(int RoomNumber, RoomType roomType)
        {
            this.RoomNumber = RoomNumber;
            this.roomType = roomType;
            IsOccupied = false;
        }

        public void OccupyRoom()
        {
            IsOccupied = true;
        }

        public void VacateRoom()
        {
            IsOccupied = false;
        }

        public bool GetOccupiedStatus()
        {
            return IsOccupied;
        }

        public string DisplayRoomInfo()
        {
            string RoomStatus;
            if (IsOccupied)
            {
                RoomStatus = "Busy";
            }
            else
            {
                RoomStatus = "Available";
            }
            return $"Room Number: {RoomNumber} | Room Type: {roomType} | Room Status: {RoomStatus}";
        }
    }
}
