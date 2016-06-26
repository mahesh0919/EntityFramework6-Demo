using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtabaseFirstApproach
{
    class Program
    {
        static void Main(string[] args)
        {

            EDMQuey();

            //DBCommandLogging();
            //TrackChanges();
            //ValidatePatientRecord();
            //GetRecords();
            //AddRecords();
            //FetchRecords();
            //CRUDOperation();
            //Transaction();

            Console.WriteLine("Press Any Key");
            Console.Read();
        }

        public static void EDMQuey()
        { 
            //Linq to Entities
            using (var db = new HospetalDBFirstEntities())
            {
                var query = from p in db.Patients
                            where p.FirstName == "Vikram"
                            select p;

                Console.WriteLine("Fetching all Patints from Linq to Entities");

                foreach (Patient p in query)
                {
                    Console.WriteLine("Patient Name from Linq to Entities: " + p.FirstName);
                }
            }
            //Linq to Entity SQL
            using (var con = new EntityConnection("name=HospetalDBFirstEntities"))
            {
                con.Open();
                EntityCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Value p FROM HospetalDBFirstEntities.patients as p where p.FirstName = 'Vikram'";
                using (EntityDataReader rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection))
                {
                    while (rdr.Read())
                    {
                        Console.WriteLine("Patient Name: from Linq to Entity SQL: " + rdr["FirstName"]);
                    }
                }
            }

            ///Native SQL:
            //using (var db = new HospetalDBFirstEntities())
            //{
            // var studentName = db.Patients.SqlQuery("Select firstname from patients where firstname='Vikram'").ToList();
            //}


        }

        public static void DBCommandLogging()
        {
            using (var db = new HospetalDBFirstEntities())
            {
                db.Database.Log = Logger;
                var patients = (from p in db.Patients
                               where p.FirstName == "mahesh"
                               select p);
                foreach (Patient p in patients)
                {
                    // commang logging will be fired on accessing an entity 
                }

            }
        }

        public static void Logger(string dbcommand)
        {
            Console.WriteLine(dbcommand);
        }

        public static void TrackChanges()
        {
            using (var db = new HospetalDBFirstEntitiesOverride())
            {
                db.Configuration.AutoDetectChangesEnabled = true;

                Patient patient = new Patient()
                {
                    Address = "Hyderabad",
                    FirstName = "123",
                    PatientLastName = "Validation"
                };
                db.Patients.Add(patient);

                //Update Record
                //GetPatientsList();

                Console.WriteLine("\nContext tracking changes of {0} entity.", db.ChangeTracker.Entries().Count());
                var entries = db.ChangeTracker.Entries();
                foreach (var entry in entries)
                {
                    Console.WriteLine("Entity Name: {0}", entry.Entity.GetType().Name);
                    Console.WriteLine("Status: {0}", entry.State);
                }

                db.SaveChanges();

            }
        }
        public static void ValidatePatientRecord()
        {
            using (var db = new HospetalDBFirstEntitiesOverride())
            {
                Patient patient = new Patient()
                {
                    Address = "Hyderabad",
                    //FirstName = "Test",
                    PatientLastName = "Validation"
                };
                db.Patients.Add(patient);
                db.SaveChanges();
            }

            GetPatientsList();

        }

        public static void AddRecords()
        {
            using (var db = new HospetalDBFirstEntities())
            {
                //Adding new Doctor  ---------------------------------------------
                Doctor doctor1 = new Doctor()
                {
                    DoctorFirstName = "Mahesh123",
                    DoctorLastName = "Pendker",
                    EmailAddress = "mahesh0919@gmail.com"
                };

                Doctor doctor2 = new Doctor()
                {
                    DoctorFirstName = "Rakesh123",
                    DoctorLastName = "Pendker",
                    EmailAddress = "rakesh.p@gmail.com"
                };

                //Adding Patents ---------------------------------------------
                Patient patient1 = new Patient()
                {
                    Address = "Hyderabad",
                    FirstName = "Vikram",
                    PatientLastName = "Voma"
                };

                Patient patient2 = new Patient()
                {
                    Address = "Hyderabad",
                    FirstName = "Vikram",
                    PatientLastName = "Voma"
                };

                db.Patients.Add(patient1);
                db.Patients.Add(patient2);

                //Adding Appointments ------------------------------------------
                Appointment appointment1 = new Appointment() { AppointmentTime = DateTime.Now.Date, Doctor = doctor1, Patient = patient1 };
                Appointment appointment2 = new Appointment() { AppointmentTime = DateTime.Now.Date, Doctor = doctor2, Patient = patient2 };
                db.Appointments.Add(appointment1);
                db.Appointments.Add(appointment2);

                db.Doctors.Add(doctor1);
                db.Doctors.Add(doctor2);

                db.SaveChanges();
            }

        }

        public static void GetRecords()
        {
                 // Now Fetch records
                using (var db = new HospetalDBFirstEntitiesOverride())
                {
                    var query = from p in db.Patients
                                select p;

                    Console.WriteLine("Fetching all Patints");

                    foreach (Patient p in db.Patients)
                    {
                        Console.WriteLine("Doctor Name: " + p.FirstName);
                    }
                }
        }


        public static void FetchRecords()
         {
             GetPatientsList();
             GetDoctorsList();
         }

        public static void CRUDOperation()
        { 
           // Add records
         using(var db = new HospetalDBFirstEntities())
         {
             Doctor d1 = new Doctor() { DoctorFirstName ="David", Age= 40, DoctorLastName="Young", EmailAddress="test@gmail.com", Gender = "M" };
             Doctor d2 = new Doctor() { DoctorFirstName = "DavidTest", Age = 40, DoctorLastName = "Young", EmailAddress = "test@gmail.com", Gender = "M" };

             db.Doctors.Add(d1);
             db.Doctors.Add(d2);
             db.SaveChanges();
         }

        //Update Record
         using (var db = new HospetalDBFirstEntities())
         {
             var doctors = (from d in db.Doctors
                            where d.DoctorFirstName == "David"
                            select d);

             foreach (Doctor d in doctors)
             {
                 d.DoctorFirstName = "David Update";
             }
             db.SaveChanges();
         }

        //Delete Record
         using (var db = new HospetalDBFirstEntities())
         {
             var doctors = (from d in db.Doctors
                                    where d.DoctorFirstName == "DavidTest"
                                    select d);
             foreach (Doctor d in doctors)
             {
                 db.Doctors.Remove(d);
             }
             db.SaveChanges();
         }

        //Select Records
         using (var db = new HospetalDBFirstEntities())
         {
             foreach (Doctor d in db.Doctors)
             {
                 Console.WriteLine("Doctor Name: "+ d.DoctorFirstName);
             }
         }

        }

        public static void Transaction()
        {
            //using (var db = new HospetalDBFirstEntities())
            //{
            //    Patient p1 = new Patient()
            //    {
            //        Address = "Hyderabad",
            //        Age = 15,
            //        FirstName = "Masood",
            //        Gender = "M",
            //        PatientLastName = "Ali"
            //    };
            //    db.Patients.Add(p1);

            //    Patient p2 = new Patient()
            //    {
            //        Address = "Warangal",
            //        Age = 15,
            //        FirstName = "Shanmukh",
            //        Gender = "M",
            //        PatientLastName = "Sai"
            //    };
            //    db.Patients.Add(p2);
            //    db.SaveChanges();        // In a single transaction add above 2 records
            //}

            // use transaction object
            using(var db = new HospetalDBFirstEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Patient p1 = new Patient()
                        {
                            Address = "Hyderabad",
                            Age = 15,
                            FirstName = "Masood",
                            Gender = "M",
                            PatientLastName = "Ali"
                        };
                        db.Patients.Add(p1);
                        db.SaveChanges();

                        Patient p2 = new Patient()
                        {
                            Address = "Warangal",
                            Age = 15,
                            FirstName = "Shanmukh",
                            Gender = "M",
                            PatientLastName = "Sai"
                        };
                        db.Patients.Add(p2);
                        db.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }

            GetPatientsList();
        }

        public static void GetPatientsList()
        {
            // Now Fetch records
            using (var db = new HospetalDBFirstEntities())
            {
                var query = from p in db.Patients
                            select p;

                Console.WriteLine("Fetching all Patints");

                foreach (Patient p in db.Patients)
                {
                    Console.WriteLine("Doctor Name: " + p.FirstName);
                }
            }
        }

        public static void GetDoctorsList()
        {
            using (var db = new HospetalDBFirstEntities())
            {
                var query = from d in db.Doctors
                            select d;

                Console.WriteLine("\n\nFetching all Doctors");

                foreach (Doctor d in db.Doctors)
                {
                    Console.WriteLine("Doctor Name: " + d.DoctorFirstName);
                }
            }
        }
    
    
    }
}
