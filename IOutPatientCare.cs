using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public interface IOutPatientCare : IPatientCare
    {
        void ScheduleFollowUpAppointment(OutPatient outPatient, Clinic clinic, DateTime appointmentDay, TimeSpan appointmentTime);
    }
}
