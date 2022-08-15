#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace LoginAndReg.Models;

public class LoginAndRegContext : DbContext
{
    public LoginAndRegContext(DbContextOptions options) : base(options) { }
    public DbSet<User> users {get;set;}
}