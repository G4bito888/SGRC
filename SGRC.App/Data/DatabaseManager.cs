using Oracle.ManagedDataAccess.Client;
using Neo4j.Driver;

namespace SGRC.App.Data
{
    public class DatabaseManager
    {
        // El connection string de Oracle (Ajústalo con tus credenciales)
        private string _oracleConn = "User Id=admin;Password=admin;Data Source=localhost:1521/xe;";
        
        private readonly IDriver _neo4jDriver;

        public DatabaseManager()
        {
            _neo4jDriver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "password"));
        }
    }
}