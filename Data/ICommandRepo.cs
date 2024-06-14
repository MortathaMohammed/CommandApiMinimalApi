using MinimalApi.Models;

namespace MinimalApi.Data;

public interface ICommandRepo
{
    Task<Command?> GetCommand(int id);
    Task<IEnumerable<Command>> GetCommands();
    void UpdateCommand(Command command);
    Task CreateCommand(Command command);
    void DeleteCommand(Command command);
    Task SaveCommand();
}