using Domain;
using Services.Interfaces.Repository;

namespace Persistance.ListRepositoryImplementations;

public class ListRepositoryGameSessionUser : IRepositoryGameSessionUser
{
    private List<GameSessionUser> _sessionUsers = new List<GameSessionUser>();
    
    public List<GameSessionUser> GetUsers()
    {
        return _sessionUsers.ToList();
    }

    public GameSessionUser? GetUser(string email)
    {
        return _sessionUsers.FirstOrDefault(user => user.User.Email == email);
    }

    public void RemoveUser(string email)
    {
        lock (_sessionUsers)
        {
            var foundUser = GetUser(email);
            if (foundUser != null)
                _sessionUsers.Remove(foundUser);
        }
        
    }

    public void RemoveUser(GameSessionUser sessionUser)
    {
        lock (_sessionUsers)
        {
            if (_sessionUsers.Contains(sessionUser))
                _sessionUsers.Remove(sessionUser);
        }
    }

    public void AddUser(GameSessionUser sessionUser)
    {
        lock (_sessionUsers)
        {
            var found = GetUser(sessionUser.User.Email);
            if(found == null)
                _sessionUsers.Add(sessionUser);
        }
    }
}