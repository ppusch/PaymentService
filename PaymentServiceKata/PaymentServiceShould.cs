using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;

namespace PaymentServiceKata
{
    public class PaymentServiceShould
    {
        [Test]
        public void ThrowException_When_User_IsNotValid()
        {
            IPaymentGateway paymentGateway = A.Fake<IPaymentGateway>();
            IUserService userService = A.Fake<IUserService>();


            var paymentService = new PaymentService(userService, paymentGateway);

            User invalidUser = null;
            PaymentDetails paymentDetails = null;

            Assert.Throws<Exception>((() => paymentService.ProcessPayment(invalidUser, paymentDetails)));
            A.CallTo(() => paymentGateway.Send(paymentDetails)).MustNotHaveHappened();
        }

        [Test]
        public void send_payment_to_payment_gateway_when_user_is_valid()
        {
            User validUser = null;
            PaymentDetails paymentDetails = null;
            IPaymentGateway paymentGateway = A.Fake<IPaymentGateway>();
            IUserService userService = A.Fake<IUserService>();
            A.CallTo(() => userService.IsValid(validUser)).Returns(true);

            var paymentService = new PaymentService(userService, paymentGateway);
            paymentService.ProcessPayment(validUser, paymentDetails);

            A.CallTo(() => paymentGateway.Send(paymentDetails)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }

    public class PaymentService
    {
        private readonly IUserService _userService;
        private readonly IPaymentGateway _paymentGateway;

        public PaymentService(IUserService userService, IPaymentGateway paymentGateway)
        {
            _userService = userService;
            _paymentGateway = paymentGateway;
        }

        public void ProcessPayment(User user, PaymentDetails paymentDetails)
        {
            if (!_userService.IsValid(user))
            {
                throw new Exception();
            }
            _paymentGateway.Send(paymentDetails);
        }
    }
}
