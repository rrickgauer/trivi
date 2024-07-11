using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Trivi.Lib.Domain.Constants;

namespace Trivi.Lib.Domain.Other;

public class SessionManager(ISession session)
{
    private readonly ISession _session = session;

    public Guid? ClientId
    {
        get
        {
            var data = _session.GetString(GuiSessionKeys.AuthClientId);

            if (Guid.TryParse(data, out var clientId))
            {
                return clientId;
            }

            return null;
        }
    }

    public void StoreClientId(Guid? clientId)
    {
        SetSessionValue(GuiSessionKeys.AuthClientId, clientId);
    }


    private void SetSessionValue(string key, object? value)
    {
        if (value != null)
        {
            _session.SetString(key, $"{value}");
        }
        else
        {
            _session.Remove(key);
        }
    }



}
