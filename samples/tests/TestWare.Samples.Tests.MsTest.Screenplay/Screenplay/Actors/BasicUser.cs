
using TestWare.Core.Interfaces;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Actors;

public class BasicUser : Actor
{
    public string Name;
    public string Password;

    public BasicUser(string name, string password) : base()
    {
        Name = name;
        Password = password;
    }
}

public class StandardUser() : BasicUser("standard_user", "secret_sauce")
{
}