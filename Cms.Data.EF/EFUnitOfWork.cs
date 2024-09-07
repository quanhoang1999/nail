using Cms.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cms.Data.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private AppDbContextDefault _context;
        private IDbTransaction _transaction;
        private IDbConnection _connection;
        private bool _disposed;
        public EFUnitOfWork(AppDbContextDefault context)
        {
            _context = context;
            //_connection.Open();
            // _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                //_transaction.Commit();
                _context.SaveChanges();
            }
            catch
            {
                //_transaction.Rollback();
                Rollback();
                throw;
            }
            finally
            {
                //_transaction.Dispose();
                //_transaction = _connection.BeginTransaction();
                //resetRepositories();
            }
            _context.SaveChanges();
        }
        public void Rollback()
        {
            _context
                .ChangeTracker
                .Entries()
                .ToList()
                .ForEach(x => x.Reload());
        }
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }
        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }
        ~EFUnitOfWork()
        {
            dispose(false);
        }
    }
}
