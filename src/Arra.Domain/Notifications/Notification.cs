using Arra.Domain.Users;
using Arra.SharedKernel;

namespace Arra.Domain.Notifications;

public sealed class Notification : Entity
{
    private Notification(
        Guid id,
        Guid userId,
        NotificationMessage message,
        NotificationType type,
        bool isRead,
        bool isEnabled,
        DateTime createdOnUtc) : base(id)
    {
        UserId = userId;
        Message = message;
        Type = type;
        IsRead = isRead;
        IsEnabled = isEnabled;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid UserId { get; private set; }

    public User User { get; private set; }

    public NotificationMessage Message { get; private set; }

    public NotificationType Type { get; private set; }

    public bool IsRead { get; private set; }

    public bool IsEnabled { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public static Notification Create(
        Guid userId,
        NotificationMessage message,
        NotificationType type,
        bool isRead,
        bool isEnabled,
        DateTime createdOnUtc)
    {
        var newNotification = new Notification(
            Guid.NewGuid(),
            userId,
            message,
            type,
            isRead,
            isEnabled,
            createdOnUtc);

        return newNotification;
    }
}
