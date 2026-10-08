using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OsmPlayground.Data.Context;

public class OsmDbContext : DbContext
{
    public OsmDbContext()
    {
    }

    public OsmDbContext(DbContextOptions<OsmDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=OsmPlayground", x => x.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("hstore")
            .HasPostgresExtension("postgis");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
