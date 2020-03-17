using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

var target = Argument("target", "Default");


public class NccOptions {
    public IEnumerable<string> References {get; set;} = new string[0];
    public IEnumerable<FilePath> Sources {get; set;} = new FilePath[0];
    public string Target {get; set;} = "exe";
    public FilePath Output {get; set;} = null;
    public IEnumerable<string> Arguments {get;set;} = new string[0];
}

public void Ncc(FilePath nccPath, NccOptions options) {
    var args = new List<string>();

    args.Add($"-target:{options.Target}");

    foreach(var arg in options.Arguments)
        args.Add(arg);

    if(options.Output != null)
        args.Add($"-out:{options.Output.FullPath}");
    
    foreach(var reference in options.References)
        args.Add($"-ref:{reference}");

    foreach(var source in options.Sources)
        args.Add(source.FullPath);

    var projSettings = new ProcessSettings {
        Arguments = string.Join(" ", args)
    };
    StartProcess(nccPath, projSettings);
}

Task("Default")
  .Does(() =>
{
    CreateDirectory("bin");
    CleanDirectory("bin");

    CreateDirectory("bin/boot");
    CopyDirectory("boot", "bin/boot");

    CreateDirectory("bin/stage1");
    Ncc("bin/boot/ncc.exe", new NccOptions {
        Target = "library",
        Output = "bin/stage1/Nemerle.Macros.dll",
        Sources = GetFiles("lib/*.n"),
        Arguments = new string[] {"-nostdlib", "-nowarn:618"},
        References = new string[] {"mscorlib.dll", "System.dll", "System.Xml.dll"}
    });
});

RunTarget(target);