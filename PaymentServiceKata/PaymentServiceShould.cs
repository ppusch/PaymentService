using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PaymentServiceKata
{
    public class PaymentServiceShould
    {
        [Test]
        public void ThrowException_When_User_IsNotValid()
        {
            var paymentService = new PaymentService();

            User invalidUser = null;
            PaymentDetails paymentDetails = null;
            Assert.Throws<Exception>((() => paymentService.ProcessPayment(invalidUser, paymentDetails)));
        }
     }

    public class PaymentService
    {
        public void ProcessPayment(User user, PaymentDetails paymentDetails)
        {
            throw new Exception();
        }
    }
}
