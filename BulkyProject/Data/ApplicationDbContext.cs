﻿using BulkyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyProject.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        
        
            
        }
        public DbSet<Category> Categories { get; set; } //creating table in database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            //insert data in database category table
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Sci-fi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Romantic", DisplayOrder = 3 }
                );
        }
    }
}