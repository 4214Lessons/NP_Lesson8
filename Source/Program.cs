using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using System.Net;
using System.Net.Mail;


var email = "nplesson@gmail.com";
var password = "yzbnoxfesqdbgfcj";


// Smtp();
// Imap();



void Imap()
{
    var host = "imap.gmail.com";
    var port = 993;

    using var client = new ImapClient();

    client.Connect(host, port, SecureSocketOptions.SslOnConnect);
    client.Authenticate(email, password);

    client.Inbox.Open(FolderAccess.ReadOnly);

    int messageCount = client.Inbox.Count;
    Console.WriteLine(messageCount);

    for (int i = 0; i < messageCount; i++)
    {
        var message = client.Inbox.GetMessage(i);

        Console.WriteLine($"From: {message.From}");
        Console.WriteLine($"Subject: {message.Subject}");
        Console.WriteLine($"Body: {message.Body}");
    }

    client.Disconnect(true);
}


void Smtp()
{
    var host = "smtp.gmail.com";
    var port = 587;

    using var client = new SmtpClient(host)
    {
        Port = port,
        EnableSsl = true,
        Credentials = new NetworkCredential(email, password)
    };

    using var message = new MailMessage()
    {
        Body = "Test message body",
        Subject = "Test subject",
        IsBodyHtml = false,
    };

    message.From = new MailAddress(email);
    message.To.Add(new MailAddress("turalinovruzov@gmail.com"));

    client.Send(message);
}