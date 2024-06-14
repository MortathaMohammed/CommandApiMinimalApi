using AutoMapper;
using MinimalApi.Dtos;
using MinimalApi.Models;

namespace MinimalApi.Profiles;
public class CommandPofile : Profile
{
    public CommandPofile()
    {
        CreateMap<Command, CommandReadDto>();
        CreateMap<CommandCreateDto, Command>();
        CreateMap<CommandUpdateDto, Command>();
    }
}