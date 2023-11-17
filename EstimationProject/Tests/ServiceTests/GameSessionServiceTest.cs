using Domain;
using Moq;
using Services.Interfaces.Input;
using Services.Interfaces.Repository;
using Services.Services;

namespace Tests.ServiceTests;

public class GameSessionServiceTest
{
    //AddNewSessionUser
    [Fact]
    public void ShouldAddSessionUser_WhenUserIsNotAlreadyAdded()
    {
        //Arrange
        User user = GenerateCorrectUser();
        GameSessionUser? found = null;

        var mockRepositoryUser = new Mock<IRepositoryUser>();
        mockRepositoryUser.Setup(x => x.GetUser(user.Email)).Returns(user);

        var mockRepositoryGameSession = new Mock<IRepositoryGameSession>();
        
        var mockRepositoryGameSessionUser = new Mock<IRepositoryGameSessionUser>();
        mockRepositoryGameSessionUser.Setup(x => x.GetUser(user.Email)).Returns(found);
        
        IGameSessionService gameSessionService = new GameSessionService(mockRepositoryUser.Object, mockRepositoryGameSession.Object,mockRepositoryGameSessionUser.Object);

        //Act
        gameSessionService.AddNewSessionUser(user.Email);

        //Assert
        mockRepositoryGameSessionUser.Verify(x => x.AddUser(It.IsAny<GameSessionUser>()), Times.Once);
    }

    //RemoveSessionUser
    [Fact]
    public void ShouldRemoveSessionUser_WhenUserIsAlreadyAdded()
    {
        //Arrange
        User user = GenerateCorrectUser();
        GameSessionUser sessionUser = new GameSessionUser(user);

        var mockRepositoryUser = new Mock<IRepositoryUser>();
        var mockRepositoryGameSession = new Mock<IRepositoryGameSession>();
        var mockRepositoryGameSessionUser = new Mock<IRepositoryGameSessionUser>();
        mockRepositoryGameSessionUser.Setup(x => x.GetUser(user.Email)).Returns(sessionUser);
        
        IGameSessionService gameSessionService = new GameSessionService(mockRepositoryUser.Object, mockRepositoryGameSession.Object,mockRepositoryGameSessionUser.Object);
        
        //Act
        gameSessionService.RemoveSessionUser(user.Email);

        //Assert
        mockRepositoryGameSessionUser.Verify(x => x.RemoveUser(sessionUser), Times.Once);
    }
    
    //JoinSession
    [Fact]
    public void ShouldJoinSession_WhenUserIsValidAndConnected()
    {
        //Arrange
        User user = GenerateCorrectUser();
        GameSessionUser sessionUser = new GameSessionUser(user);

        GameSession gameSession = GenerateCorrectGameSession();
        string expectedSessionId = gameSession.GameSessionId;
            
        var mockRepositoryUser = new Mock<IRepositoryUser>();
        var mockRepositoryGameSession = new Mock<IRepositoryGameSession>();
        mockRepositoryGameSession.Setup(x => x.GetSession(gameSession.GameSessionId)).Returns(gameSession);
        var mockRepositoryGameSessionUser = new Mock<IRepositoryGameSessionUser>();
        mockRepositoryGameSessionUser.Setup(x => x.GetUser(user.Email)).Returns(sessionUser);

        IGameSessionService gameSessionService = new GameSessionService(mockRepositoryUser.Object,
            mockRepositoryGameSession.Object, mockRepositoryGameSessionUser.Object);
        
        //Act
        gameSessionService.JoinSession(user.Email, gameSession.GameSessionId);

        //Assert
        Assert.Contains<GameSessionUser>(gameSession.ActiveUsers,
            gameSessionUser => gameSessionUser.Equals(sessionUser));
        Assert.Equal(expectedSessionId, sessionUser.SessionId);
    }

    [Fact]
    public void ShouldLeaveSession_WhenUserIsConnectedAndCurrentlyInSession()
    {
        //Arrange
        User user = GenerateCorrectUser();
        GameSessionUser sessionUser = new GameSessionUser(user);

        GameSession gameSession = GenerateCorrectGameSession();
        
        gameSession.AddUser(sessionUser); //Czy nie powinienem zastosować tu moka?
        sessionUser.SessionId = gameSession.GameSessionId;

        string? expectedSessionId = null;
        
        
        var mockRepositoryUser = new Mock<IRepositoryUser>();
        var mockRepositoryGameSession = new Mock<IRepositoryGameSession>();
        mockRepositoryGameSession.Setup(x => x.GetSession(gameSession.GameSessionId)).Returns(gameSession);
        var mockRepositoryGameSessionUser = new Mock<IRepositoryGameSessionUser>();
        mockRepositoryGameSessionUser.Setup(x => x.GetUser(user.Email)).Returns(sessionUser);

        IGameSessionService gameSessionService = new GameSessionService(mockRepositoryUser.Object,
            mockRepositoryGameSession.Object, mockRepositoryGameSessionUser.Object);

        //Act
        gameSessionService.LeaveSession(user.Email, gameSession.GameSessionId);

        //Assert
        Assert.DoesNotContain(gameSession.ActiveUsers, gameSessionUser => gameSessionUser.Equals(sessionUser));
        Assert.Equal(expectedSessionId, sessionUser.SessionId);
    }
    
    //TODO: Tests about transaction for joinSession and leave session (if possible)

    private User GenerateCorrectUser()
    {
        string email = "test@email.com";
        string name = "test";
        string password = "test";

        var hashedPassword = Password.CreateHashedPassword(password);
        
        User user = new User(email, name, hashedPassword);
        return user;
    }

    private GameSession GenerateCorrectGameSession()
    {
        string sessionId = "testSession123";
        string sessionName = "Test Session 123";
        
        GameSession gameSession = GameSession.CreateSession(sessionId, sessionName);
        return gameSession;
    }
}