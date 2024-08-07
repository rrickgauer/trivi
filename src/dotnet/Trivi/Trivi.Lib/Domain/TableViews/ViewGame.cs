using System.Text.Json.Serialization;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Models;

namespace Trivi.Lib.Domain.TableViews;

public class ViewGame
{

    [SqlColumn("game_id")]
    [CopyToProperty<Game>(nameof(Game.Id))]
    public string? Id { get; set; }

    [SqlColumn("game_collection_id")]
    [CopyToProperty<Game>(nameof(Game.CollectionId))]
    public Guid? CollectionId { get; set; }

    [SqlColumn("game_status_id")]
    [CopyToProperty<Game>(nameof(Game.Status))]
    public GameStatus Status { get; set; } = GameStatus.Open;

    [SqlColumn("game_randomize_questions")]
    [CopyToProperty<Game>(nameof(Game.RandomizeQuestions))]
    public bool RandomizeQuestions { get; set; } = false;

    [SqlColumn("game_question_time_limit")]
    [CopyToProperty<Game>(nameof(Game.QuestionTimeLimit))]
    public ushort? QuestionTimeLimit { get; set; }

    [SqlColumn("game_created_on")]
    [CopyToProperty<Game>(nameof(Game.CreatedOn))]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    [SqlColumn("game_started_on")]
    [CopyToProperty<Game>(nameof(Game.StartedOn))]
    public DateTime? StartedOn { get; set; }

    [SqlColumn("game_collection_user_id")]
    public Guid? UserId { get; set; }

    [SqlColumn("active_question_id")]
    public string? ActiveQuestionId { get; set; }



    [JsonIgnore]
    public List<ViewGameQuestion> Questions { get; set; } = new();
}


