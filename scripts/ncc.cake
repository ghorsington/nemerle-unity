using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

public class NccOptions {
    public IEnumerable<string> References {get; set;} = new string[0];
    public IEnumerable<FilePath> Sources {get; set;} = new FilePath[0];
    public string Target {get; set;} = "exe";
    public FilePath Output {get; set;} = null;
    public IEnumerable<string> Arguments {get;set;} = new string[0];
    public IEnumerable<DirectoryPath> LibraryPaths {get;set;} = new DirectoryPath[0];
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

    foreach(var libPath in options.LibraryPaths)
        args.Add($"-library-path:{libPath.FullPath}");

    foreach(var source in options.Sources)
        args.Add(source.FullPath);

    var projSettings = new ProcessSettings {
        Arguments = string.Join(" ", args)
    };
    StartProcess(nccPath, projSettings);
}

public void NccNoStdlib(FilePath nccPath, NccOptions options) {
    Ncc(nccPath, new NccOptions {
        Target = options.Target,
        Output = options.Output,
        Sources = options.Sources,
        Arguments = options.Arguments.Concat(new string[] {"-nostdlib", "-greedy-"}),
        References = (new string[] {"mscorlib", "System", "System.Xml", "System.Linq", "System.Core"}).Concat(options.References)
    });
}