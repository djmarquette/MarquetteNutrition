using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MNF4.Models
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        IQueryable<Appointment> ListAppts();
    }
}
