using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace Innlevering01
{
    public class DatabaseHandler
    {
        public Table<tile> TileTable { get; private set; }

        public DataContext DataContxt { get; private set; }

        public DatabaseHandler()
        {
            // DataContext takes a connection string - gotta make this one dynamic
            DataContxt = new DataContext("Data Source=MOTHERSHIP;Initial Catalog=Database;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            // Get a typed table to run queries.
            TileTable = DataContxt.GetTable<tile>();
        }

    }
}
