# What Is It

This is a stripped down and edited version of the Nemerle language. 
The main aim is to be able to compile and run code (REPL + scripts) on Unity.

From original repo:

> Nemerle is a high-level statically-typed programming language for the .NET platform. It offers functional, object-oriented and imperative features. It has a simple C#-like syntax and a powerful meta-programming system.
> 
> Features that come from the functional land are variants, pattern matching, type inference and parameter polymorphism (aka generics). The meta-programming system allows great compiler extensibility, embedding domain specific languages, partial evaluation and aspect-oriented programming.
>
> To find out more, please visit: http://nemerle.org/

# Changes (WIP)

Not all of these are implemented yet

* Ignore visibility checks when compiling with a flag
* Minor QoL API changes to the compiler API (enable referencing assemblies already loaded in the CLR)
* Replace `System.Reflection.Emit` backend with Cecil
