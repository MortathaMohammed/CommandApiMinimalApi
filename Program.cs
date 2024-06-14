using System.Windows.Input;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Data;
using MinimalApi.Dtos;
using MinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICommandRepo, CommandRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("dbConnection"))
);

var app = builder.Build();

app.UseHttpsRedirection();

// Get all commands
app.MapGet("api/commands", async (ICommandRepo repo, IMapper mapper) =>
{
    var commands = await repo.GetCommands();
    return Results.Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
});

// Get command
app.MapGet("api/commands/{id}", async (int id, ICommandRepo repo, IMapper mapper) =>
{
    var command = await repo.GetCommand(id);
    if (command != null)
        return Results.Ok(mapper.Map<CommandReadDto>(command));

    return Results.NotFound();
});

// Create command
app.MapPost("api/commands/add", async (ICommandRepo repo, IMapper mapper, CommandCreateDto commandCreateDto) =>
{
    if (commandCreateDto == null)
        return Results.BadRequest();

    var createCommand = mapper.Map<Command>(commandCreateDto);

    // Change the creat and save commands to async in the next time
    await repo.CreateCommand(createCommand);
    await repo.SaveCommand();

    var commandReadDto = mapper.Map<Command>(createCommand);
    return Results.Created($"api/commands/{commandReadDto.Id}", commandReadDto);
});

// Update command
app.MapPut("api/commands/update/{id}", async (ICommandRepo repo, IMapper mapper, CommandUpdateDto commandUpdateDto, int id) =>
{
    if (commandUpdateDto == null)
        return Results.BadRequest();

    var command = await repo.GetCommand(id);
    if (command == null)
        return Results.NotFound();

    mapper.Map(commandUpdateDto, command);

    repo.UpdateCommand(command);
    await repo.SaveCommand();

    var commandReadDto = mapper.Map<Command>(command);
    return Results.Created($"api/commands/{command.Id}", command);
});

// Delete command
app.MapDelete("api/commands/delete/{id}", async (ICommandRepo repo, IMapper mapper, int id) =>
{
    var command = await repo.GetCommand(id);
    if (command == null)
        return Results.NotFound();

    repo.DeleteCommand(command);
    await repo.SaveCommand();

    return Results.Ok();
});


app.Run();
