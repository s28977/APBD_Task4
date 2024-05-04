using System;

namespace LegacyApp
{
    //adds user to user db
    public class UserService(IClientRepository clientRepository)
    {
        private readonly IClientRepository _clientRepository = clientRepository;

        public UserService() : this(new ClientRepository())
        {
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var client = _clientRepository.GetById(clientId);
            User user;
            try
            {
                user = new User
                {
                    Client = client,
                    DateOfBirth = dateOfBirth,
                    EmailAddress = email,
                    FirstName = firstName,
                    LastName = lastName
                };
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
                return false;
            }
            

            if (client.Type == ClientType.VeryImportant)
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == ClientType.Important)
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName);
                    user.CreditLimit = creditLimit;
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
