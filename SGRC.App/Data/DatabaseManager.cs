using Oracle.ManagedDataAccess.Client;
using Neo4j.Driver;

namespace SGRC.App.Data
{
    public class DatabaseManager
    {
        private string _oracleConn = "User Id=${NEO4J_USER};Password=${DB_PASSWORD};Data Source=localhost:1521/xe;";
        
        private readonly IDriver _neo4jDriver;

        public DatabaseManager()
        {
            _neo4jDriver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("${NEO4J_USER}", "${DB_PASSWORD}"));
        }
    }
}