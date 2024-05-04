namespace LegacyApp;

public class UserCreditRepository : IUserCreditRepository
{
    public int GetCreditLimit(string lastName)
    {
        using var databaseReader = new UserCreditDatabaseReader();
        return databaseReader.GetCreditLimit(lastName);
    }
}