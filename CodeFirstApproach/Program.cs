using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework123
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayRecords();
            
        }

        public static void DisplayRecords()
        {
            using (HospetalContext hContext = new HospetalContext())
            {

                //Display Doctors List
                var doctors = (from d in hContext.Doctors
                               orderby d.DoctorFirstName
                               select d).ToList();

                Console.WriteLine("-------List of doctors in hospetal------");
                foreach (Doctor d in doctors)
                {
                    Console.WriteLine("Doctor FirstName: " + d.DoctorFirstName);
                }

                var patients = (from p in hContext.Patients
                                orderby p.PatientFirstName
                                select p).ToList();
                
                Console.WriteLine("\nList of Patients in hospetal");
                foreach (Patient p in hContext.Patients)
                {
                    Console.WriteLine("Patients Name: " + p.PatientFirstName);
                }

                Console.WriteLine("Press any Key");
                Console.Read();
            }
        }
    
    }
}
