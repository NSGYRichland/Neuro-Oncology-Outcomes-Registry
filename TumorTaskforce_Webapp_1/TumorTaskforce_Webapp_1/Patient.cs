//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TumorTaskforce_Webapp_1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Patient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient()
        {
            this.FamilyHistoryPivots = new HashSet<FamilyHistoryPivot>();
            this.HealthFactorsPivots = new HashSet<HealthFactorsPivot>();
            this.OtherMedsPivots = new HashSet<OtherMedsPivot>();
            this.SymptomsPivots = new HashSet<SymptomsPivot>();
            this.TreatmentsPivots = new HashSet<TreatmentsPivot>();
        }

        public void CreatePatient(Patient pat)
        {
            throw new NotImplementedException();
        }

        public int patientID { get; set; }
        public string Sex { get; set; }
        public bool Married { get; set; }
        public byte Age { get; set; }
        public string HistologicalClassification { get; set; }
        public Nullable<byte> HistologicalGrade { get; set; }
        public short TumorWidth { get; set; }
        public short TumorHeight { get; set; }
        public short TumorLength { get; set; }
        public string TumorLocation { get; set; }
        public string Constitutional { get; set; }
        public string Respiratory { get; set; }
        public string Cardiovascular { get; set; }
        public string Gastrointestinal { get; set; }
        public string Musculoskeletal { get; set; }
        public string Integumentary { get; set; }
        public string Neurologic { get; set; }
        public string Exercize { get; set; }
        public string Diet { get; set; }
        public bool isCompare { get; set; }
        public string userName { get; set; }
        public string comparisonResults { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FamilyHistoryPivot> FamilyHistoryPivots { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HealthFactorsPivot> HealthFactorsPivots { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OtherMedsPivot> OtherMedsPivots { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SymptomsPivot> SymptomsPivots { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TreatmentsPivot> TreatmentsPivots { get; set; }

        public object GetPatients()
        {
            throw new NotImplementedException();
        }
    }
}
