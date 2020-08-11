using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.Connection;

namespace WebApi1.Domains
{
    public class RepositoryConnection : ConnectionOptions
    {
        /// <summary>
        /// ctor
        /// </summary>
        public RepositoryConnection() { }


        /// <summary>
        /// ctor
        /// </summary>
        public RepositoryConnection(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        public RepositoryConnection(ConnectionOptions connection) : base(connection)
        {
        }
    }
}