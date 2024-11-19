using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients.Builders;

public class RecipientBuilder : IRecipientBuilderInitializer, IRecipientBuilderConfigured
{
    private RecipientBuilder() { }

    public static IRecipientBuilderInitializer GetBuilder()
    {
        return new RecipientBuilder();
    }

    private IRecipient? Recipient { get; set; }

    public IRecipientBuilderConfigured WithRecipient(IRecipient recipient)
    {
        Recipient = recipient;
        return this;
    }

    public IRecipientBuilderConfigured WithLogger(IMessageLogger messageLogger)
    {
        if (Recipient == null)
        {
            throw new ArgumentException("Recipient is null");
        }

        Recipient = new MessageLoggerProxy(Recipient, messageLogger);
        return this;
    }

    public IRecipientBuilderConfigured WithFilter(int importanceLevel)
    {
        if (Recipient == null)
        {
            throw new ArgumentException("Recipient is null");
        }

        Recipient = new MessageFilterProxy(Recipient, importanceLevel);
        return this;
    }

    public IRecipient BuildRecipient()
    {
        return Recipient ?? throw new ArgumentException("Recipient has not been configured");
    }
}