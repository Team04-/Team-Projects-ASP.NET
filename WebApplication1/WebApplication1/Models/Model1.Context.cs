﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TestDBEntities : DbContext
    {
        public TestDBEntities()
            : base("name=TestDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<timetable_room> timetable_room { get; set; }
        public virtual DbSet<timetable_module> timetable_module { get; set; }
        public virtual DbSet<timetable_building> timetable_building { get; set; }
        public virtual DbSet<timetable_facility> timetable_facility { get; set; }
    }
}
