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
        public virtual DbSet<timetable_department> timetable_department { get; set; }
        public virtual DbSet<timetable_day> timetable_day { get; set; }
        public virtual DbSet<timetable_park> timetable_park { get; set; }
        public virtual DbSet<timetable_request> timetable_request { get; set; }
        public virtual DbSet<timetable_request_facility> timetable_request_facility { get; set; }
        public virtual DbSet<timetable_request_room_allocation> timetable_request_room_allocation { get; set; }
        public virtual DbSet<timetable_request_week> timetable_request_week { get; set; }
        public virtual DbSet<timetable_room_facility> timetable_room_facility { get; set; }
        public virtual DbSet<timetable_room_type> timetable_room_type { get; set; }
        public virtual DbSet<timetable_round> timetable_round { get; set; }
    }
}
