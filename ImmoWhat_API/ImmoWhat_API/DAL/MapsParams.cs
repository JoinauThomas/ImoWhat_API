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
    
    public partial class MapsParams
    {
        public string codePostal { get; set; }
        public string Description { get; set; }
        public Nullable<int> idCouleur { get; set; }
        public string GMapPartRecord_Id { get; set; }
        public string StartZoom { get; set; }
        public string StopZoom { get; set; }
        public string Commune { get; set; }
        public string Quarter { get; set; }
        public Nullable<long> Price { get; set; }
        public Nullable<int> idCouleurApp { get; set; }
        public Nullable<int> idCouleurTer { get; set; }
        public Nullable<int> idCouleurVilla { get; set; }
        public Nullable<long> PriceA { get; set; }
        public Nullable<long> PriceT { get; set; }
        public Nullable<long> PriceV { get; set; }
    }
}
