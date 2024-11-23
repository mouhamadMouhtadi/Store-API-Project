﻿using Store.Data.Context;
using Store.Data.Entity;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using System.Collections;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreDbContext _context;
    private Hashtable _repositories;

    public UnitOfWork(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<int> CompletedAsync()
        => await _context.SaveChangesAsync();

    public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
    {
        if (_repositories == null)
        {
            _repositories = new Hashtable();
        }

        var entityKey = typeof(TEntity).Name;

        // Only create a new repository if it doesn't exist in the _repositories hashtable
        if (!_repositories.ContainsKey(entityKey))
        {
            var repositoryType = typeof(GenericRepository<,>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TKey)), _context);
            _repositories.Add(entityKey, repositoryInstance);
        }

        return (IGenericRepository<TEntity, TKey>)_repositories[entityKey];
    }
}
