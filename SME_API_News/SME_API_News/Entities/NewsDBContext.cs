using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SME_API_News.Entities;

public partial class NewsDBContext : DbContext
{
    public NewsDBContext()
    {
    }

    public NewsDBContext(DbContextOptions<NewsDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MApiInformation> MApiInformations { get; set; }

    public virtual DbSet<MBanner> MBanners { get; set; }

    public virtual DbSet<MCategory> MCategories { get; set; }

    public virtual DbSet<MDepartment> MDepartments { get; set; }

    public virtual DbSet<MLookUp> MLookUps { get; set; }

    public virtual DbSet<MMenu> MMenus { get; set; }

    public virtual DbSet<MNews> MNews { get; set; }

    public virtual DbSet<MPopup> MPopups { get; set; }

    public virtual DbSet<MRole> MRoles { get; set; }

    public virtual DbSet<MTag> MTags { get; set; }

    public virtual DbSet<TComment> TComments { get; set; }

    public virtual DbSet<TEmployeeRole> TEmployeeRoles { get; set; }

    public virtual DbSet<TNewsTag> TNewsTags { get; set; }

    public virtual DbSet<TRoleMenu> TRoleMenus { get; set; }

    public virtual DbSet<TRolePermission> TRolePermissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.9.155;Database=bluecarg_SME_News;User Id=sa;Password=Osmep@2025;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Thai_CI_AS");

        modelBuilder.Entity<MApiInformation>(entity =>
        {
            entity.ToTable("M_ApiInformation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey).HasMaxLength(150);
            entity.Property(e => e.AuthorizationType).HasMaxLength(50);
            entity.Property(e => e.Bearer).HasColumnType("ntext");
            entity.Property(e => e.ContentType).HasMaxLength(150);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MethodType).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(150);
            entity.Property(e => e.ServiceNameCode).HasMaxLength(250);
            entity.Property(e => e.ServiceNameTh).HasMaxLength(250);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Urldevelopment).HasColumnName("URLDevelopment");
            entity.Property(e => e.Urlproduction).HasColumnName("URLProduction");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<MBanner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_Banner__3214EC07F861291A");

            entity.ToTable("M_Banner", "SMENEWS");

            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasDefaultValue(true);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.LinkUrl).HasMaxLength(500);
            entity.Property(e => e.PublishDate).HasColumnType("datetime");
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(250);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MCategory>(entity =>
        {
            entity.ToTable("M_Categories", "SMENEWS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategorieCode).HasMaxLength(50);
            entity.Property(e => e.CategorieNameEn).HasMaxLength(50);
            entity.Property(e => e.CategorieNameTh)
                .HasMaxLength(50)
                .HasColumnName("CategorieNameTH");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MDepartment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Department");

            entity.ToTable("M_Department", "SMENEWS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DepartmentCode).HasMaxLength(50);
            entity.Property(e => e.DepartmentNameEn).HasMaxLength(50);
            entity.Property(e => e.DepartmentNameTh)
                .HasMaxLength(50)
                .HasColumnName("DepartmentNameTH");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MLookUp>(entity =>
        {
            entity.ToTable("M_LookUp", "SMENEWS");

            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasMaxLength(1);
            entity.Property(e => e.FlagDelete).HasMaxLength(1);
            entity.Property(e => e.LookupCode).HasMaxLength(200);
            entity.Property(e => e.LookupDescription).HasMaxLength(300);
            entity.Property(e => e.LookupType).HasMaxLength(100);
            entity.Property(e => e.LookupValue).HasMaxLength(200);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MMenu>(entity =>
        {
            entity.HasKey(e => e.MenuId);

            entity.ToTable("M_Menu");

            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FlagActive).HasMaxLength(1);
            entity.Property(e => e.FlagDelete).HasMaxLength(1);
            entity.Property(e => e.MenuName).HasMaxLength(50);
            entity.Property(e => e.MenuUrl)
                .HasMaxLength(50)
                .HasColumnName("MenuURL");
            entity.Property(e => e.Style).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MNews>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_M_Articles");

            entity.ToTable("M_News", "SMENEWS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArticlesAutherName).HasMaxLength(150);
            entity.Property(e => e.ArticlesShortDescription).HasMaxLength(255);
            entity.Property(e => e.ArticlesTitle).HasMaxLength(150);
            entity.Property(e => e.BusinessUnitId).HasMaxLength(50);
            entity.Property(e => e.CatagoryCode).HasMaxLength(50);
            entity.Property(e => e.CoverFilePath).HasMaxLength(255);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.PicNewsFilePath).HasMaxLength(255);
            entity.Property(e => e.PublishDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MPopup>(entity =>
        {
            entity.ToTable("M_Popup", "SMENEWS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(250);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MRole>(entity =>
        {
            entity.ToTable("M_Role", "SMENEWS");

            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FlagDelete).HasMaxLength(50);
            entity.Property(e => e.RoleCode).HasMaxLength(50);
            entity.Property(e => e.RoleName).HasMaxLength(150);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MTag>(entity =>
        {
            entity.ToTable("M_Tags", "SMENEWS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasMaxLength(50);
            entity.Property(e => e.TagName).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TComment>(entity =>
        {
            entity.ToTable("T_Comments", "SMENEWS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasMaxLength(50);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.NewsId).HasColumnName("news_id");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TEmployeeRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_T_Employee_Roles_1");

            entity.ToTable("T_Employee_Roles", "SMENEWS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BusinessUnitId).HasMaxLength(50);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.PositionId).HasMaxLength(50);
            entity.Property(e => e.RoleCode)
                .HasMaxLength(50)
                .HasColumnName("Role_code");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TNewsTag>(entity =>
        {
            entity.ToTable("T_News_Tags", "SMENEWS");

            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TRoleMenu>(entity =>
        {
            entity.ToTable("T_RoleMenu", "SMENEWS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.RoleCode).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TRolePermission>(entity =>
        {
            entity.ToTable("T_RolePermission", "SMENEWS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MenuId).HasColumnName("MenuID");
            entity.Property(e => e.PermissionCode).HasMaxLength(50);
            entity.Property(e => e.RoleCode).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
