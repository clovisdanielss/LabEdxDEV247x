using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;

namespace FirstLab.Models
{
    /// Usado para brincar com banco de dados.
    public class SakilaDbContext : DbContext
    {
        public SakilaDbContext(DbContextOptions<SakilaDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Product {get; set;}


    }

    public class SakilaDbContextFactory { 
        public static SakilaDbContext Create(string connection){
            var optionsBuilder = new DbContextOptionsBuilder<SakilaDbContext>();
            optionsBuilder.UseMySQL(connection);
            var sakilaDbContext = new SakilaDbContext(optionsBuilder.Options);
            return sakilaDbContext;
        }
    }
    /// Usado para brincar com alguns testes.
    public class Repository
    {
        public static IDictionary<int, Product> Products;
        static Repository()
        {
            Products = new Dictionary<int, Product>();
            Products.Add(0, new Product { id = 0, name = "Computador", value = 1900.5f });
            Products.Add(1, new Product { id = 1, name = "Televisão", value = 1200f });
            Products.Add(2, new Product { id = 2, name = "Carro", value = 127000f });
        }

        public static Product[] GetProductsFiltredByValue(int min, int max)
        {
            if (max < min)
            {
                max = int.MaxValue;
            }
            List<Product> list = new List<Product>();
            foreach (Product p in Products.Values)
            {
                if (p.value >= min && p.value <= max)
                {
                    list.Add(p);
                }
            }
            return list.ToArray();
        }

        public static void AddProduct(Product product)
        {
            int max = 0;
            foreach (var key in Products.Keys)
            {
                if (key > max)
                {
                    max = key;
                }
            }
            product.id = max + 1;
            Products.Add(product.id, product);
        }

        public static Product[] GetProducts()
        {
            Product[] array = new Product[Products.Count];
            Products.Values.CopyTo(array, 0);
            return array;
        }

        public static Product GetProduct(int id)
        {
            if (!Products.ContainsKey(id))
            {
                return null;
            }
            Product product = Products[id];
            return product;
        }

        public static void DeleteProduct(int id)
        {
            Products.Remove(id);
        }
    }
}