using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiXYZServices.Interfaces
{
    public interface IHubClient
    {
        Task BroadcastMessage();
        Task BroadcastMessage(object message);
    }
}
