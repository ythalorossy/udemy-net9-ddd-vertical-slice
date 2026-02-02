using EmailService.Contracts;
using MimeKit;

namespace EmailService.Smtp;

internal static class Extensions
{
    public static MailboxAddress ToMailboxAddress(this EmailAddress emailAddress)
        => new(emailAddress.Name, emailAddress.Address);

    public static MimeMessage ToMailKitMessage(this EmailMessage emailMessage)
    {
        var message = new MimeMessage
        {
            Subject = emailMessage.Subject
        };

        message.From.Add(emailMessage.From.ToMailboxAddress());

        message.To.AddRange(emailMessage.To.Select(t => t.ToMailboxAddress()));

        var bodyBuilder = new BodyBuilder();

        if (emailMessage.Content.Type == EmailService.Contracts.ContentType.Html)
        {
            bodyBuilder.HtmlBody = emailMessage.Content.Value;
        }
        else
        {
            bodyBuilder.TextBody = emailMessage.Content.Value;
        }

        message.Body = bodyBuilder.ToMessageBody();

        return message;
    }
}
