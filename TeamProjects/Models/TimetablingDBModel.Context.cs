﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TeamProjects.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class team04Entities : DbContext
    {
        public team04Entities()
            : base("name=team04Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<timetable_building> timetable_building { get; set; }
        public DbSet<timetable_day> timetable_day { get; set; }
        public DbSet<timetable_department> timetable_department { get; set; }
        public DbSet<timetable_facility> timetable_facility { get; set; }
        public DbSet<timetable_module> timetable_module { get; set; }
        public DbSet<timetable_park> timetable_park { get; set; }
        public DbSet<timetable_request> timetable_request { get; set; }
        public DbSet<timetable_request_facility> timetable_request_facility { get; set; }
        public DbSet<timetable_request_room_allocation> timetable_request_room_allocation { get; set; }
        public DbSet<timetable_request_week> timetable_request_week { get; set; }
        public DbSet<timetable_room> timetable_room { get; set; }
        public DbSet<timetable_room_facility> timetable_room_facility { get; set; }
        public DbSet<timetable_room_type> timetable_room_type { get; set; }
        public DbSet<timetable_round> timetable_round { get; set; }
    }
}
