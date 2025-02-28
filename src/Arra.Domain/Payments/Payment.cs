using Arra.Domain.Groups;
using Arra.Domain.Payments.Events;
using Arra.Domain.Users;
using Arra.SharedKernel;

namespace Arra.Domain.Payments;

public sealed class Payment : Entity
{
    private Payment(
        Guid id,
        Guid groupId,
        Group group,
        Guid paidByUserId,
        Guid paidToUserId,
        Money amount,
        DateTime paidOnUtc) : base(id)
    {
        GroupId = groupId;
        Group = group;
        PaidByUserId = paidByUserId;
        PaidToUserId = paidToUserId;
        Amount = amount;
        PaidOnUtc = paidOnUtc;
    }

    public Guid GroupId { get; private set; }

    public Group Group { get; private set; }

    public Guid PaidByUserId { get; private set; }

    public User PaidByUser { get; private set; }

    public Guid PaidToUserId { get; private set; }

    public User PaidToUser { get; private set; }

    public Money Amount { get; private set; }

    public DateTime PaidOnUtc { get; private set; }

    public static Payment Create(
        Guid groupId,
        Group group,
        Guid paidByUserId,
        Guid paidToUserId,
        Money amount,
        DateTime paidOnUtc)
    {
        var newPayment = new Payment(
            Guid.NewGuid(),
            groupId,
            group,
            paidByUserId,
            paidToUserId,
            amount,
            paidOnUtc);

        newPayment.RaiseDomainEvent(new PaymentMadeDomainEvent(newPayment.Id));

        return newPayment;
    }
}
