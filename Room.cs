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
            Console.WriteLine($"Room {RoomNumber} is now occupied.");
        }

        public void VacateRoom()
        {
            IsOccupied = false;
            Console.WriteLine($"Room {RoomNumber} is now vacated.");
        }

        public bool GetOccupiedStatus()
        {
            return IsOccupied;
        }

        public string DisplayRoomInfo()
        {
            string RoomStatus = IsOccupied ? "Busy" : "Available";
            return $"Room Number: {RoomNumber} | Room Type: {roomType} | Room Status: {RoomStatus}";
        }
    }
}
