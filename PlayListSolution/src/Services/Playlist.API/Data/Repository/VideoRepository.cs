using Microsoft.EntityFrameworkCore;
using Playlist.API.Data.Context;
using Playlist.API.Domain.Interfaces.Repository;
using Playlist.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlist.API.Data.Repository
{
    public class VideoRepository : BaseRepository<Video>, IVideoRepository
    {
        private readonly PlayListDbContext _db;

        public VideoRepository(PlayListDbContext db) 
            : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Video>> BuscarTodos(bool visualizado)
        {
            return await _db.Set<Video>()
                .Where(e => e.Visualizado == visualizado).ToListAsync();
        }

        public void DetachLocal(Func<Video, bool> precicate)
        {
            var local = _db.Set<Video>().Local.Where(precicate).FirstOrDefault();
            if (local != null)
            {
                _db.Entry(local).State = EntityState.Detached;
            }
        }
    }
}