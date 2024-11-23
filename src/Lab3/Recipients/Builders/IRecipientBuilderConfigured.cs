using Itmo.ObjectOrientedProgramming.Lab3.Loggers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients.Builders;

public interface IRecipientBuilderConfigured
{
    IRecipientBuilderConfigured WithLogger(IMessageLogger messageLogger);

    IRecipientBuilderConfigured WithFilter(int importanceLevel);

    IRecipient BuildRecipient();
}