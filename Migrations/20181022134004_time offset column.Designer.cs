﻿// <auto-generated />
using System;
using IsItBeerOclock.API.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IsItBeerOclock.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20181022134004_time offset column")]
    partial class timeoffsetcolumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-preview3-35497");

            modelBuilder.Entity("IsItBeerOclock.API.DataAccess.PushSubscription", b =>
                {
                    b.Property<string>("Endpoint")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan>("TimeOffset");

                    b.HasKey("Endpoint");

                    b.ToTable("PushSubscriptions");
                });

            modelBuilder.Entity("IsItBeerOclock.API.DataAccess.PushSubscriptionKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Endpoint");

                    b.Property<string>("KeyType");

                    b.Property<string>("KeyValue");

                    b.HasKey("Id");

                    b.HasIndex("Endpoint");

                    b.ToTable("PushSubscriptionKeys");
                });

            modelBuilder.Entity("IsItBeerOclock.API.DataAccess.PushSubscriptionKey", b =>
                {
                    b.HasOne("IsItBeerOclock.API.DataAccess.PushSubscription", "PushSubscription")
                        .WithMany("Keys")
                        .HasForeignKey("Endpoint");
                });
#pragma warning restore 612, 618
        }
    }
}
