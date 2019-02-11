using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Store
{
    public class SqliteStore : IAppendOnlyStore
    {

        //https://docs.microsoft.com/en-us/ef/core/get-started/netcore/new-db-sqlite

        public SqliteStore()
        {
            
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Append(string streamName, byte[] data, long expectedStreamVersion = -1)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataWithVersion> ReadRecords(string streamName, long afterVersion, int maxCount)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataWithName> ReadRecords(long afterVersion, int maxCount)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}
