#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LoginAndReg.Models;

public class User
{
    [Key]
    public int id {get;set;}

    [Required(ErrorMessage = "is required")]
    [MinLength(2, ErrorMessage = "must be at least 2 characters")]
    [Display(Name = "firstName")]
    public string firstName {get;set;}

    [Required(ErrorMessage = "is required")]
    [MinLength(2, ErrorMessage = "must be at least 2 characters")]
    [Display(Name = "lastName")]
    public string lastName {get;set;}

    [Required(ErrorMessage = "is required")]
    [EmailAddress(ErrorMessage = "must be a valid Email")]
    [Display(Name = "email")]
    public string email {get;set;}

    [Required(ErrorMessage = "is required")]
    [MinLength(8, ErrorMessage = "must be at least 8 characters")]
    [DataType(DataType.Password)]
    [Display(Name = "password")]
    public string password {get;set;}

    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("password", ErrorMessage = "password doesn't match")]
    [Display(Name = "confirmPass")]
    public string confirmPass {get;set;}
    public DateTime created_at {get;set;} = DateTime.Now;
    public DateTime updated_at {get;set;} = DateTime.Now;
}