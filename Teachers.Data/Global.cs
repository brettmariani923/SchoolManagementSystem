using System.Runtime.CompilerServices;

// This allows the Test project to see classes that are internal so that we can test them without exposing them to their dependencies.
[assembly: InternalsVisibleTo("LibraryManagement.Data.Tests")]