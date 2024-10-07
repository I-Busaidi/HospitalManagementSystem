using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public interface IInPatientCare : IPatientCare
    {
        void AssignRoomToPatient(InPatient patient, Room room);
        void DischargePatient(InPatient patient);
    }
}
