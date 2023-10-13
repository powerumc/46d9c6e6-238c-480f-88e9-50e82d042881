// See https://aka.ms/new-console-template for more information

using Diff.Cli;

try
{
    var app = new App();
    app.Run(Environment.GetCommandLineArgs());
}
catch (Exception e)
{
    Console.Error.WriteLine(e.Message);
    Environment.Exit(-1);
}
