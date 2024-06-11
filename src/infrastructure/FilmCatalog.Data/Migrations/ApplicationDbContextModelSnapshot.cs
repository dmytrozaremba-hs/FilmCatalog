﻿// <auto-generated />
using System;
using FilmCatalog.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FilmCatalog.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ActorFilms", b =>
                {
                    b.Property<int>("ActedInFilmsId")
                        .HasColumnType("integer");

                    b.Property<int>("ActorsId")
                        .HasColumnType("integer");

                    b.HasKey("ActedInFilmsId", "ActorsId");

                    b.HasIndex("ActorsId");

                    b.ToTable("ActorFilms");
                });

            modelBuilder.Entity("DirectorFilms", b =>
                {
                    b.Property<int>("DirectedFilmsId")
                        .HasColumnType("integer");

                    b.Property<int>("DirectorsId")
                        .HasColumnType("integer");

                    b.HasKey("DirectedFilmsId", "DirectorsId");

                    b.HasIndex("DirectorsId");

                    b.ToTable("DirectorFilms");
                });

            modelBuilder.Entity("FilmCatalog.Domain.Entities.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("AverageRating")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("DurationInMinutes")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("NumberOfVotes")
                        .HasColumnType("integer");

                    b.Property<string>("PosterUrl")
                        .HasColumnType("text");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("FilmCatalog.Domain.Entities.FilmList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FilmLists");
                });

            modelBuilder.Entity("FilmCatalog.Domain.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("FilmCatalog.Domain.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsActor")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDirector")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsProducer")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("FilmCatalog.Domain.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("FilmId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FilmId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("FilmCatalog.Domain.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("FilmCatalog.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<int>("WatchLaterId")
                        .HasColumnType("integer");

                    b.Property<int>("WatchedId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WatchLaterId")
                        .IsUnique();

                    b.HasIndex("WatchedId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FilmFilmList", b =>
                {
                    b.Property<int>("FilmListsId")
                        .HasColumnType("integer");

                    b.Property<int>("FilmsId")
                        .HasColumnType("integer");

                    b.HasKey("FilmListsId", "FilmsId");

                    b.HasIndex("FilmsId");

                    b.ToTable("FilmFilmList");
                });

            modelBuilder.Entity("FilmGenre", b =>
                {
                    b.Property<int>("FilmsId")
                        .HasColumnType("integer");

                    b.Property<int>("GenresId")
                        .HasColumnType("integer");

                    b.HasKey("FilmsId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("FilmGenre");
                });

            modelBuilder.Entity("FilmTag", b =>
                {
                    b.Property<int>("FilmsId")
                        .HasColumnType("integer");

                    b.Property<int>("TagsId")
                        .HasColumnType("integer");

                    b.HasKey("FilmsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("FilmTag");
                });

            modelBuilder.Entity("ProducerFilms", b =>
                {
                    b.Property<int>("ProducedFilmsId")
                        .HasColumnType("integer");

                    b.Property<int>("ProducersId")
                        .HasColumnType("integer");

                    b.HasKey("ProducedFilmsId", "ProducersId");

                    b.HasIndex("ProducersId");

                    b.ToTable("ProducerFilms");
                });

            modelBuilder.Entity("ActorFilms", b =>
                {
                    b.HasOne("FilmCatalog.Domain.Entities.Film", null)
                        .WithMany()
                        .HasForeignKey("ActedInFilmsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCatalog.Domain.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("ActorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DirectorFilms", b =>
                {
                    b.HasOne("FilmCatalog.Domain.Entities.Film", null)
                        .WithMany()
                        .HasForeignKey("DirectedFilmsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCatalog.Domain.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("DirectorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FilmCatalog.Domain.Entities.Review", b =>
                {
                    b.HasOne("FilmCatalog.Domain.Entities.Film", "Film")
                        .WithMany("Reviews")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCatalog.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FilmCatalog.Domain.Entities.User", b =>
                {
                    b.HasOne("FilmCatalog.Domain.Entities.FilmList", "WatchLater")
                        .WithOne()
                        .HasForeignKey("FilmCatalog.Domain.Entities.User", "WatchLaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCatalog.Domain.Entities.FilmList", "Watched")
                        .WithOne()
                        .HasForeignKey("FilmCatalog.Domain.Entities.User", "WatchedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WatchLater");

                    b.Navigation("Watched");
                });

            modelBuilder.Entity("FilmFilmList", b =>
                {
                    b.HasOne("FilmCatalog.Domain.Entities.FilmList", null)
                        .WithMany()
                        .HasForeignKey("FilmListsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCatalog.Domain.Entities.Film", null)
                        .WithMany()
                        .HasForeignKey("FilmsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FilmGenre", b =>
                {
                    b.HasOne("FilmCatalog.Domain.Entities.Film", null)
                        .WithMany()
                        .HasForeignKey("FilmsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCatalog.Domain.Entities.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FilmTag", b =>
                {
                    b.HasOne("FilmCatalog.Domain.Entities.Film", null)
                        .WithMany()
                        .HasForeignKey("FilmsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCatalog.Domain.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProducerFilms", b =>
                {
                    b.HasOne("FilmCatalog.Domain.Entities.Film", null)
                        .WithMany()
                        .HasForeignKey("ProducedFilmsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmCatalog.Domain.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("ProducersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FilmCatalog.Domain.Entities.Film", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
