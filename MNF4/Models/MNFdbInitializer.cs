using System;
using System.Collections.Generic;
//using System.Data.Entity.Database;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.Entity;

namespace MNF4.Models
{
    public class MNFdbInitializer : DropCreateDatabaseIfModelChanges<MNFdb>
                                    //DropCreateDatabaseAlways<MNFdb>
    {
        protected override void Seed(MNFdb context)
        {
            // default Database entries for testing

            #region Sources defaults
            //
            // Add Sources for contacts, clients
            context.Sources.Add(new Source
            {
                ID = 1,
                Name = "Contact Form Submission",
                Notes = "Submitted via the Contact Form"
            });
            context.Sources.Add(new Source
            {
                ID = 2,
                Name = "Telephone call - ARC Referral",
                Notes = "Telephone call from a referral from ARC"
            });
            context.Sources.Add(new Source
            {
                ID = 3,
                Name = "PCOS Webinar Opt-In",
                Notes = "Opt-In from from PCOS Webinar page"
            });
            context.Sources.Add(new Source
            {
                ID = 4,
                Name = "Restaurant Opt-In",
                Notes = "Contact requested the Restaurant Guide from the Home page."
            });
            context.Sources.Add(new Source
            {
                ID = 5,
                Name = "Celiac Opt-In",
                Notes = "Contact requested the 'Go Gluten Free' document from the Celiac page."
            });
            #endregion

            #region Contact defaults
            //
            // Add default Contact records
            context.Contacts.Add(new Contact
            {
                FirstName = "Dan",
                LastName = "Marquette",
                EmailAddress = "djmarquette@gmail.com",
                Phone = "512-913-4000",
                SubmitDate = DateTime.Parse("5/20/2000"),
                Contacted = true,
                ContactNotes = "Left voicemail",
                SourceID = 2
            });

            context.Contacts.Add(new Contact
            {
                FirstName = "Chris",
                LastName = "Marquette",
                EmailAddress = "chris@marquettenutrition.com",
                Phone = "512-468-4338",
                SubmitDate = DateTime.Parse("3/15/1972"),
                Contacted = true,
                ContactNotes = "Woke up right next to her :-) ",
                SourceID = 1
            });

            context.Contacts.Add(new Contact
            {
                FirstName = "Sally",
                LastName = "Brown",
                EmailAddress = "lilsis@peanuts.com",
                Phone = "512-555-1212",
                SubmitDate = DateTime.Parse("9/05/2011"),
                Contacted = false,
                SourceID = 1

            });
            context.Contacts.Add(new Contact
            {
                FirstName = "Charlie",
                LastName = "Brown",
                EmailAddress = "blockhead@peanuts.com",
                Phone = "512-555-1212",
                SubmitDate = DateTime.Parse("9/05/2011"),
                Contacted = false,
                SourceID = 2

            });
            #endregion

            #region Client defaults
            //
            // Add default Client records
            context.Clients.Add(new Client
            {
                FirstName = "Dan",
                LastName = "Marquette",
                EmailAddress = "djmarquette@gmail.com",
                Address = "6006 Blanco River Pass",
                City = "Austin",
                State = "TX",
                ZipCode = "78749",
                Phone = "512-913-4000",
                Notes = "One awesome guy !!",
                DOB = DateTime.Parse("05/08/1964"),
                StartWeight = 175.5F,
                Doctor = "Dr. Perez",
                MaritalStatus = "M",
                Occupation = "Superhero / Dog trainer",
                SourceID = 1

            });

            context.Clients.Add(new Client
            {
                FirstName = "Chris",
                LastName = "Marquette",
                EmailAddress = "chris@marquettenutrition.com",
                Address = "6006 Blanco River Pass",
                City = "Austin",
                State = "TX",
                ZipCode = "78749",
                Phone = "512-468-4338",
                Notes = "A smokin hot babe !!!",
                DOB = DateTime.Parse("03/15/1972"),
                StartWeight = 115.0F,
                Doctor = "Dr. Frankenstein",
                MaritalStatus = "M",
                Occupation = "Supermodel / Registered Dietitian",
                SourceID = 1
            });
            #endregion

            #region Appointment
            context.Appointments.Add(new Appointment
            {
                ID = 1,
                ClientID = 1,
                Date = DateTime.Parse("5/8/2011"),
                Time = DateTime.Parse("2:00PM"),
                Notes = "This is Dan's test appointment note"
            });

            context.Appointments.Add(new Appointment
            {
                ID = 2,
                ClientID = 2,
                Date = DateTime.Parse("3/15/2011"),
                Time = DateTime.Parse("3:00PM"),
                Notes = "This is Chris' test appointment note"
            });

            #endregion

            context.SaveChanges();


#if DEBUG   // double check, along with comment out init code in Global.asax
            base.Seed(context);
#endif
        }
    }
}