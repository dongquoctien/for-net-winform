using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Welcome to the dt app. Use 'dt --help' for usage information.");
            return;
        }

        switch (args[0])
        {
            case "--help":
                ShowHelp();
                break;
            case "generate":
                if (args.Length < 3)
                {
                    Console.WriteLine("Invalid command. Use 'dt --help' for usage information.");
                    return;
                }
                Generate(args[1], args[2]);
                break;
            default:
                Console.WriteLine("Unknown command. Use 'dt --help' for usage information.");
                break;
        }
    }

    static void ShowHelp()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  dt --help                    Show this help menu");
        Console.WriteLine("  dt generate <schematic> [name]  Generate a schematic");
        Console.WriteLine("\nSchematics:");
        Console.WriteLine("  module");
        Console.WriteLine("  service");
        Console.WriteLine("  model");
        Console.WriteLine("  controller");
        Console.WriteLine("  view");
    }

    static void Generate(string schematic, string name)
    {
        List<string> validSchematics = new List<string> { "module", "service", "model", "controller", "view" };

        if (!validSchematics.Contains(schematic.ToLower()))
        {
            Console.WriteLine($"Invalid schematic: {schematic}");
            Console.WriteLine("Valid schematics are: module, service, model, controller, view");
            return;
        }

        Console.WriteLine($"Generating {schematic}: {name}");
        // Add your generation logic here
    }
}