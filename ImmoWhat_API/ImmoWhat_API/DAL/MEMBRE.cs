//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImmoWhat_API.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class MEMBRE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MEMBRE()
        {
            this.BIENS = new HashSet<BIENS>();
        }
    
        public int idMembre { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public int idCommune { get; set; }
        public string rue { get; set; }
        public string numero { get; set; }
        public string boite { get; set; }
        public Nullable<int> idCompte { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BIENS> BIENS { get; set; }
        public virtual COMMUNES COMMUNES { get; set; }
    }
}
