namespace LegacyApp;

public interface IUserCreditRepository
{
    int GetCreditLimit(string lastName);
}