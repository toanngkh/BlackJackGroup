﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

public partial class BlackJackOnlineEntities : DbContext
{
    public BlackJackOnlineEntities()
        : base("name=BlackJackOnlineEntities")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }

    public virtual DbSet<Admin> Admins { get; set; }
    public virtual DbSet<CardInfo> CardInfoes { get; set; }
    public virtual DbSet<Hand> Hands { get; set; }
    public virtual DbSet<User> Users { get; set; }
}