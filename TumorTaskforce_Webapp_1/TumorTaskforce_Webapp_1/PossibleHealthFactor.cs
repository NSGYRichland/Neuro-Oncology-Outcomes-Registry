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
    
    public partial class PossibleHealthFactor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PossibleHealthFactor()
        {
            this.HealthFactorsPivots = new HashSet<HealthFactorsPivot>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int TempPatientID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HealthFactorsPivot> HealthFactorsPivots { get; set; }
    }
}
