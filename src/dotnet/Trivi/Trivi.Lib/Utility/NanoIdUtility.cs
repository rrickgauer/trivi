using NanoidDotNet;
using Trivi.Lib.Domain.Constants;
using Trivi.Lib.Domain.Contracts;

namespace Trivi.Lib.Utility;

public class NanoIdUtility
{
    public static string New()
    {
        return Nanoid.Generate(alphabet: NanoIdConstants.Alphabet, size: NanoIdConstants.IdLength);
    }

    public static void GenerateNewOnes(int number, QuestionType type)
    {
        for(int i = 0; i < number; i++)
        {
            Console.WriteLine($"{type.GetNewNanoId()}");
        }
    }

    public static string BuildNanoId<T>() where T : INanoIdPrefix
    {
        var prefix = T.NanoIdPrefix;

        var id = New();

        return $"{prefix}_{id}";
    }


}
