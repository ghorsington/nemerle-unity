# Change roadmap

## Implement `Assembly` referencing

Currently `ManagerClass` doesn't allow to easily add new `Assembly` references.
As such, expose `LibraryManager` to public to allow calls to `AddAssembly`.
In addition, implement `LoadAssembly` to load assemblies as `SR.Assembly`.

## Implement visibility ignoring

Add `Options.IgnoreAccessChecks` to allow compilation without checking 
for accessibility. This will produce assemblies that then can be loaded as 
corlibs into mono.

**Current status**: Can emit assemblies, but has some warnings about 
multiple type definitions.
TODO: Remove those warnings.

## Port SRE generation code to Cecil

Currently SRE is used for code generation. However, IL gen code is so well 
designed that it can be fairly easily converted to use Cecil.

This requires removal of all references to SRE. All `Type`s used in codegen
will be replaces with `TypeReference` and required stuff will be autoimported.