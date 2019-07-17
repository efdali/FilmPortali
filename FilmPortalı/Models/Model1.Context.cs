﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FilmPortalı.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FilmPortaliEntities : DbContext
    {
        public FilmPortaliEntities()
            : base("name=FilmPortaliEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Crews> Crews { get; set; }
        public virtual DbSet<FilmCategory> FilmCategory { get; set; }
        public virtual DbSet<FilmCrew> FilmCrew { get; set; }
        public virtual DbSet<Films> Films { get; set; }
        public virtual DbSet<List> List { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<Sources> Sources { get; set; }
        public virtual DbSet<SubComments> SubComments { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Views> Views { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    
        public virtual int spFilmEkle(string ad, string konu, Nullable<int> yil, string ulke, Nullable<double> imdb, string poster, string trailer, string seo, string turkAd, Nullable<int> kategori)
        {
            var adParameter = ad != null ?
                new ObjectParameter("ad", ad) :
                new ObjectParameter("ad", typeof(string));
    
            var konuParameter = konu != null ?
                new ObjectParameter("konu", konu) :
                new ObjectParameter("konu", typeof(string));
    
            var yilParameter = yil.HasValue ?
                new ObjectParameter("yil", yil) :
                new ObjectParameter("yil", typeof(int));
    
            var ulkeParameter = ulke != null ?
                new ObjectParameter("ulke", ulke) :
                new ObjectParameter("ulke", typeof(string));
    
            var imdbParameter = imdb.HasValue ?
                new ObjectParameter("imdb", imdb) :
                new ObjectParameter("imdb", typeof(double));
    
            var posterParameter = poster != null ?
                new ObjectParameter("poster", poster) :
                new ObjectParameter("poster", typeof(string));
    
            var trailerParameter = trailer != null ?
                new ObjectParameter("trailer", trailer) :
                new ObjectParameter("trailer", typeof(string));
    
            var seoParameter = seo != null ?
                new ObjectParameter("seo", seo) :
                new ObjectParameter("seo", typeof(string));
    
            var turkAdParameter = turkAd != null ?
                new ObjectParameter("turkAd", turkAd) :
                new ObjectParameter("turkAd", typeof(string));
    
            var kategoriParameter = kategori.HasValue ?
                new ObjectParameter("kategori", kategori) :
                new ObjectParameter("kategori", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spFilmEkle", adParameter, konuParameter, yilParameter, ulkeParameter, imdbParameter, posterParameter, trailerParameter, seoParameter, turkAdParameter, kategoriParameter);
        }
    
        public virtual int spFilmSil(Nullable<int> filmId)
        {
            var filmIdParameter = filmId.HasValue ?
                new ObjectParameter("filmId", filmId) :
                new ObjectParameter("filmId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spFilmSil", filmIdParameter);
        }
    }
}
