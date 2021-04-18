using Canducci.Pagination;
using Microsoft.EntityFrameworkCore;
using Playlist.API.Data.Context;
using Playlist.API.Domain.Interfaces.Repository;
using Playlist.API.Domain.Models;
using System;
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

        public async Task<PaginatedRest<Video>> BuscarTodosPaginado(int? pageNumber, int? pageSize, bool visualizado)
        {
            return await _db.Set<Video>()
                .AsNoTracking()
                .Where(e => e.Visualizado == visualizado)
                .OrderBy(e => e.DataCadastro)
                .ToPaginatedRestAsync(pageNumber.Value, pageSize.Value);
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