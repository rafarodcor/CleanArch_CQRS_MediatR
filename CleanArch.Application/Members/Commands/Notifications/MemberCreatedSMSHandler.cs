using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArch.Application.Members.Commands.Notifications;

internal class MemberCreatedSMSHandler : INotificationHandler<MemberCreatedNotification>
{
    private readonly ILogger<MemberCreatedSMSHandler> _logger;

    public MemberCreatedSMSHandler(ILogger<MemberCreatedSMSHandler> logger) => _logger = logger;

    public Task Handle(MemberCreatedNotification notification, CancellationToken cancellationToken)
    {
        // Send a confirmation SMS
        _logger.LogInformation($"Confirmation SMS sent for : {notification.Member.LastName}");

        //lógica para enviar SMS

        return Task.CompletedTask;
    }
}