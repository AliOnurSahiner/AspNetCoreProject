using AspNetCoreProject.Contexts;
using AspNetCoreProject.Entites;
using AspNetCoreProject.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreProject.Repositories
{
    public class KategoriRepository:GenericRepository<Kategori>,IKategoriRepository
    {
       
    }
}
