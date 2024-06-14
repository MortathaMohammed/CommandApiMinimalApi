using Microsoft.EntityFrameworkCore;
using MinimalApi.Models;

namespace MinimalApi.Data;

public class CommandRepo : ICommandRepo
{
    private readonly AppDbContext _db;

    public CommandRepo(AppDbContext db)
    {
        _db = db;
    }

    public async Task CreateCommand(Command command)
    {
        ArgumentNullException.ThrowIfNull(command);
        await _db.Commands.AddAsync(command);
    }

    public void DeleteCommand(Command command)
    {
        ArgumentNullException.ThrowIfNull(command);
        _db.Commands.Remove(command);
    }

    public async Task<Command?> GetCommand(int id)
    {
        return await _db.Commands.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Command>> GetCommands()
    {
        return await _db.Commands.ToListAsync();
    }

    public async Task SaveCommand()
    {
        await _db.SaveChangesAsync();
    }

    public void UpdateCommand(Command command)
    {
        ArgumentNullException.ThrowIfNull(command);
        _db.Commands.Update(command);
    }
}
