﻿using System.Text.RegularExpressions;

namespace e_handelsystem.Models
{
    public class UserInputModel
    {
        private string firstName;
        private string lastName;
        private string email;
        private string password;
        private string addressLine;
        private string postalCode;
        private string city;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value.Trim(); }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value.Trim(); }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (new Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$").IsMatch(value.Trim()))
                    email = value.Trim();
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (new Regex(@"^(?=.*?[A-Ö])(?=.*?[a-ö])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").IsMatch(value.Trim()))
                    password = value.Trim();
            }
        }

        public string AddressLine
        {
            get { return addressLine; }
            set { addressLine = value.Trim(); }
        }

        public string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value.Replace(" ", ""); }
        }

        public string City
        {
            get { return city; }
            set { city = value.Trim(); }
        }
    }
}