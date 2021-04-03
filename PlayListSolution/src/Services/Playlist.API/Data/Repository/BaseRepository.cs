using Microsoft.EntityFrameworkCore;
using Playlist.API.Data.Context;
using Playlist.API.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playlist.API.Data.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly PlayListDbContext _db;

        public BaseRepository(PlayListDbContext playListDbContext)
        {
            _db = playListDbContext;
        }

        public void Atualizar(TEntity e)
        {
            _db.Entry(e).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public async Task<TEntity> BuscarPorId(Guid id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> BuscarTodos()
        {
            return await _db.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async void Excluir(TEntity e)
        {
            _db.Set<TEntity>().Remove(e);
            await _db.SaveChangesAsync();
        }

        public void Inserir(TEntity e)
        {
             _db.Set<TEntity>().Add(e);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}