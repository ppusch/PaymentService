namespace PaymentServiceKata
{
    public interface IPaymentGateway
    {
        void Send(PaymentDetails paymentDetails);
    }
}