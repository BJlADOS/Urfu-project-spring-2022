﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Workshop.Infrastructure.Storage;

namespace Workshop.Infrastructure.Migrations
{
    [DbContext(typeof(WorkshopDbContext))]
    [Migration("20210108090832_TeamReviewModelUpdate")]
    partial class TeamReviewModelUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:IdentityIncrement", 1)
                .HasAnnotation("SqlServer:IdentitySeed", 1)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Workshop.Core.Domain.Model.Auditorium.Auditorium", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<long>("EventId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDefaultAuditory")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Key");

                    b.ToTable("Auditoriums");
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.Competency.Competency", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompetencyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Key");

                    b.ToTable("Competencies");
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.Event.Event", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("TestDuration")
                        .HasColumnType("time");

                    b.HasKey("Key");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.KeyTechnology.KeyTechnology", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("EventId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Key");

                    b.ToTable("KeyTechnologies");
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.LifeScenario.LifeScenario", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("EventId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.HasKey("Key");

                    b.ToTable("LifeScenarios");
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.Project.Project", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contacts")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Curator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("EventId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPromoted")
                        .HasColumnType("bit");

                    b.Property<long>("KeyTechnologyId")
                        .HasColumnType("bigint");

                    b.Property<long>("LifeScenarioId")
                        .HasColumnType("bigint");

                    b.Property<int>("MaxTeamCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.Property<string>("Organization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Purpose")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeamCapacity")
                        .HasColumnType("int");

                    b.HasKey("Key");

                    b.HasIndex("KeyTechnologyId");

                    b.HasIndex("LifeScenarioId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.ProjectСompetency.ProjectCompetency", b =>
                {
                    b.Property<long>("CompetencyId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.HasKey("CompetencyId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectCompetencies");
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.Role.Role", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.HasKey("Key");

                    b.HasIndex("ProjectId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.Session.Session", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastConnection")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.Team.Team", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("AuditoriumId")
                        .HasColumnType("bigint");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("EventId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ExpertId")
                        .HasColumnType("bigint");

                    b.Property<int?>("Mark")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("TeamCompleteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TeamStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Key");

                    b.HasIndex("AuditoriumId");

                    b.HasIndex("ExpertId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.TeamCompetencyReview.TeamCompetencyReview", b =>
                {
                    b.Property<long>("ExpertId")
                        .HasColumnType("bigint");

                    b.Property<long>("TeamId")
                        .HasColumnType("bigint");

                    b.Property<long>("CompetencyId")
                        .HasColumnType("bigint");

                    b.Property<int>("Mark")
                        .HasColumnType("int");

                    b.HasKey("ExpertId", "TeamId", "CompetencyId");

                    b.HasIndex("CompetencyId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamCompetencyReviews");
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.TeamReview.TeamReview", b =>
                {
                    b.Property<long>("ExpertId")
                        .HasColumnType("bigint");

                    b.Property<long>("TeamId")
                        .HasColumnType("bigint");

                    b.Property<int?>("GoalsAndTasks")
                        .HasColumnType("int");

                    b.Property<int?>("Impact")
                        .HasColumnType("int");

                    b.Property<int?>("Knowledge")
                        .HasColumnType("int");

                    b.Property<int?>("Presentation")
                        .HasColumnType("int");

                    b.Property<int?>("Result")
                        .HasColumnType("int");

                    b.Property<int?>("Solution")
                        .HasColumnType("int");

                    b.Property<int?>("Technical")
                        .HasColumnType("int");

                    b.HasKey("ExpertId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamReviews");
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.User.User", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AcademicGroup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Auditorium")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("EventId")
                        .HasColumnType("bigint");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ProfileFilled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("TeamId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Key");

                    b.HasIndex("RoleId");

                    b.HasIndex("TeamId");

                    b.HasIndex("Login", "EventId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.UserCompetency.UserCompetency", b =>
                {
                    b.Property<long>("CompetencyId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserCompetencyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompetencyId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCompetencies");
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.Project.Project", b =>
                {
                    b.HasOne("Workshop.Core.Domain.Model.KeyTechnology.KeyTechnology", "KeyTechnology")
                        .WithMany("Projects")
                        .HasForeignKey("KeyTechnologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Workshop.Core.Domain.Model.LifeScenario.LifeScenario", "LifeScenario")
                        .WithMany("Projects")
                        .HasForeignKey("LifeScenarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.ProjectСompetency.ProjectCompetency", b =>
                {
                    b.HasOne("Workshop.Core.Domain.Model.Competency.Competency", "Competency")
                        .WithMany()
                        .HasForeignKey("CompetencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Workshop.Core.Domain.Model.Project.Project", "Project")
                        .WithMany("Competencies")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.Role.Role", b =>
                {
                    b.HasOne("Workshop.Core.Domain.Model.Project.Project", "Project")
                        .WithMany("Roles")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.Session.Session", b =>
                {
                    b.OwnsOne("Workshop.Core.Domain.Model.Session.SessionId", "Key", b1 =>
                        {
                            b1.Property<long>("SessionId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("SessionId");

                            b1.ToTable("Sessions");

                            b1.WithOwner()
                                .HasForeignKey("SessionId");
                        });
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.Team.Team", b =>
                {
                    b.HasOne("Workshop.Core.Domain.Model.Auditorium.Auditorium", "Auditorium")
                        .WithMany()
                        .HasForeignKey("AuditoriumId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Workshop.Core.Domain.Model.User.User", "Expert")
                        .WithMany()
                        .HasForeignKey("ExpertId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Workshop.Core.Domain.Model.Project.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.TeamCompetencyReview.TeamCompetencyReview", b =>
                {
                    b.HasOne("Workshop.Core.Domain.Model.Competency.Competency", "Competency")
                        .WithMany()
                        .HasForeignKey("CompetencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Workshop.Core.Domain.Model.User.User", "Expert")
                        .WithMany()
                        .HasForeignKey("ExpertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Workshop.Core.Domain.Model.Team.Team", "Team")
                        .WithMany("TeamCompetencyReviews")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.TeamReview.TeamReview", b =>
                {
                    b.HasOne("Workshop.Core.Domain.Model.User.User", "Expert")
                        .WithMany()
                        .HasForeignKey("ExpertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Workshop.Core.Domain.Model.Team.Team", "Team")
                        .WithMany("TeamReviews")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.User.User", b =>
                {
                    b.HasOne("Workshop.Core.Domain.Model.Role.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Workshop.Core.Domain.Model.Team.Team", "Team")
                        .WithMany("Users")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Workshop.Core.Domain.Model.UserCompetency.UserCompetency", b =>
                {
                    b.HasOne("Workshop.Core.Domain.Model.Competency.Competency", "Competency")
                        .WithMany()
                        .HasForeignKey("CompetencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Workshop.Core.Domain.Model.User.User", "User")
                        .WithMany("Competencies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
