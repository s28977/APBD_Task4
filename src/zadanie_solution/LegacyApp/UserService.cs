using System;

namespace LegacyApp;

public class UserService(IClientRepository clientRepository, IUserCreditRepository userCreditRepository)
{
    public UserService() : this(new ClientRepository(), new UserCreditRepository())
    {
    }

    public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
    {
        var user = CreateUser(firstName, lastName, email, dateOfBirth, clientId);
        if (user == null) return false;
        UserDataAccess.AddUser(user);
        return true;
    }

    private User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
    {
        User user;
        var client = clientRepository.GetById(clientId);
        try
        {
            user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName,
            };
        }
        catch (ArgumentException argumentException)
        {
            Console.WriteLine(argumentException.Message);
            return null;
        }
        return !SetUserCreditLimit(user) ? null : user;
    }

    private bool SetUserCreditLimit(User user)
    {
        var creditLimit = GetCreditLimit(user.LastName, user.Client.Type);
        user.HasCreditLimit = creditLimit.HasValue;
        user.CreditLimit = creditLimit.GetValueOrDefault(0);
        return !(user.HasCreditLimit && user.CreditLimit < 500);
    }

    private int? GetCreditLimit(string lastName, ClientType type)
    {
        var creditLimit = userCreditRepository.GetCreditLimit(lastName);
        return type switch
        {
            ClientType.Normal => creditLimit,
            ClientType.Important => 2 * creditLimit,
            ClientType.VeryImportant => null,
            _ => throw new ArgumentOutOfRangeException(null,
                $"User credit limit for this client type is not specified.")
        };
    }
}