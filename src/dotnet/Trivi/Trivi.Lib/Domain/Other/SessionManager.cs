using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Trivi.Lib.Domain.Constants;

namespace Trivi.Lib.Domain.Other;

public class SessionManager(ISession session)
{

    public static Guid? TESTING_MASTER_USER_ID = null;


    private readonly ISession _session = session;

    public Guid? ClientId
    {
        get
        {
            if (TESTING_MASTER_USER_ID is Guid masterUserId)
            {
                return masterUserId;
            }

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

    public void ClearClientId()
    {
        StoreClientId(null);
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
