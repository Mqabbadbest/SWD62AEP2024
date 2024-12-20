﻿using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataContext
{
    //The context class is an abstract representation of our database.
    public class AttendanceContext : IdentityDbContext<CustomUser>
    {
        public AttendanceContext(DbContextOptions<AttendanceContext> options) : base(options)
        {
        }

        //Objects MUST be in plural
        public DbSet<Log> Logs { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }
    }
}
