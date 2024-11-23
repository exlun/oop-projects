namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients.Builders;

public interface IRecipientBuilderInitializer
{
    IRecipientBuilderConfigured WithRecipient(IRecipient recipient);
}