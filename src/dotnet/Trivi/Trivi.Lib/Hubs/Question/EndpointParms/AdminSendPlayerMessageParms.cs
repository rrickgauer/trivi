namespace Trivi.Lib.Hubs.Question.EndpointParms;


public class AdminSendPlayerMessageParms : AdminSendAllPlayersMessageParms
{
    public required Guid PlayerId { get; set; }
}