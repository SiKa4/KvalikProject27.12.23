//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrganizationGreem.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contracts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contracts()
        {
            this.Items = new HashSet<Items>();
        }
    
        public int id_Contract { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public Nullable<int> Organozation_id { get; set; }
        public Nullable<int> Login_id { get; set; }
        public Nullable<int> Cost { get; set; }
        public Nullable<int> StatusContract_id { get; set; }
    
        public virtual StatusContrants StatusContrants { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Items> Items { get; set; }
        public virtual Logins Logins { get; set; }
        public virtual Organizations Organizations { get; set; }
    }
}
