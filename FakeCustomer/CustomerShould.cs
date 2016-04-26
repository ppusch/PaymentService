using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NFluent;

namespace FakeCustomer
{
    public class CustomerShould
    {
        [Test]
        public void Return_Robin()
        {
            ICustomerRepository customerRepository = A.Fake<ICustomerRepository>();
            A.CallTo(() => customerRepository.Get(1)).Returns("Robin");
            var customer = new Customer(customerRepository);
            var actual =customer.GetCustomerByID(1);
            Check.That(actual).Equals("Robin");
        }

    }

    public interface ICustomerRepository
    {
        string  Get(int Id);
    }

    public class Customer
    {
        private ICustomerRepository customerRepository;

        public Customer(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public string GetCustomerByID(int customerId)
        {
            return customerRepository.Get(customerId);
        }
    }
}
