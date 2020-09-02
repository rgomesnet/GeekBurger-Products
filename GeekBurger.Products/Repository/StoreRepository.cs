using GeekBurger.Products.Contract;
using GeekBurger.Products.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeekBurger.Products.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private ProductsDbContext _context { get; set; }

        public StoreRepository(ProductsDbContext context)
        {
            _context = context;
        }

        public Store GetStoreByName(string storeName)
        {
            return _context.Stores.FirstOrDefault(store => store.Name.Equals(storeName, StringComparison.InvariantCultureIgnoreCase));   
        }
    }
}
