using Microsoft.EntityFrameworkCore;
using Playlist.API.Data.Context;
using Playlist.API.Domain.Interfaces.Repository;
using Playlist.API.Domain.Models;
using System;
using System.Linq;

namespace Playlist.API.Data.Repository
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        private readonly PlayListDbContext _db;

        public CategoriaRepository(PlayListDbContext db)
            : base(db)
        {
            _db = db;
        }

        public void DetachLocal(Func<Categoria, bool> precicate)
        {
            var local = _db.Set<Categoria>().Local.Where(precicate).FirstOrDefault();
            if (local != null)
            {
                _db.Entry(local).State = EntityState.Detached;
            }
        }
    }
}