//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DtabaseFirstApproach
{
    using System;
    using System.Collections.Generic;
    
    public partial class Doctor
    {
        public Doctor()
        {
            this.Appointments = new HashSet<Appointment>();
        }
    
        public int DoctorID { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string EmailAddress { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
