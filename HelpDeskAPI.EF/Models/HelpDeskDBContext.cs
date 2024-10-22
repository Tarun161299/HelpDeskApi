using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class HelpDeskDBContext : DbContext
    {
        public HelpDeskDBContext()
        {
        }

        public HelpDeskDBContext(DbContextOptions<HelpDeskDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppCaptcha> AppCaptcha { get; set; } = null!;
        public virtual DbSet<AppDocumentUploadedDetail> AppDocumentUploadedDetail { get; set; } = null!;
        public virtual DbSet<AppLoginDetails> AppLoginDetails { get; set; } = null!;
        public virtual DbSet<AppRemarks> AppRemarks { get; set; } = null!;
        public virtual DbSet<AppRoleModulePermission> AppRoleModulePermission { get; set; } = null!;
        public virtual DbSet<AppServiceRequest> AppServiceRequest { get; set; } = null!;
        public virtual DbSet<AppServiceRequestHistory> AppServiceRequestHistory { get; set; } = null!;
        public virtual DbSet<AppTicket> AppTicket { get; set; } = null!;
        public virtual DbSet<AppTicketHistory> AppTicketHistory { get; set; } = null!;
        public virtual DbSet<BakMdUserBoardRoleMapping> BakMdUserBoardRoleMapping { get; set; } = null!;
        public virtual DbSet<Esodata> Esodata { get; set; } = null!;
        public virtual DbSet<Log> Log { get; set; } = null!;
        public virtual DbSet<LogActivityAuditTrail> LogActivityAuditTrail { get; set; } = null!;
        public virtual DbSet<MdActionType> MdActionType { get; set; } = null!;
        public virtual DbSet<MdAgency> MdAgency { get; set; } = null!;
        public virtual DbSet<MdBoard> MdBoard { get; set; } = null!;
        public virtual DbSet<MdDocumentType> MdDocumentType { get; set; } = null!;
        public virtual DbSet<MdModule> MdModule { get; set; } = null!;
        public virtual DbSet<MdPriority> MdPriority { get; set; } = null!;
        public virtual DbSet<MdRole> MdRole { get; set; } = null!;
        public virtual DbSet<MdSection> MdSection { get; set; } = null!;
        public virtual DbSet<MdSmsEmailTemplate> MdSmsEmailTemplate { get; set; } = null!;
        public virtual DbSet<MdStatus> MdStatus { get; set; } = null!;
        public virtual DbSet<MdUserBoardMapping> MdUserBoardMapping { get; set; } = null!;
        public virtual DbSet<MdUserBoardRoleMapping> MdUserBoardRoleMapping { get; set; } = null!;
        public virtual DbSet<TblRnd> TblRnd { get; set; } = null!;
        public virtual DbSet<UserAuthorization> UserAuthorization { get; set; } = null!;
        public virtual DbSet<WorkOrderDetails> WorkOrderDetails { get; set; } = null!;
        public virtual DbSet<YourTable> YourTable { get; set; } = null!;
        public virtual DbSet<ZmstProjects> ZmstProjects { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppCaptcha>(entity =>
            {
                entity.ToTable("App_Captcha");

                entity.Property(e => e.CaptchBaseString).HasColumnName("Captch_BaseString");

                entity.Property(e => e.CaptchaKey)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Captcha_Key");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Md5Hash)
                    .IsUnicode(false)
                    .HasColumnName("Md5_Hash");
            });

            modelBuilder.Entity<AppDocumentUploadedDetail>(entity =>
            {
                entity.HasKey(e => e.DocumentId);

                entity.ToTable("App_DocumentUploadedDetail");

                entity.Property(e => e.DocumentId)
                    .HasColumnName("documentId")
                    .HasComment("required;number");

                entity.Property(e => e.Activityid)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("activityid")
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("createdBy");

                entity.Property(e => e.CycleId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cycleId")
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.DocContent)
                    .IsUnicode(false)
                    .HasColumnName("docContent")
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.DocId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("docId")
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.DocNatureId)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("docNatureId")
                    .IsFixedLength()
                    .HasComment("required;alphabet");

                entity.Property(e => e.DocSubject)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("docSubject")
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.DocType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("docType")
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ipAddress");

                entity.Property(e => e.ObjectId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("objectId")
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.ObjectUrl)
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("objectUrl")
                    .HasComment("required;url");

                entity.Property(e => e.SubTime)
                    .HasColumnType("datetime")
                    .HasColumnName("subTime");
            });

            modelBuilder.Entity<AppLoginDetails>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("App_LoginDetails");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("userId")
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.EisUserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("eisUserId")
                    .HasComment("required;email");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .HasComment("required;email");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("isActive")
                    .IsFixedLength()
                    .HasComment("required;alphabet");

                entity.Property(e => e.LastLoginIp)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("lastLoginIP")
                    .HasComment("required");

                entity.Property(e => e.LastLoginTime)
                    .HasColumnType("datetime")
                    .HasColumnName("lastLoginTime")
                    .HasComment("required");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mobile")
                    .HasComment("required;number;maxlength;minlength");

                entity.Property(e => e.MobileIsd)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("mobileIsd");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("userName")
                    .HasComment("required;alphanumeric");
            });

            modelBuilder.Entity<AppRemarks>(entity =>
            {
                entity.ToTable("App_Remarks");

                entity.Property(e => e.Id).HasComment("required;number");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Module)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("required;number");

                entity.Property(e => e.ModuleId).HasComment("required;number");

                entity.Property(e => e.Remarks).HasComment("required;alphanumeric");
            });

            modelBuilder.Entity<AppRoleModulePermission>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("App_RoleModulePermission");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("required");

                entity.Property(e => e.ModuleId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.RoleId).HasComment("required;number");
            });

            modelBuilder.Entity<AppServiceRequest>(entity =>
            {
                entity.HasKey(e => e.ServiceRequestId);

                entity.ToTable("App_ServiceRequest");

                entity.Property(e => e.BoardId).HasComment("required;alphanumeric");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedIp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Priority)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("required");

                entity.Property(e => e.RequestCategoryIds)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("required;number");

                entity.Property(e => e.ResolutionDate)
                    .HasColumnType("datetime")
                    .HasComment("required");

                entity.Property(e => e.ServiceRequestNo)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasComputedColumnSql("([dbo].[fn_SRandTKT]('SRN',[ServiceRequestId]))", false);

                entity.Property(e => e.Status)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasComment("required");

                entity.Property(e => e.Subject)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("required");
            });

            modelBuilder.Entity<AppServiceRequestHistory>(entity =>
            {
                entity.ToTable("App_ServiceRequestHistory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("required;number");

                entity.Property(e => e.BoardId).HasComment("required;number");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedIp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Priority)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("required");

                entity.Property(e => e.RequestCategoryids).HasComment("required;number");

                entity.Property(e => e.ResolutionDate)
                    .HasColumnType("datetime")
                    .HasComment("required");

                entity.Property(e => e.ServiceRequestId).HasComment("required;number");

                entity.Property(e => e.ServiceRequestNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasComment("required");

                entity.Property(e => e.Subject)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("required,alphanumeric");
            });

            modelBuilder.Entity<AppTicket>(entity =>
            {
                entity.HasKey(e => e.TicketId)
                    .HasName("PK__Tickets__D596F96B6945A67D");

                entity.ToTable("App_Ticket");

                entity.Property(e => e.TicketId)
                    .ValueGeneratedNever()
                    .HasComment("required;number");

                entity.Property(e => e.AssignStatus)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasComment("required;alphabet");

                entity.Property(e => e.AssignTo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.BoardId).HasComment("required;number");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasComment("required");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedIp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Priority)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("required");

                entity.Property(e => e.SectionId).HasComment("required;number");

                entity.Property(e => e.ServiceRequestId).HasComment("required;number");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasComment("required");

                entity.Property(e => e.Subject)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.TaskStatus)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasComment("required;alphabet");

                entity.Property(e => e.TicketNo)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasComputedColumnSql("([dbo].[fn_SRandTKT]('TKT',[TicketId]))", false);
            });

            modelBuilder.Entity<AppTicketHistory>(entity =>
            {
                entity.ToTable("App_TicketHistory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("required;number");

                entity.Property(e => e.AssignStatus)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasComment("required;alphabet");

                entity.Property(e => e.AssignTo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.BoardId).HasComment("required;number");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedIp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasComment("required");

                entity.Property(e => e.InsertedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedIp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Priority)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasComment("required;alphabet");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.RequestCategoryId).HasComment("required;number");

                entity.Property(e => e.ServiceRequestId).HasComment("required;number");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasComment("required");

                entity.Property(e => e.Subject)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.TaskStatus)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasComment("required;alphabet");

                entity.Property(e => e.TicketId).HasComment("required;number");

                entity.Property(e => e.TicketNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");
            });

            modelBuilder.Entity<BakMdUserBoardRoleMapping>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bak_MD_UserBoardRoleMapping");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Esodata>(entity =>
            {
                entity.ToTable("ESOData");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccessToken)
                    .IsUnicode(false)
                    .HasColumnName("access_token");

                entity.Property(e => e.ClaimData)
                    .IsUnicode(false)
                    .HasColumnName("claim_data");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.ExpiresIn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("expires_in");

                entity.Property(e => e.RefreshToken)
                    .IsUnicode(false)
                    .HasColumnName("refresh_token");

                entity.Property(e => e.TokenId)
                    .IsUnicode(false)
                    .HasColumnName("token_id");

                entity.Property(e => e.TokenType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("token_type");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.Ip)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IP");

                entity.Property(e => e.Level).HasMaxLength(128);

                entity.Property(e => e.Properties).HasColumnType("xml");

                entity.Property(e => e.UserName).HasMaxLength(200);
            });

            modelBuilder.Entity<LogActivityAuditTrail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Log_ActivityAuditTrail");

                entity.Property(e => e.Activity).IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MdActionType>(entity =>
            {
                entity.HasKey(e => e.ActionTypeId);

                entity.ToTable("MD_ActionType");

                entity.Property(e => e.ActionTypeId)
                    .ValueGeneratedNever()
                    .HasComment("required;number");

                entity.Property(e => e.ActionType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("required;alphabet");
            });

            modelBuilder.Entity<MdAgency>(entity =>
            {
                entity.HasKey(e => e.AgencyId);

                entity.ToTable("Md_Agency");

                entity.Property(e => e.AgencyId)
                    .ValueGeneratedNever()
                    .HasComment("required;number");

                entity.Property(e => e.Abbreviation)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasComment("required;pattern;maxlength;minlength");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("address")
                    .HasComment("required");

                entity.Property(e => e.AgencyName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("required;pattern;maxlength;minlength");

                entity.Property(e => e.AgencyType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("required;pattern;maxlength;minlength");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("isActive")
                    .IsFixedLength()
                    .HasComment("required");

                entity.Property(e => e.Priority)
                    .HasColumnName("priority")
                    .HasComment("required");

                entity.Property(e => e.ServiceTypeId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("required;pattern");

                entity.Property(e => e.StateId)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");
            });

            modelBuilder.Entity<MdBoard>(entity =>
            {
                entity.HasKey(e => e.BoardId)
                    .HasName("PK__MD_Board__4D82CA9065728F9C");

                entity.ToTable("MD_Board");

                entity.Property(e => e.BoardId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("boardId");

                entity.Property(e => e.Abbreviation)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("abbreviation");

                entity.Property(e => e.AgencyId).HasColumnName("agencyId");

                entity.Property(e => e.BoardName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("boardName");
            });

            modelBuilder.Entity<MdDocumentType>(entity =>
            {
                entity.ToTable("MD_DocumentType");

                entity.Property(e => e.Id)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .HasComment("reuired");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_date");

                entity.Property(e => e.DisplayPriority)
                    .HasColumnName("displayPriority")
                    .HasComment("required;number");

                entity.Property(e => e.DocumentNatureType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("documentNatureType")
                    .IsFixedLength()
                    .HasComment("required;number");

                entity.Property(e => e.DocumentNatureTypeDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("documentNatureTypeDesc")
                    .HasComment("required;alphabet");

                entity.Property(e => e.Format)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("format")
                    .HasComment("required;alphabet");

                entity.Property(e => e.IsPasswordProtected)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("isPasswordProtected")
                    .IsFixedLength()
                    .HasComment("required");

                entity.Property(e => e.MaxSize)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("maxSize")
                    .HasComment("required");

                entity.Property(e => e.MinSize)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("minSize")
                    .HasComment("required");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("modified_date");

                entity.Property(e => e.Title)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("title")
                    .HasComment("required;alphanumeric");
            });

            modelBuilder.Entity<MdModule>(entity =>
            {
                entity.HasKey(e => e.ModuleId);

                entity.ToTable("MD_Module");

                entity.Property(e => e.ModuleId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.Heading)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Parent)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.Path)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasComment("required");

                entity.Property(e => e.SubHeading)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasComment("required;url");
            });

            modelBuilder.Entity<MdPriority>(entity =>
            {
                entity.HasKey(e => e.PriorityId);

                entity.ToTable("MD_Priority");

                entity.Property(e => e.PriorityId)
                    .ValueGeneratedNever()
                    .HasColumnName("priorityId");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.PriorityName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("priorityName");
            });

            modelBuilder.Entity<MdRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("MD_Role");

                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("roleId")
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.Description)
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("roleName")
                    .HasComment("required;alphabet");
            });

            modelBuilder.Entity<MdSection>(entity =>
            {
                entity.HasKey(e => e.SectionId);

                entity.ToTable("MD_Section");

                entity.Property(e => e.SectionId)
                    .ValueGeneratedNever()
                    .HasComment("required;number");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("required;alphabet");

                entity.Property(e => e.Section)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");
            });

            modelBuilder.Entity<MdSmsEmailTemplate>(entity =>
            {
                entity.HasKey(e => e.TemplateId);

                entity.ToTable("MD_SmsEmailTemplate");

                entity.Property(e => e.TemplateId)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.MessageSubject)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("messageSubject");

                entity.Property(e => e.MessageTemplate)
                    .IsUnicode(false)
                    .HasColumnName("messageTemplate");

                entity.Property(e => e.MessageTypeId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("messageTypeId");

                entity.Property(e => e.RegisteredTemplateId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MdStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Md_Status");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EntityType)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("entityType");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Priority).HasColumnName("priority");
            });

            modelBuilder.Entity<MdUserBoardMapping>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MD_UserBoardMapping");

                entity.Property(e => e.BoardId).HasComment("required;alphanumeric");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("required");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");
            });

            modelBuilder.Entity<MdUserBoardRoleMapping>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MD_UserBoardRoleMapping");

                entity.Property(e => e.BoardId).HasComment("required;number");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("required");

                entity.Property(e => e.RoleId).HasComment("required;number");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("required;alphanumeric");
            });

            modelBuilder.Entity<TblRnd>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TBlRnd");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<UserAuthorization>(entity =>
            {
                entity.ToTable("User_Authorization");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Mode)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("mode");

                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("refreshToken");

                entity.Property(e => e.Token)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("userId");
            });

            modelBuilder.Entity<WorkOrderDetails>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AgencyName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("agencyName");

                entity.Property(e => e.DocName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("docName");

                entity.Property(e => e.Document).HasColumnName("document");

                entity.Property(e => e.IssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("issueDate");

                entity.Property(e => e.NoofMonths)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("noofMonths");

                entity.Property(e => e.ProjectCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("projectCode");

                entity.Property(e => e.ResourceCategory)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("resourceCategory");

                entity.Property(e => e.ResourceNo)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("resourceNo");

                entity.Property(e => e.WorkorderFrom)
                    .HasColumnType("datetime")
                    .HasColumnName("workorderFrom");

                entity.Property(e => e.WorkorderNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("workorderNo");

                entity.Property(e => e.WorkorderTo)
                    .HasColumnType("datetime")
                    .HasColumnName("workorderTo");
            });

            modelBuilder.Entity<YourTable>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AlphanumericColumn)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("alphanumeric_column")
                    .HasComputedColumnSql("([dbo].[fn_SRandTKT]('SRN',[id]))", false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ZmstProjects>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.ToTable("Zmst_Projects");

                entity.Property(e => e.ProjectId)
                    .ValueGeneratedNever()
                    .HasColumnName("projectId")
                    .HasComment("required;number");

                entity.Property(e => e.AcademicYear)
                    .HasColumnName("academicYear")
                    .HasComment("required;number");

                entity.Property(e => e.AgencyId)
                    .HasColumnName("agencyId")
                    .HasComment("required");

                entity.Property(e => e.Attempt)
                    .HasColumnName("attempt")
                    .HasComment("required;number");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_date");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.ExamCounsid)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("examCounsid")
                    .HasComment("required");

                entity.Property(e => e.IsLive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("modified_date");

                entity.Property(e => e.Pinitiated)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PInitiated")
                    .IsFixedLength();

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("projectName")
                    .HasComment("required;alphanumeric");

                entity.Property(e => e.RequestLetter).HasColumnName("requestLetter");

                entity.Property(e => e.ServiceType)
                    .HasColumnName("serviceType")
                    .HasComment("required;number");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
