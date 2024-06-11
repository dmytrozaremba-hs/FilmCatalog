using System.Linq;

namespace FilmCatalog.Application.Common.Models;

public class Result
{
    internal Result(bool succeeded, IDictionary<string, string[]> errors)
    {
        Succeeded = succeeded;
        Errors = errors switch
        {
            Dictionary<string, string[]> d => d,
            not null => new Dictionary<string, string[]>(errors),
            _ => new Dictionary<string, string[]>()
        };
    }

    public bool Succeeded { get; set; }

    public Dictionary<string, string[]> Errors { get; set; }

    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        Dictionary<string, string[]> errorsDictionary = new();
        if (errors != null && errors.Any())
        {
            errorsDictionary[string.Empty] = errors.ToArray();
        }
        return Failure(errorsDictionary);
    }

    public static Result Failure(IEnumerable<(string key, string message)> errors)
    {
        Dictionary<string, string[]> errorsDictionary = errors switch
        {
            not null => errors.GroupBy(x => x.key, x => x.message, (k, messages) => (k, v: messages.ToArray())).ToDictionary(kv => kv.k, kv => kv.v),
            _ => new Dictionary<string, string[]>()
        };
        return Failure(errorsDictionary);
    }

    public static Result Failure(IDictionary<string, string[]> errors)
    {
        return new Result(false, errors);
    }
}
