using System.Collections.Concurrent;
using TestDevelopment.Models;

namespace TestDevelopment.DataService
{
    public class SharedDb
    {
        private readonly ConcurrentDictionary<string, UserConnectionModel> _connections=new ConcurrentDictionary<string, UserConnectionModel>();
        public ConcurrentDictionary<string, UserConnectionModel> connections => _connections;
    }
}
