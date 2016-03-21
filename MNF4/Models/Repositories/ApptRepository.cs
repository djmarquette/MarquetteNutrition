using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNF4.Models
{
    public class ApptRepository : IAppointmentRepository
    {
        MNFdb _db = new MNFdb();

        #region IRepository<Appointment> Members

        public IQueryable<Appointment> ListAppts()
        {
            return from appt in All
                   orderby appt.Date descending
                   select appt;
        }

        public IQueryable<Appointment> All
        {
            get { return _db.Appointments; }
        }

        public void InsertOrUpdate(Appointment table)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException(); 
        }
        #endregion

        // Persistence
        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}