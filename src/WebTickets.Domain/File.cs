using CSharpFunctionalExtensions;

namespace WebTickets.Domain;

public record File
{
    private File(string pathToStorage)
    {
        PathToStorage = pathToStorage;
    }
    public string PathToStorage { get; }

    public static Result<File> Create(string pathToStorage)
    {
        if (string.IsNullOrWhiteSpace(pathToStorage))
        {
            return Result.Failure<File>("PathToStorage cannot be empty");
        }

        return new File(pathToStorage);
    }
    
}