using System;

namespace LegacyApp
{
    public class User
    {
        private string _firstName;
        private string _lastName;
        private string _emailAddress;
        private DateTime _dateOfBirth;
        public Client Client { get; internal set; }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            internal set
            {
                var now = DateTime.Now;
                var age = now.Year - _dateOfBirth.Year;
                if (now.Month < _dateOfBirth.Month || (now.Month == _dateOfBirth.Month && now.Day < _dateOfBirth.Day)) age--;

                if (age < 21)
                {
                    throw new ArgumentException("Age of user must be at least 21 years old.");
                }
            }
        }

        public string EmailAddress
        {
            get => _emailAddress;
            internal set
            {
                if (!_emailAddress.Contains("@") && !_emailAddress.Contains("."))
                {
                    throw new ArgumentException("Email address of user must contain character  '.' and '@'.");
                }
            }
        }

        public string FirstName
        {
            get => _firstName;
            internal set
            {
                if (string.IsNullOrEmpty(_firstName))
                {
                    throw new ArgumentException("First name of user can't be null or empty.");
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            internal set
            {
                if (string.IsNullOrEmpty(_lastName))
                {
                    throw new ArgumentException("Last name of user can't be null or empty.");
                }
            }
        }

        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }
    }
}