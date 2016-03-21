using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Objects;


namespace MNF4.Models
{
    public class MNFdb : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Chart> Charts { get; set; }
        public DbSet<Media> Media  { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<MNFEvent> MNFEvents { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }


        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            // can use modelBuilder to map model column names to different db column names, etc.
            base.OnModelCreating(modelBuilder);
        }

        protected void Seed(MNFdb context)
        {
            // default Database entries for testing - context.SaveChanges() called after each table

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
            context.SaveChanges();
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
            context.SaveChanges();
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
            context.SaveChanges();
            #endregion

            #region EventType defaults
            //
            // Add default EventType records
            context.EventTypes.Add(new EventType
            {
                Name = "Open House",
                Description = "Open House Description"
            });

            context.EventTypes.Add(new EventType
            {
                Name = "Seminar",
                Description = "Seminar Description"
            });

            context.EventTypes.Add(new EventType
            {
                Name = "Class",
                Description = "Class Description"
            });

            context.EventTypes.Add(new EventType
            {
                Name = "Lunch and Learn",
                Description = "Lunch and Learn Description"
            });
            context.SaveChanges();

            #endregion  //  EventType defaults

            #region MNFEvents defaults
            //
            // defaults for MNFEvents table (Events is a reserved word)
            // none yet
            context.MNFEvents.Add(new MNFEvent
            {
                Name = "MNF Open House 2013",
                EventTypeID = 1,
                Cost = "$0.00",
                Description = "Actual Open House from 2013",
                EventDate = DateTime.Parse("2014-03-02 00:00:00"),
                StartTime = DateTime.Parse("2014-03-02 13:30:00"),
                EndTime = DateTime.Parse("2014-03-02 15:00:00"),
                Location = "MNF Office",
                ShowEvent = true,
                EventMarkup = @"<div><h2 class=""center"" style=""text-align: center;"">Marquette Nutrition &amp; Fitness Open House</h2>
                    <h3 class=""center"" style=""text-align: center;"">Saturday, March 2nd from 1:30 PM - 4:00 PM</h3>
                    <h3 class=""center"" style=""text-align: center;"">3421 W William Cannon Drive, Suite 145, Austin, TX 78745</h3></div>"
            });
            context.MNFEvents.Add(new MNFEvent
            {
                Name = "MNF Holiday Challenge",
                EventTypeID = 2,
                Cost = "$0.00",
                Description = "Christmas Holiday Challenge",
                EventDate = DateTime.Parse("2014-12-17 00:00:00"),
                StartTime = DateTime.Parse("2014-12-17 18:00"),
                EndTime = DateTime.Parse("2014-12-17 21:00"),
                Location = "MNF Office",
                ShowEvent = true,
                EventMarkup = @"<h1 style=""text-align: center;"">Holiday Challenge: Take Charge of Your Health</h1>",
            });
            context.MNFEvents.Add(new MNFEvent()
                {
                    Name = "What Can an RD do for You?",
                    EventTypeID = 4,
                    Cost = "$0.00",
                    Description = "A talk about what registered dietitians can do for you",
                    EventDate = DateTime.Parse("2013-05-08"),
                    StartTime = DateTime.Parse("2013-05-08 11:30"),
                    EndTime = DateTime.Parse("2013-05-08 12:30"),
                    Location = "MNF Office",
                    ShowEvent = true,
                    EventMarkup = @"<h1 style=""text-align: center;"">What can an RD do for You?</h1>",
                });
            context.SaveChanges();

            #endregion

            #region Appointment defaults

            context.Appointments.Add(new Appointment
            {
                ClientID = 1,
                Date = DateTime.Parse("5/8/2011"),
                Time = DateTime.Parse("2:00PM"),
                Notes = "This is Dan's test appointment note"
            });
            context.Appointments.Add(new Appointment
            {
                ClientID = 2,
                Date = DateTime.Parse("3/15/2011"),
                Time = DateTime.Parse("3:00PM"),
                Notes = "This is Chris' test appointment note"
            });
            context.SaveChanges();
            #endregion

            #region PromoCode defaults

            context.PromoCodes.Add(new PromoCode
            {
                Code = "Transform",
                Name = "Transform Your Health Through Nutrition",
                Description = "Telesummit Promo Brochure",
                Message = @"You are now eligible to receive my ebook ""Transform Your Health Through Nutrition"" FREE!!",
                Document = @"Transform Your Health Through Nutrition.pdf",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(3),
                Active = true,
                Markup = @"<p>Welcome, listeners of the Telesummit <strong>""Mr. Wrong to Mr. Right: Super Successful Strategies to Dating with Confidence after Divorce.""</strong></p>"
            });
            context.PromoCodes.Add(new PromoCode
            {
                Code = "PromoCode2",
                Description = "PromoCode2 Description",
                Message = @"PromoCode2 Message",
                Document = @"Transform Your Health Through Nutrition.pdf",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(3),
                Active = true,
                Markup = @"<p>PromoCode2 Markup goes here</p>"
            });
            context.SaveChanges();
            #endregion

            #region MediaType defaults

            context.MediaTypes.Add(new MediaType
                {
                    Extension = "pdf",
                    Name = "Adobe PDF document",
                    Description = "Adobe PDF document"
                });
            context.MediaTypes.Add(new MediaType
            {
                Extension = "doc",
                Name = "MS Word doc",
                Description = "Microsoft Word document"
            });
            context.MediaTypes.Add(new MediaType
            {
                Extension = "mp3",
                Name = "mp3 audio file",
                Description = "Digital audio mp3 file"
            });
            context.SaveChanges();

            #endregion

            #region Media defaults

            context.Media.Add(new Media
                {
                    Name = @"FOMO Radio-11_9- Chris Marquette.mp3",
                    Description = "Radio broadcast interview w/Carrie Barrett",
                    Path = "/Content/Media",
                    TypeID = (MediaTypes.FirstOrDefault(t => t.Extension == "mp3").ID)
                });

            #endregion

        }

        public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<MNFdb>
                                                     //DropCreateDatabaseAlways<MNFdb> 
        {
            protected override void Seed(MNFdb context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }

        public class CreateInitializer : CreateDatabaseIfNotExists<MNFdb>
        {
            protected override void Seed(MNFdb context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }
        
        static MNFdb()
        {
#if DEBUG
            Database.SetInitializer<MNFdb>(new DropCreateIfChangeInitializer());
#else
            Database.SetInitializer<MNFdb> (new CreateInitializer ());
#endif
        }
    }
}