using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebuggerDisplay
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer("James","Butt"),
                new Customer("Josephine","Darakjy"),
                new Customer("Art","Venere"),
                new Customer("Lenna","Paprocki"),
                new Customer("Donette","Foller"),
                new Customer("Simona","Morasca")
            };
        }
    }
    ////Method ::: 2
    ////DebuggerDisplay attribute
    [DebuggerDisplay("Name: {FullName,nq}")]
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get => $"{FirstName} {LastName}"; }

        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        ////Method ::: 1
        ////Override ToString()

        //public override string ToString()
        //{
        //    return FullName;
        //}
    }
}
