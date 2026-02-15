using Articles.Abstractions;
using Auth.Domain.Persons.ValueObjects;
using Auth.Domain.Users;

namespace Auth.Domain.Persons;

public partial class Person
{
    public static Person Create(IPersonCreationInfo personInfo)
    {
        var person = new Person
        {
            FirstName = personInfo.FirstName,
            LasttName = personInfo.LastName,
            Email = personInfo.Email,
            Gender = personInfo.Gender,
            PictureUrl = personInfo.PictureUrl,
            Honorific = HonorificTitle.FromEnum(personInfo.Honorific),
            ProfessionalProfile = ProfessionalProfile.Create(
                affiliation: personInfo.Affiliation,
                companyName: personInfo.CompanyName,
                position: personInfo.Position
            )
        };

        // TODO: Add domain events if needed

        return person;
    }

    public void AssignUser(User user)
    {

        this.UserId = user.Id;
        this.Email.NormalizedEmail = user.NormalizedEmail!;
    }
}
