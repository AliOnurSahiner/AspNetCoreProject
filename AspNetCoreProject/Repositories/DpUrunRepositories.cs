using AspNetCoreProject.Entites;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreProject.Repositories
{
    public class DpUrunRepositories
    {
        public List<Urun> GetirHepsi()
        {
            using var connection = new SqlConnection("Server=localhost ,1434;Database=AspNetCoreProject;User Id=SA;Password=Arya!1234;");
            
                return connection.GetAll<Urun>().ToList();
            

        }


    }
}
