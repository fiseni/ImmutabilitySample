using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutabilitySample.ValueObjects
{
    public class Customer : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }

        public Customer(string firstName, string lastName, string email)
        {
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentNullException(nameof(firstName));
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentNullException(nameof(lastName));

            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.FirstName;
            yield return this.LastName;
            yield return this.Email;
        }
    }
}
