﻿// <auto-generated />
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ultiminer_Database.Migrations
{
    [DbContext(typeof(UltiminerContext))]
    partial class UltiminerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Database.Models.User", b =>
                {
                    b.Property<string>("DiscordId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DiscordId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
