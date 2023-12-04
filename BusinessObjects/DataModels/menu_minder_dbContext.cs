using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessObjects.DataAccess
{
    public partial class menu_minder_dbContext : DbContext
    {
        public menu_minder_dbContext()
        {
        }

        public menu_minder_dbContext(DbContextOptions<menu_minder_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<DiningTable> DiningTables { get; set; } = null!;
        public virtual DbSet<FeedBack> FeedBacks { get; set; } = null!;
        public virtual DbSet<Food> Foods { get; set; } = null!;
        public virtual DbSet<FoodOrder> FoodOrders { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Permit> Permits { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Serving> Servings { get; set; } = null!;
        public virtual DbSet<TableUsed> TableUseds { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Server=localhost;Port=5432;User ID=postgres;Password=1234;Database=menu_minder_db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum("tp_role", new[] { "STAFF", "ADMIN" })
                .HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.HasIndex(e => e.Email, "account_email_key")
                    .IsUnique();

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Avatar)
                    .HasColumnName("avatar")
                    .HasDefaultValueSql("'https://res.cloudinary.com/dwskvqnkc/image/upload/v1698909675/menu_minder_store/img_default_elml1l.jpg'::text");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IsBlock)
                    .HasColumnName("is_block")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("phone_number");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasKey(e => new { e.ServingId, e.CreatedBy })
                    .HasName("bill_pkey");

                entity.ToTable("bill");

                entity.Property(e => e.ServingId).HasColumnName("serving_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.TotalPrice).HasColumnName("total_price");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("fk_bill_account");

                entity.HasOne(d => d.Serving)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.ServingId)
                    .HasConstraintName("fk_bill_serving");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(40)
                    .HasColumnName("category_name");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<DiningTable>(entity =>
            {
                entity.HasKey(e => e.TableId)
                    .HasName("dining_table_pkey");

                entity.ToTable("dining_table");

                entity.HasIndex(e => e.TableNumber, "dining_table_table_number_key")
                    .IsUnique();

                entity.Property(e => e.TableId).HasColumnName("table_id");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'AVAILABLE'::character varying");

                entity.Property(e => e.TableNumber)
                    .HasMaxLength(10)
                    .HasColumnName("table_number");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.DiningTables)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("fk_table_account");
            });

            modelBuilder.Entity<FeedBack>(entity =>
            {
                entity.ToTable("feed_back");

                entity.Property(e => e.FeedBackId).HasColumnName("feed_back_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Message).HasColumnName("message");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.ServingId).HasColumnName("serving_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Serving)
                    .WithMany(p => p.FeedBacks)
                    .HasForeignKey(d => d.ServingId)
                    .HasConstraintName("fk_feedback_serving");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("food");

                entity.HasIndex(e => e.Name, "food_name_key")
                    .IsUnique();

                entity.Property(e => e.FoodId).HasColumnName("food_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Recipe).HasColumnName("recipe");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'PENDING'::character varying");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("fk_food_category");
            });

            modelBuilder.Entity<FoodOrder>(entity =>
            {
                entity.ToTable("food_order");

                entity.Property(e => e.FoodOrderId).HasColumnName("food_order_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FoodId).HasColumnName("food_id");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.ServingId).HasColumnName("serving_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'PENDING'::character varying");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FoodOrders)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("fk_foodorder_food");

                entity.HasOne(d => d.Serving)
                    .WithMany(p => p.FoodOrders)
                    .HasForeignKey(d => d.ServingId)
                    .HasConstraintName("fk_foodorder_serving");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permission");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.Property(e => e.PermissionName)
                    .HasMaxLength(100)
                    .HasColumnName("permission_name");
            });

            modelBuilder.Entity<Permit>(entity =>
            {
                entity.HasKey(e => new { e.PermissionId, e.AccountId })
                    .HasName("permit_pkey");

                entity.ToTable("permit");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Permits)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("fk_permit_account");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.Permits)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("fk_permit_permission");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("reservation");

                entity.Property(e => e.ReservationId).HasColumnName("reservation_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(40)
                    .HasColumnName("customer_name");

                entity.Property(e => e.CustomerPhone)
                    .HasMaxLength(12)
                    .HasColumnName("customer_phone");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.NumberOfCustomer)
                    .HasColumnName("number_of_customer")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.ReservationTime)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("reservation_time");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'PENDING'::character varying");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("fk_reservation_account");
            });

            modelBuilder.Entity<Serving>(entity =>
            {
                entity.ToTable("serving");

                entity.Property(e => e.ServingId).HasColumnName("serving_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.NumberOfCutomer).HasColumnName("number_of_cutomer");

                entity.Property(e => e.TimeIn)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("time_in");

                entity.Property(e => e.TimeOut)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("time_out");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Servings)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("fk_serving_account");
            });

            modelBuilder.Entity<TableUsed>(entity =>
            {
                entity.HasKey(e => new { e.TableId, e.ServingId })
                    .HasName("table_used_pkey");

                entity.ToTable("table_used");

                entity.Property(e => e.TableId).HasColumnName("table_id");

                entity.Property(e => e.ServingId).HasColumnName("serving_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Serving)
                    .WithMany(p => p.TableUseds)
                    .HasForeignKey(d => d.ServingId)
                    .HasConstraintName("fk_tableused_serving");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.TableUseds)
                    .HasForeignKey(d => d.TableId)
                    .HasConstraintName("fk_tableused_table");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
