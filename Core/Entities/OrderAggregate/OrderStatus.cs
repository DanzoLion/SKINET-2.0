using System.Runtime.Serialization;

namespace Core.Entities.OrderAggregate
{
    public enum OrderStatus                                 // enum tracks the status our order is in
    {
        [EnumMember(Value = "Pending")]
        Pending,                                                    // uising enum the satus given here is 0 - Pending | 1 - Payment Received | 2 PaymentFailed

        [EnumMember(Value = "Payment Received")]
        PaymentReceived,

        [EnumMember(Value = "Payment Failed")]
        PaymentFailed,
    }
}