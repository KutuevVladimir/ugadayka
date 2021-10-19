﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiExample.Models;

namespace WebApiExample.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("WebApiExample.Models.DataModels+Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(4000)");

                    b.Property<string>("LoginName")
                        .HasColumnType("text");

                    b.Property<byte[]>("Password")
                        .HasColumnType("varbinary(4000)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("varbinary(4000)");

                    b.HasKey("PlayerId");

                    b.HasIndex("RoomId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("WebApiExample.Models.DataModels+Playlist", b =>
                {
                    b.Property<int>("PlaylistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(4000)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("PlaylistId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("WebApiExample.Models.DataModels+PlaylistsToLikes", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("PlaylistId")
                        .HasColumnType("int");

                    b.HasKey("PlayerId", "PlaylistId");

                    b.HasIndex("PlaylistId");

                    b.ToTable("PlaylistsToLikes");
                });

            modelBuilder.Entity("WebApiExample.Models.DataModels+PlaylistsToTracks", b =>
                {
                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.Property<int>("PlaylistId")
                        .HasColumnType("int");

                    b.HasKey("TrackId", "PlaylistId");

                    b.HasIndex("PlaylistId");

                    b.ToTable("PlaylistsToTracks");
                });

            modelBuilder.Entity("WebApiExample.Models.DataModels+Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(4000)");

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int?>("PlaylistId")
                        .HasColumnType("int");

                    b.Property<bool>("RequiresPassword")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("State")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("RoomId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("PlaylistId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("WebApiExample.Models.DataModels+Track", b =>
                {
                    b.Property<int>("TrackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Author")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("TrackId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("WebApiExample.Models.DataModels+Player", b =>
                {
                    b.HasOne("WebApiExample.Models.DataModels+Room", null)
                        .WithMany("Players")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("WebApiExample.Models.DataModels+Playlist", b =>
                {
                    b.HasOne("WebApiExample.Models.DataModels+Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("WebApiExample.Models.DataModels+PlaylistsToLikes", b =>
                {
                    b.HasOne("WebApiExample.Models.DataModels+Player", "Player")
                        .WithMany("PlaylistsToLikes")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiExample.Models.DataModels+Playlist", "Playlist")
                        .WithMany("PlaylistsToLikes")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Playlist");
                });

            modelBuilder.Entity("WebApiExample.Models.DataModels+PlaylistsToTracks", b =>
                {
                    b.HasOne("WebApiExample.Models.DataModels+Playlist", "Playlist")
                        .WithMany("PlaylistsToTracks")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiExample.Models.DataModels+Track", "Track")
                        .WithMany("PlaylistsToTracks")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("WebApiExample.Models.DataModels+Room", b =>
                {
                    b.HasOne("WebApiExample.Models.DataModels+Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.HasOne("WebApiExample.Models.DataModels+Playlist", "Playlist")
                        .WithMany()
                        .HasForeignKey("PlaylistId");

                    b.Navigation("Player");

                    b.Navigation("Playlist");
                });

            modelBuilder.Entity("WebApiExample.Models.DataModels+Player", b =>
                {
                    b.Navigation("PlaylistsToLikes");
                });

            modelBuilder.Entity("WebApiExample.Models.DataModels+Playlist", b =>
                {
                    b.Navigation("PlaylistsToLikes");

                    b.Navigation("PlaylistsToTracks");
                });

            modelBuilder.Entity("WebApiExample.Models.DataModels+Room", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("WebApiExample.Models.DataModels+Track", b =>
                {
                    b.Navigation("PlaylistsToTracks");
                });
#pragma warning restore 612, 618
        }
    }
}
