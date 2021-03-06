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

        public virtual async Task Atualizar(TEntity e)
        {
            _db.Entry(e).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public virtual async Task<TEntity> BuscarPorId(Guid id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> BuscarTodos()
        {
            return await _db.Set<TEntity>().ToListAsync();
        }

        public virtual async Task Excluir(TEntity e)
        {
            _db.Set<TEntity>().Remove(e);
            await _db.SaveChangesAsync();
        }

        public virtual async Task Inserir(TEntity e)
        {
            await _db.Set<TEntity>().AddAsync(e);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}