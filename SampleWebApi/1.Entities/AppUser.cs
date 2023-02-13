using Microsoft.AspNetCore.Identity;

namespace SampleWebApi._1.Entities;

public class AppUser:IdentityUser, IEntity
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PostalCode { get; set; }
    public virtual List<Address> LstAddress { get; set; }
}
