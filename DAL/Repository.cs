using System;
using ProductDb;

namespace DAL
{
    public class Repo
    {
        private readonly ProductDbContext _context;

        internal ProductDbContext Context 
        {
            get 
            {
                return _context;
            }
        }

        public Repo(ProductDbContext context)
        {
            _context = context;
        }
    }
}