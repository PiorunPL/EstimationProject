using Domain;

namespace Services.Interfaces.Repository;

public interface IRepositoryUser
{
    public User? GetUser(string email);
    public void RemoveUser(string email);
    public void RemoveUser(User user);
    public void AddUser(User user);
    public void EditUser(User user, string previousEmail);
}