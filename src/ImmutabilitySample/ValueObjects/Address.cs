using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutabilitySample.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string Country { get; }

        public Address(string street, string city, string postalCode, string country)
        {
            if (string.IsNullOrEmpty(street)) throw new ArgumentNullException(nameof(street));
            if (string.IsNullOrEmpty(city)) throw new ArgumentNullException(nameof(city));
            if (string.IsNullOrEmpty(postalCode)) throw new ArgumentNullException(nameof(postalCode));
            if (string.IsNullOrEmpty(country)) throw new ArgumentNullException(nameof(country));

            this.Street = street;
            this.City = city;
            this.PostalCode = postalCode;
            this.Country = country;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Street;
            yield return this.City;
            yield return this.PostalCode;
            yield return this.Country;
        }
    }
}
