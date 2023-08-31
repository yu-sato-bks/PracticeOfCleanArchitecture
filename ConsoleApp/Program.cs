// See https://aka.ms/new-console-template for more information
using ConsoleApp;
using Lib.Sync;
using UseCase;

Startup.Run();
var bus = ServiceProvider.GetService<UseCaseBus>();

Console.WriteLine("Welcome to practice of clean architecture.");
prompt:
Console.WriteLine("------------------------------------------");
Console.WriteLine("[C]reate user");
Console.WriteLine("[G]et users list");
Console.WriteLine("[Q]uit");
Console.WriteLine("------------------------------------------");
Console.WriteLine("type command(C/G/Q)");
Console.Write("> ");
var rawInput = Console.ReadLine();
var input = rawInput.ToLower();
switch (input)
{
    case "c":
        Console.WriteLine("type username");
        Console.Write("> ");
        var username = Console.ReadLine();
        var createRequest = new UserCreateRequest(username);
        bus.Handle(createRequest);
        goto prompt;
    case "g":
        var getListRequest = new UserGetListRequest();
        var list = bus.Handle(getListRequest);
        Console.WriteLine("| Id | Name |");
        Console.WriteLine("------------------------------------------");
        list.Summaries.ForEach(summary => {
            Console.WriteLine($"| {summary.Id} | {summary.UserName} |");
        });
        goto prompt;
    case "q":
        break;
    default:
        Console.WriteLine("not command");
        goto prompt;
}

