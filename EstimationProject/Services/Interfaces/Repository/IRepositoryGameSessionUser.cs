using Domain;

namespace Services.Interfaces.Repository;

public interface IRepositoryGameSessionUser
{
    public List<GameSessionUser> GetUsers();
    public GameSessionUser? GetUser(string email);
    public void RemoveUser(string email);
    public void RemoveUser(GameSessionUser sessionUser);
    public void AddUser(GameSessionUser sessionUser);
    
    // InMemory, there is no need to edit
    // public void EditUser(GameSessionUser sessionUser, string previousEmail); 
}