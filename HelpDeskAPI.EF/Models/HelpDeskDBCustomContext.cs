using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
//using CommonModels = HelpDeskAPI.Common.Models;
namespace HelpDeskAPI.Data.EF.Models;

public partial class HelpDeskDBContext : DbContext
{
    //public virtual DbSet<CommonModels.MDStatus> MDStatus { get; set; }

    //modelBuilder.Entity<AbsCustomModels.MDStatus>(entity => { entity.HasNoKey(); });
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new NotImplementedException("Hard coded connection strings are not allowed, use appsettings.json to configure database connection.");
        }
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        //auditInfoMapper.MapAuditColumns(ChangeTracker.Entries());
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges()
    {
        //auditInfoMapper.MapAuditColumns(ChangeTracker.Entries());
        return base.SaveChanges();
    }
}