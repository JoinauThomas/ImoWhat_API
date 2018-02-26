﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ImmoWhatEntities : DbContext
    {
        public ImmoWhatEntities()
            : base("name=ImmoWhatEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BIEN> BIEN { get; set; }
        public virtual DbSet<COMMUNE> COMMUNE { get; set; }
        public virtual DbSet<MEMBRE> MEMBRE { get; set; }
        public virtual DbSet<OPTIONS> OPTIONS { get; set; }
        public virtual DbSet<RefCouleur> RefCouleur { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TYPEBIEN> TYPEBIEN { get; set; }
        public virtual DbSet<VAL_PRIX_MOYEN_BIEN> VAL_PRIX_MOYEN_BIEN { get; set; }
        public virtual DbSet<BIEN_PHOTOS> BIEN_PHOTOS { get; set; }
        public virtual DbSet<MapsParams> MapsParams { get; set; }
        public virtual DbSet<StatImmo> StatImmo { get; set; }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int addNewMembre(string mail, string nom, string prenom, string commune, string rue, string numero, string boite, Nullable<System.DateTime> dateDeNaissance, string telephone, string photo)
        {
            var mailParameter = mail != null ?
                new ObjectParameter("mail", mail) :
                new ObjectParameter("mail", typeof(string));
    
            var nomParameter = nom != null ?
                new ObjectParameter("nom", nom) :
                new ObjectParameter("nom", typeof(string));
    
            var prenomParameter = prenom != null ?
                new ObjectParameter("prenom", prenom) :
                new ObjectParameter("prenom", typeof(string));
    
            var communeParameter = commune != null ?
                new ObjectParameter("commune", commune) :
                new ObjectParameter("commune", typeof(string));
    
            var rueParameter = rue != null ?
                new ObjectParameter("rue", rue) :
                new ObjectParameter("rue", typeof(string));
    
            var numeroParameter = numero != null ?
                new ObjectParameter("numero", numero) :
                new ObjectParameter("numero", typeof(string));
    
            var boiteParameter = boite != null ?
                new ObjectParameter("boite", boite) :
                new ObjectParameter("boite", typeof(string));
    
            var dateDeNaissanceParameter = dateDeNaissance.HasValue ?
                new ObjectParameter("dateDeNaissance", dateDeNaissance) :
                new ObjectParameter("dateDeNaissance", typeof(System.DateTime));
    
            var telephoneParameter = telephone != null ?
                new ObjectParameter("telephone", telephone) :
                new ObjectParameter("telephone", typeof(string));
    
            var photoParameter = photo != null ?
                new ObjectParameter("photo", photo) :
                new ObjectParameter("photo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("addNewMembre", mailParameter, nomParameter, prenomParameter, communeParameter, rueParameter, numeroParameter, boiteParameter, dateDeNaissanceParameter, telephoneParameter, photoParameter);
        }
    
        public virtual int CommunesToLower()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CommunesToLower");
        }
    
        public virtual ObjectResult<Nullable<int>> GetMyId(string mail)
        {
            var mailParameter = mail != null ?
                new ObjectParameter("mail", mail) :
                new ObjectParameter("mail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetMyId", mailParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetMyIds(string mail)
        {
            var mailParameter = mail != null ?
                new ObjectParameter("mail", mail) :
                new ObjectParameter("mail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetMyIds", mailParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetIdMmbre(string mail)
        {
            var mailParameter = mail != null ?
                new ObjectParameter("mail", mail) :
                new ObjectParameter("mail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetIdMmbre", mailParameter);
        }
    
        public virtual ObjectResult<GetMyProfile_Result> GetMyProfile(string mail)
        {
            var mailParameter = mail != null ?
                new ObjectParameter("mail", mail) :
                new ObjectParameter("mail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetMyProfile_Result>("GetMyProfile", mailParameter);
        }
    
        public virtual int GetAveragePrice(Nullable<int> annee, string type, string codePostCommune)
        {
            var anneeParameter = annee.HasValue ?
                new ObjectParameter("annee", annee) :
                new ObjectParameter("annee", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var codePostCommuneParameter = codePostCommune != null ?
                new ObjectParameter("codePostCommune", codePostCommune) :
                new ObjectParameter("codePostCommune", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetAveragePrice", anneeParameter, typeParameter, codePostCommuneParameter);
        }
    
        public virtual int GetAveragePrice2(Nullable<int> annee, string type, string codePostCommune)
        {
            var anneeParameter = annee.HasValue ?
                new ObjectParameter("annee", annee) :
                new ObjectParameter("annee", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var codePostCommuneParameter = codePostCommune != null ?
                new ObjectParameter("codePostCommune", codePostCommune) :
                new ObjectParameter("codePostCommune", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetAveragePrice2", anneeParameter, typeParameter, codePostCommuneParameter);
        }
    
        public virtual int GetAveragePrice3(Nullable<int> annee, string type, string codePostCommune)
        {
            var anneeParameter = annee.HasValue ?
                new ObjectParameter("annee", annee) :
                new ObjectParameter("annee", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var codePostCommuneParameter = codePostCommune != null ?
                new ObjectParameter("codePostCommune", codePostCommune) :
                new ObjectParameter("codePostCommune", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetAveragePrice3", anneeParameter, typeParameter, codePostCommuneParameter);
        }
    
        public virtual int GetAveragePrice5(Nullable<int> annee, string type, string codePostCommune)
        {
            var anneeParameter = annee.HasValue ?
                new ObjectParameter("annee", annee) :
                new ObjectParameter("annee", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var codePostCommuneParameter = codePostCommune != null ?
                new ObjectParameter("codePostCommune", codePostCommune) :
                new ObjectParameter("codePostCommune", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetAveragePrice5", anneeParameter, typeParameter, codePostCommuneParameter);
        }
    
        public virtual int ajoutIdCouleursDsMapsParams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ajoutIdCouleursDsMapsParams");
        }
    
        public virtual int insertCodePostInCsvTemplate()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("insertCodePostInCsvTemplate");
        }
    
        public virtual int insertCodePostInStatImmo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("insertCodePostInStatImmo");
        }
    
        public virtual int test(Nullable<int> annee, string type, string codePostCommune)
        {
            var anneeParameter = annee.HasValue ?
                new ObjectParameter("annee", annee) :
                new ObjectParameter("annee", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var codePostCommuneParameter = codePostCommune != null ?
                new ObjectParameter("codePostCommune", codePostCommune) :
                new ObjectParameter("codePostCommune", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("test", anneeParameter, typeParameter, codePostCommuneParameter);
        }
    
        public virtual int GetAveragePrices(Nullable<int> annee, string type, string codePostCommune)
        {
            var anneeParameter = annee.HasValue ?
                new ObjectParameter("annee", annee) :
                new ObjectParameter("annee", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var codePostCommuneParameter = codePostCommune != null ?
                new ObjectParameter("codePostCommune", codePostCommune) :
                new ObjectParameter("codePostCommune", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetAveragePrices", anneeParameter, typeParameter, codePostCommuneParameter);
        }
    
        public virtual int GetAveragePricess(Nullable<int> annee, string type, string codePostCommune)
        {
            var anneeParameter = annee.HasValue ?
                new ObjectParameter("annee", annee) :
                new ObjectParameter("annee", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var codePostCommuneParameter = codePostCommune != null ?
                new ObjectParameter("codePostCommune", codePostCommune) :
                new ObjectParameter("codePostCommune", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetAveragePricess", anneeParameter, typeParameter, codePostCommuneParameter);
        }
    
        [DbFunction("ImmoWhatEntities", "GetAveragePrice0")]
        public virtual IQueryable<GetAveragePrice0_Result> GetAveragePrice0(Nullable<int> annee, string type, string codePostCommune)
        {
            var anneeParameter = annee.HasValue ?
                new ObjectParameter("annee", annee) :
                new ObjectParameter("annee", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var codePostCommuneParameter = codePostCommune != null ?
                new ObjectParameter("codePostCommune", codePostCommune) :
                new ObjectParameter("codePostCommune", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetAveragePrice0_Result>("[ImmoWhatEntities].[GetAveragePrice0](@annee, @type, @codePostCommune)", anneeParameter, typeParameter, codePostCommuneParameter);
        }
    
        public virtual ObjectResult<GetAveragePrice00_Result> GetAveragePrice00(Nullable<int> annee, string type, string codePostCommune)
        {
            var anneeParameter = annee.HasValue ?
                new ObjectParameter("annee", annee) :
                new ObjectParameter("annee", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var codePostCommuneParameter = codePostCommune != null ?
                new ObjectParameter("codePostCommune", codePostCommune) :
                new ObjectParameter("codePostCommune", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAveragePrice00_Result>("GetAveragePrice00", anneeParameter, typeParameter, codePostCommuneParameter);
        }
    
        public virtual ObjectResult<GetAveragePrice1_Result> GetAveragePrice1(Nullable<int> annee, string type, string codePostCommune)
        {
            var anneeParameter = annee.HasValue ?
                new ObjectParameter("annee", annee) :
                new ObjectParameter("annee", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var codePostCommuneParameter = codePostCommune != null ?
                new ObjectParameter("codePostCommune", codePostCommune) :
                new ObjectParameter("codePostCommune", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAveragePrice1_Result>("GetAveragePrice1", anneeParameter, typeParameter, codePostCommuneParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetClassePrice(Nullable<int> annee, string type, string codePostCommune)
        {
            var anneeParameter = annee.HasValue ?
                new ObjectParameter("annee", annee) :
                new ObjectParameter("annee", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var codePostCommuneParameter = codePostCommune != null ?
                new ObjectParameter("codePostCommune", codePostCommune) :
                new ObjectParameter("codePostCommune", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetClassePrice", anneeParameter, typeParameter, codePostCommuneParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetClassePrices(Nullable<int> annee, string type, string codePostCommune)
        {
            var anneeParameter = annee.HasValue ?
                new ObjectParameter("annee", annee) :
                new ObjectParameter("annee", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var codePostCommuneParameter = codePostCommune != null ?
                new ObjectParameter("codePostCommune", codePostCommune) :
                new ObjectParameter("codePostCommune", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetClassePrices", anneeParameter, typeParameter, codePostCommuneParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetClassePrices0(Nullable<int> annee, string type, string codePostCommune)
        {
            var anneeParameter = annee.HasValue ?
                new ObjectParameter("annee", annee) :
                new ObjectParameter("annee", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var codePostCommuneParameter = codePostCommune != null ?
                new ObjectParameter("codePostCommune", codePostCommune) :
                new ObjectParameter("codePostCommune", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetClassePrices0", anneeParameter, typeParameter, codePostCommuneParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetClassePrices00(Nullable<int> annee, string type, string codePostCommune)
        {
            var anneeParameter = annee.HasValue ?
                new ObjectParameter("annee", annee) :
                new ObjectParameter("annee", typeof(int));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var codePostCommuneParameter = codePostCommune != null ?
                new ObjectParameter("codePostCommune", codePostCommune) :
                new ObjectParameter("codePostCommune", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetClassePrices00", anneeParameter, typeParameter, codePostCommuneParameter);
        }
    
        public virtual ObjectResult<GetInfoEvolutionPrices_Result> GetInfoEvolutionPrices(string codePostal)
        {
            var codePostalParameter = codePostal != null ?
                new ObjectParameter("codePostal", codePostal) :
                new ObjectParameter("codePostal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetInfoEvolutionPrices_Result>("GetInfoEvolutionPrices", codePostalParameter);
        }
    
        public virtual ObjectResult<GetTableGraphique_Result> GetTableGraphique(string codePostal)
        {
            var codePostalParameter = codePostal != null ?
                new ObjectParameter("codePostal", codePostal) :
                new ObjectParameter("codePostal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTableGraphique_Result>("GetTableGraphique", codePostalParameter);
        }
    
        public virtual ObjectResult<GetTableGraphique2_Result> GetTableGraphique2(string codePostal)
        {
            var codePostalParameter = codePostal != null ?
                new ObjectParameter("codePostal", codePostal) :
                new ObjectParameter("codePostal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTableGraphique2_Result>("GetTableGraphique2", codePostalParameter);
        }
    }
}
