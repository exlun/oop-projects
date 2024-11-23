using Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Messengers;
using Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Users;
using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients.Builders;
using Itmo.ObjectOrientedProgramming.Lab3.Utils;
using Moq;
using Xunit;

namespace Lab3.Tests;

public class MessageSystemTests
{
    [Fact]
    public void User_RecievingMessageTest_IsUnread()
    {
        var message = new Message("Header", "Body", 1);
        var user = new User("TestUser");
        user.ReceiveMessage(message);

        MessageCheckResult messageStatus = user.CheckMessageStatus(message);
        Assert.IsType<MessageCheckResult.MessageFound>(messageStatus);

        var messageResult = messageStatus as MessageCheckResult.MessageFound;
        Assert.NotNull(messageResult);
        Assert.False(messageResult.IsRead);
    }

    [Fact]
    public void User_ReadingMessageTest_IsRead()
    {
        var message = new Message("Header", "Body", 1);
        var user = new User("TestUser");
        user.ReceiveMessage(message);
        user.ReadMessage(message);

        MessageCheckResult messageStatus = user.CheckMessageStatus(message);
        Assert.IsType<MessageCheckResult.MessageFound>(messageStatus);

        var messageResult = messageStatus as MessageCheckResult.MessageFound;
        Assert.NotNull(messageResult);
        Assert.True(messageResult.IsRead);
    }

    [Fact]
    public void User_ReadingMessageTwiceTest_ReturnsFalse()
    {
        var message = new Message("Header", "Body", 1);
        var user = new User("TestUser");
        user.ReceiveMessage(message);
        user.ReadMessage(message);

        Assert.False(user.ReadMessage(message));
    }

    [Fact]
    public void MessageFilterProxy_ReceivingMessageBelowImportanceLevel_IsNotRecieved()
    {
        var recipientMock = new Mock<IRecipient>();
        int importanceLevel = 5;
        IRecipientBuilderInitializer recipientBuilder = RecipientBuilder.GetBuilder();
        IRecipient recipientWithFilter = recipientBuilder.WithRecipient(recipientMock.Object).WithFilter(importanceLevel).BuildRecipient();
        var message = new Message("Header", "Body", 3);

        recipientWithFilter.ReceiveMessage(message);

        recipientMock.Verify(r => r.ReceiveMessage(It.IsAny<Message>()), Times.Never);
    }

    [Fact]
    public void MessageLoggerProxy_ReceivingMessage_ShouldBeLogged()
    {
        var recipientMock = new Mock<IRecipient>();
        var loggerMock = new Mock<IMessageLogger>();
        IRecipientBuilderInitializer recipientBuilder = RecipientBuilder.GetBuilder();
        IRecipient recipientWithLogger = recipientBuilder.WithRecipient(recipientMock.Object).WithLogger(loggerMock.Object).BuildRecipient();
        var message = new Message("Header", "Body", 1);

        recipientWithLogger.ReceiveMessage(message);

        loggerMock.Verify(l => l.Log(message), Times.Once);
        recipientMock.Verify(r => r.ReceiveMessage(message), Times.Once);
    }

    [Fact]
    public void Messenger_ReceivingMessage_ShouldBeReceived()
    {
        var message = new Message("Header", "Body", 1);
        var consoleMock = new Mock<IConsole>();
        var messenger = new ConsoleMessenger(consoleMock.Object);

        // Act
        messenger.ReceiveMessage(message);

        // Assert
        consoleMock.Verify(c => c.WriteLine($"Messenger: {message}"), Times.Once);
    }

    [Fact]
    public void UserWithTwoFilters_ReceivingMessageNotSatisfyingOneFilter_ShouldReceiveMessageOnce()
    {
        var user = new User("User1");
        var recipientMock = new Mock<IRecipient>();
        int importanceLevelLow = 3;
        int importanceLevelHigh = 5;

        IRecipientBuilderInitializer recipientBuilder = RecipientBuilder.GetBuilder();
        IRecipient lowFilterRecipient = recipientBuilder.WithRecipient(recipientMock.Object).WithFilter(importanceLevelLow).BuildRecipient();
        IRecipient highFilterRecipient = recipientBuilder.WithRecipient(recipientMock.Object).WithFilter(importanceLevelHigh).BuildRecipient();
        var message = new Message("Header", "Body", 4);

        lowFilterRecipient.ReceiveMessage(message);
        highFilterRecipient.ReceiveMessage(message);

        recipientMock.Verify(r => r.ReceiveMessage(message), Times.Once);
    }
}