using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LithiumNukerV2;


namespace LithiumNukerV2
{
    public class Exceptions
    {
        public class ServerRateLimited : Exception
        {
            public ServerRateLimited(string message)
            {

            }
        }
    }
}
