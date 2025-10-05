namespace Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Users;

public abstract record MessageCheckResult
{
    public sealed record MessageNotFound : MessageCheckResult;

    public sealed record MessageFound(MessageState MessageState) : MessageCheckResult
    {
        public bool IsRead => MessageState.IsRead;
    }
}