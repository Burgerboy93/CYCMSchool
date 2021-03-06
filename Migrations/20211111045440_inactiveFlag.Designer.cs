// <auto-generated />
using System;
using CYCMSchool.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CYCMSchool.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20211111045440_inactiveFlag")]
    partial class inactiveFlag
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CYCMSchool.Models.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BSB")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bank");
                });

            modelBuilder.Entity("CYCMSchool.Models.Duration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("DurationTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Duration");
                });

            modelBuilder.Entity("CYCMSchool.Models.EmailSignature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Signature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmailSignature");
                });

            modelBuilder.Entity("CYCMSchool.Models.Instrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InstrumentFamily")
                        .HasColumnType("int");

                    b.Property<string>("InstrumentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Instrument");
                });

            modelBuilder.Entity("CYCMSchool.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DurationID")
                        .HasColumnType("int");

                    b.Property<int>("InstrumentID")
                        .HasColumnType("int");

                    b.Property<DateTime>("LessonDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<int>("TermID")
                        .HasColumnType("int");

                    b.Property<int>("TutorID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DurationID");

                    b.HasIndex("InstrumentID");

                    b.HasIndex("StudentID");

                    b.HasIndex("TermID");

                    b.HasIndex("TutorID");

                    b.ToTable("Lesson");
                });

            modelBuilder.Entity("CYCMSchool.Models.Letter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BankID")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getDate()");

                    b.Property<int>("EmailSignatureId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BankID");

                    b.HasIndex("EmailSignatureId");

                    b.HasIndex("StudentId");

                    b.ToTable("Letter");
                });

            modelBuilder.Entity("CYCMSchool.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("GuardianName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Inactive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("StudentGender")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("CYCMSchool.Models.Term", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("TermEnd")
                        .HasColumnType("datetime2");

                    b.Property<int>("TermNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("TermStart")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Term");
                });

            modelBuilder.Entity("CYCMSchool.Models.Tutor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Tutor");
                });

            modelBuilder.Entity("LessonLetter", b =>
                {
                    b.Property<int>("LessonsId")
                        .HasColumnType("int");

                    b.Property<int>("LettersId")
                        .HasColumnType("int");

                    b.HasKey("LessonsId", "LettersId");

                    b.HasIndex("LettersId");

                    b.ToTable("LessonLetter");
                });

            modelBuilder.Entity("CYCMSchool.Models.Lesson", b =>
                {
                    b.HasOne("CYCMSchool.Models.Duration", "Duration")
                        .WithMany("Lessons")
                        .HasForeignKey("DurationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CYCMSchool.Models.Instrument", "Instrument")
                        .WithMany("Lessons")
                        .HasForeignKey("InstrumentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CYCMSchool.Models.Student", "Student")
                        .WithMany("Lessons")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CYCMSchool.Models.Term", "Term")
                        .WithMany("Lessons")
                        .HasForeignKey("TermID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CYCMSchool.Models.Tutor", "Tutor")
                        .WithMany("Lessons")
                        .HasForeignKey("TutorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Duration");

                    b.Navigation("Instrument");

                    b.Navigation("Student");

                    b.Navigation("Term");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("CYCMSchool.Models.Letter", b =>
                {
                    b.HasOne("CYCMSchool.Models.Bank", "Bank")
                        .WithMany("Letters")
                        .HasForeignKey("BankID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CYCMSchool.Models.EmailSignature", "EmailSignature")
                        .WithMany("Letters")
                        .HasForeignKey("EmailSignatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CYCMSchool.Models.Student", "Student")
                        .WithMany("Letters")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Bank");

                    b.Navigation("EmailSignature");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LessonLetter", b =>
                {
                    b.HasOne("CYCMSchool.Models.Lesson", null)
                        .WithMany()
                        .HasForeignKey("LessonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CYCMSchool.Models.Letter", null)
                        .WithMany()
                        .HasForeignKey("LettersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CYCMSchool.Models.Bank", b =>
                {
                    b.Navigation("Letters");
                });

            modelBuilder.Entity("CYCMSchool.Models.Duration", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("CYCMSchool.Models.EmailSignature", b =>
                {
                    b.Navigation("Letters");
                });

            modelBuilder.Entity("CYCMSchool.Models.Instrument", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("CYCMSchool.Models.Student", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("Letters");
                });

            modelBuilder.Entity("CYCMSchool.Models.Term", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("CYCMSchool.Models.Tutor", b =>
                {
                    b.Navigation("Lessons");
                });
#pragma warning restore 612, 618
        }
    }
}
