using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Factory.Models
{
  public class FactoryContext : DbContext
  {
    public virtual DbSet<Machine> Machines { get; set; }
    public virtual DbSet<Engineer> Engineers { get; set; }
    public virtual DbSet<MachineEngineers> MachineEngineers { get; set; }
    public FactoryContext(DbContextOptions builder) : base(builder) { }
  }
}