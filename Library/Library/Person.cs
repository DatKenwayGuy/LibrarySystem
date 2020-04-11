﻿namespace Library
{
    public class Person
    {
        public Person(string initals, string surname, string streetName="unknown", 
            string zipcode= "unknown", string city= "unknown", string emailAddress = "unknown",
            string telephoneNumber= "unknown"
        )
        {
            this.Initials = initals;
            this.Surname = surname;
            this.StreetName = streetName;
            this.ZipCode = zipcode;
            this.City = city;
            this.EmailAddress = emailAddress;
            this.TelephoneNumber = telephoneNumber;

        }
        protected string ID;

        protected string Initials { get; set; }
        protected string Surname { get; set; }
        protected string StreetName { get; set; }
        protected string ZipCode { get; set; } 
        protected string City { get; set; }
        protected string EmailAddress { get; set; }
        protected string TelephoneNumber { get; set; }
    }
}