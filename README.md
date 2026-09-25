# Zerionix
Zerionix is a Roslyn semantic static analysis engine build for .NET comparing logic of the code to business/domain rules.
This is currently a work in progress and has a simple proof of concept and is not currently production ready.

## Current features supported
- Value assignment supporting SimpleAssignment ops for local/property references for unary op values
- Runtime evaluation/compiling of bool expression logic for example the rule A > 0 will mean if A = -1 this rule is violated
- If statement logic, nesting, else if and else statements supported, complex logic eg using method for if statement is not supported as of currently.

## Goal of the project
Help .NET developers debugging large complex projects by parsing SQL logic, LINQ logic, NoSQL logic, ternary conditions, switch statements and more. 

## Contribution
- If you have a feature in mind you'd want to see create a issue
- If you want to implement some features create a PR

## Licensing
- This is licensed under MIT
