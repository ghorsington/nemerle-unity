#l "scripts/ncc.cake"

var target = Argument("target", "Default");

Task("CleanDirs")
    .Does(() => 
{
    CreateDirectory("bin");
    CleanDirectory("bin");
});

Task("BuildLib")
    .Does(() => 
{
    Ncc("boot/ncc.exe", new NccOptions {
        Target = "library",
        Output = "bin/Nemerle.dll",
        Sources = GetFiles("lib/*.n"),
        Arguments = new string[] {"-nostdlib", "-nowarn:618", "-greedy-"},
        References = new string[] {"mscorlib", "System", "System.Xml"}
    });
});

Task("BuildMacros")
    .Does(() => 
{
    NccNoStdlib("boot/ncc.exe", new NccOptions {
        Target = "library",
        Output = "bin/Nemerle.Macros.dll",
        Sources = GetFiles("macros/*.n"),
        References = new string[] {"bin/Nemerle.dll", "bin/Nemerle.Compiler.dll", "System.Data", "System.Windows.Forms", "System.Xml.Linq"}
    });
});

Task("BuildCompiler")
    .Does(() => 
{
    NccNoStdlib("boot/ncc.exe", new NccOptions {
        Target = "library",
        Output = "bin/Nemerle.Compiler.dll",
        Sources = GetFiles("ncc/**/*.n"),
        References = new string[] {"bin/Nemerle.dll"}
    });
});

Task("BuildNcc")
    .Does(() => 
{
    NccNoStdlib("boot/ncc.exe", new NccOptions {
        Target = "exe",
        Output = "bin/ncc.exe",
        Sources = GetFiles("ncc/main.n"),
        References = new string[] {"bin/Nemerle.dll", "bin/Nemerle.Compiler.dll"}
    });
});

Task("BuildEvaluator")
    .Does(() => 
{
    Ncc("bin/ncc.exe", new NccOptions {
        Target = "library",
        Output = "bin/Nemerle.Evaluation.dll",
        Sources = new FilePath[] { "other_tools/nemerlish/eval.n" },
        References = new string[] {"Nemerle.Compiler.dll"}
    });
});

Task("BuildNemish")
    .IsDependentOn("BuildEvaluator")
    .Does(() => 
{
    Ncc("bin/ncc.exe", new NccOptions {
        Target = "exe",
        Output = "bin/nemish.exe",
        Sources = new FilePath[] { "other_tools/nemerlish/main.n", "other_tools/nemerlish/interp.n", "other_tools/nemerlish/readline.n" },
        References = new string[] {"Nemerle.Compiler.dll", "Nemerle.Evaluation.dll"}
    });
});

Task("Default")
    .IsDependentOn("CleanDirs")
    .IsDependentOn("BuildLib")
    .IsDependentOn("BuildCompiler")
    .IsDependentOn("BuildMacros")
    .IsDependentOn("BuildNcc");

RunTarget(target);