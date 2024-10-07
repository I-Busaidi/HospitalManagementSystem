using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public interface ISchedulable
    {
        void ScheduleAppointment(Patient patient, DateTime date, TimeSpan period);
        void CancelAppointment(Patient patient, DateTime date, TimeSpan period);
    }
}
