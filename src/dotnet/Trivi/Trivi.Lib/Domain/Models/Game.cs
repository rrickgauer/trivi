using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Utility;

namespace Trivi.Lib.Domain.Models;

public class Game
{
    public string? Id { get; set; }
    public Guid? CollectionId { get; set; }
    public GameStatus Status { get; set; } = GameStatus.Open;
    public bool RandomizeQuestions { get; set; } = false;
    public ushort? QuestionTimeLimit { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime? StartedOn { get; set; }

    public static Game FromNewGameForm(NewGameForm form)
    {
        Game result = new()
        {
            Id = NanoIdUtility.NewGameId(),
            CollectionId = form.CollectionId,
            RandomizeQuestions = form.RandomizeQuestions,
            QuestionTimeLimit = form.QuestionTimeLimit,
            CreatedOn = DateTime.UtcNow,
        };

        return result;
    }
}
