﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarEvents.Models;

public partial class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarEvent> CarEvents { get; set; }

    public virtual DbSet<CarPhoto> CarPhotos { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventPhoto> EventPhotos { get; set; }

    public virtual DbSet<FeatureFlag> FeatureFlags { get; set; }

    public virtual DbSet<FeatureUser> FeatureUsers { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cars_pkey");

            entity.ToTable("cars");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(20)
                .HasColumnName("license_plate");
            entity.Property(e => e.Make)
                .HasMaxLength(50)
                .HasColumnName("make");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("model");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<CarEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("car_events_pkey");

            entity.ToTable("car_events");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CarId).HasColumnName("car_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");

            entity.HasOne(d => d.Car).WithMany(p => p.CarEvents)
                .HasForeignKey(d => d.CarId)
                .HasConstraintName("car_events_car_id_fkey");

            entity.HasOne(d => d.Event).WithMany(p => p.CarEvents)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("car_events_event_id_fkey");
        });

        modelBuilder.Entity<CarPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("car_photos_pkey");

            entity.ToTable("car_photos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CarId).HasColumnName("car_id");
            entity.Property(e => e.PhotoUrl).HasColumnName("photo_url");

            entity.HasOne(d => d.Car).WithMany(p => p.CarPhotos)
                .HasForeignKey(d => d.CarId)
                .HasConstraintName("car_photos_car_id_fkey");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("events_pkey");

            entity.ToTable("events");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_date");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.OrganizerId).HasColumnName("organizer_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_date");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Events)
                .HasForeignKey(d => d.OrganizerId)
                .HasConstraintName("events_organizer_id_fkey");
        });

        modelBuilder.Entity<EventPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("event_photos_pkey");

            entity.ToTable("event_photos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.PhotoUrl).HasColumnName("photo_url");

            entity.HasOne(d => d.Event).WithMany(p => p.EventPhotos)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("event_photos_event_id_fkey");
        });

        modelBuilder.Entity<FeatureFlag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("feature_flags_pkey");

            entity.ToTable("feature_flags");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FeatureName)
                .HasMaxLength(255)
                .HasColumnName("feature_name");
        });

        modelBuilder.Entity<FeatureUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("feature_users_pkey");

            entity.ToTable("feature_users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FeatureId).HasColumnName("feature_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Feature).WithMany(p => p.FeatureUsers)
                .HasForeignKey(d => d.FeatureId)
                .HasConstraintName("feature_users_feature_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.FeatureUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("feature_users_user_id_fkey");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("registrations_pkey");

            entity.ToTable("registrations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.RegistrationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("registration_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Event).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("registrations_event_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("registrations_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
