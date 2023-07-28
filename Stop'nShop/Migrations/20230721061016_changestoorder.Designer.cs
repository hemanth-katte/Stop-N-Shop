﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stop_nShop.Data;

#nullable disable

namespace Stop_nShop.Migrations
{
    [DbContext(typeof(StopAndShopDBContext))]
    [Migration("20230721061016_changestoorder")]
    partial class changestoorder
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Stop_nShop.Models.Interested", b =>
                {
                    b.Property<int>("interestedID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("interestedID"));

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.Property<int>("sellerId")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("interestedID");

                    b.HasIndex("productId");

                    b.ToTable("Interested");
                });

            modelBuilder.Entity("Stop_nShop.Models.Orders", b =>
                {
                    b.Property<int>("orderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("orderId"));

                    b.Property<DateTime>("orderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("orderStatus")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<int>("sellerId")
                        .HasColumnType("int");

                    b.Property<int>("totalPrice")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("orderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Stop_nShop.Models.Product", b =>
                {
                    b.Property<int>("productId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("productId"));

                    b.Property<int?>("OrdersorderId")
                        .HasColumnType("int");

                    b.Property<string>("brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<int>("sellerId")
                        .HasColumnType("int");

                    b.Property<int>("size")
                        .HasColumnType("int");

                    b.HasKey("productId");

                    b.HasIndex("OrdersorderId");

                    b.HasIndex("sellerId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Stop_nShop.Models.Seller", b =>
                {
                    b.Property<int>("sellerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("sellerId"));

                    b.Property<string>("sellerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sellerMailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sellerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sellerPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sellerPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("sellerRatings")
                        .HasColumnType("int");

                    b.HasKey("sellerId");

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("Stop_nShop.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userId"));

                    b.Property<int>("cartId")
                        .HasColumnType("int");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userStatus")
                        .HasColumnType("int");

                    b.Property<int>("wishListId")
                        .HasColumnType("int");

                    b.HasKey("userId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Stop_nShop.Models.Interested", b =>
                {
                    b.HasOne("Stop_nShop.Models.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("Stop_nShop.Models.Product", b =>
                {
                    b.HasOne("Stop_nShop.Models.Orders", null)
                        .WithMany("products")
                        .HasForeignKey("OrdersorderId");

                    b.HasOne("Stop_nShop.Models.Seller", "seller")
                        .WithMany()
                        .HasForeignKey("sellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("seller");
                });

            modelBuilder.Entity("Stop_nShop.Models.Orders", b =>
                {
                    b.Navigation("products");
                });
#pragma warning restore 612, 618
        }
    }
}
