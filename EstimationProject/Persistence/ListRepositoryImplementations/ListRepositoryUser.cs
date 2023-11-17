using Domain;
using Services.Interfaces.Repository;

namespace Persistance.ListRepositoryImplementations;

public class ListRepositoryUser : IRepositoryUser
{
    private List<User> _users = new List<User>();
    
    public User? GetUser(string email)
    {
        return  _users.FirstOrDefault((user => user.Email == email));
    }

    public void RemoveUser(string email)
    {
        var foundUser = GetUser(email);
        if (foundUser != null)
        {
            lock (_users)
            {
                _users.Remove(foundUser);
            }
        }
    }

    public void RemoveUser(User user)
    {
        RemoveUser(user.Email);
    }

    public void AddUser(User user)
    {
        if (user.Username == "" || user.Password.HashedPassword == "" || user.Password.Salt == "" || user.Email == "")
            return;

        lock (_users)
        {
            var foundUser = GetUser(user.Email);
            if (foundUser == null)
            {
                _users.Add(user);
            }
        }
    }

    public void EditUser(User user, string previousEmail)
    {
        var foundUser = GetUser(previousEmail);
        if (foundUser == null)
            return;

        lock (_users)
        {
            foundUser.Password = user.Password;
            foundUser.Email = user.Email;
            foundUser.Username = user.Username;
        }
    }
}