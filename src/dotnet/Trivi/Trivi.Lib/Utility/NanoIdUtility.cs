using NanoidDotNet;
using Trivi.Lib.Domain.Constants;
using Trivi.Lib.Domain.Contracts;

namespace Trivi.Lib.Utility;

public class NanoIdUtility
{
    public static string NewQuestionId()
    {
        return Nanoid.Generate(alphabet: NanoIdConstants.QuestionAlphabet, size: NanoIdConstants.QuestionIdLength);
    }

    public static void GenerateNewQuestionIds(int number, QuestionType type)
    {
        for(int i = 0; i < number; i++)
        {
            Console.WriteLine($"{type.GetNewQuestionId()}");
        }
    }

    public static string BuildNanoId<T>() where T : INanoIdPrefix
    {
        var prefix = T.NanoIdPrefix;

        var id = NewQuestionId();

        return $"{prefix}_{id}";
    }



    public static void GenerateNewGameIds(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Console.WriteLine(NewGameId());
        }
    }

    public static string NewGameId()
    {
        return Nanoid.Generate(alphabet: NanoIdConstants.GameAlphabet, size: NanoIdConstants.GameIdLength);
    }

}
