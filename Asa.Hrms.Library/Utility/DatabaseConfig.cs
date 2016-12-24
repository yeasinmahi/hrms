using System;
using System.Collections.Generic;
using GITS.Hrms.Library.Data.Entity;

namespace GITS.Hrms.Library.Utility
{
    [Serializable()]
    public class DatabaseConfig : List<DatabaseConfiguration>
    {
        private String _Location;
        private String _Version;
        private DatabaseConfiguration _Installation;
        private DatabaseConfiguration _DatabaseConfiguration;

        public String Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public String Version
        {
            get { return _Version; }
            set { _Version = value; }
        }

        public DatabaseConfiguration Installation
        {
            get { return _Installation; }
            set { _Installation = value; }
        }

        public DatabaseConfiguration DatabaseConfiguration
        {
            get { return _DatabaseConfiguration; }
            set { _DatabaseConfiguration = value; }
        }
    }
}
