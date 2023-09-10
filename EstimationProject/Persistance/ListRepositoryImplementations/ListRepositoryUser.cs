using Domain;
using Services.Interfaces.Repository;

namespace Persistance.ListRepositoryImplementations;

public class ListRepositoryUser : IRepositoryUser
{
    public List<User> Users = new List<User>();
    
    public User? GetUser(string email)
    {
        return  Users.FirstOrDefault((user => user.Email == email));
    }

    public void RemoveUser(string email)
    {
        var foundUser = GetUser(email);
        if (foundUser != null)
        {
            lock (Users)
            {
                Users.Remove(foundUser);
            }
        }
    }

    public void RemoveUser(User user)
    {
        RemoveUser(user.Email);
    }

    public void AddUser(User user)
    {
        if (user.Username == "" || user.Password == "" || user.Email == "")
            return;

        lock (Users)
        {
            var foundUser = GetUser(user.Email);
            if (foundUser == null)
            {
                Users.Add(user);
            }
        }
    }

    public void EditUser(User user, string previousEmail)
    {
        var foundUser = GetUser(previousEmail);
        if (foundUser == null)
            return;

        lock (Users)
        {
            foundUser.Password = user.Password;
            foundUser.Email = user.Email;
            foundUser.Username = user.Username;
        }
    }
}