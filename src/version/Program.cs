using System;
using System.Linq;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace version
{
    class Program
    {
        static int Main(string[] args)
        {
            // Create a root command with some options
            var rootCommand = new RootCommand
            {
                new Option<string>(
                    "--action",
                    description: "Action to take (verify/up/reset)"),
                new Option<string>(
                    "--version",
                    "Version to take the action on"),
                new Option<string>(
                    "--versiontype",
                    getDefaultValue: () => "build",
                    "Part of the version number to update(major.minor.build.revision)")
            };
            rootCommand.Description = "Version number modifier";

            // Note that the parameters of the handler method are matched according to the names of the options
            rootCommand.Handler = CommandHandler.Create<string, string, string>((action, version, versiontype) =>
            {
                DoAction(action,version,versiontype);
            });

            // Parse the incoming args and invoke the handler
            return rootCommand.InvokeAsync(args).Result;
        }

        private static void DoAction(string action, string version, string versiontype) {
            if(string.IsNullOrEmpty(action)) {
                Console.Error.WriteLine("action is a required");
                Environment.Exit(1);
            }
            if(string.IsNullOrEmpty(version)) {
                Console.Error.WriteLine("Version is required");
                Environment.Exit(1);
            }

            Version result = null;
            switch(action.ToLower()) {
                case "verify":
                    new VersionModification().Verify(version);
                    Console.WriteLine("OK");
                    break;
                case "up":
                    result = new VersionModification().Up(version, versiontype);
                    break;
                case "down":
                    result = new VersionModification().Down(version, versiontype);
                    break;
                case "reset":
                    result = new VersionModification().Reset(version, versiontype);
                    break;
            }
            if(result != null) {
                Console.WriteLine(result.ToString(version.Count(c => c == '.') + 1));
            }
        }       
    }
}