using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FirstLab.Models;
namespace FirstLab.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {

        private SakilaDbContext dbContext;

        public ProductController(){
            string connectionString = "server=localhost;port=3306;database=sakila;userid=root;pwd=meunome;sslmode=none";
            dbContext = SakilaDbContextFactory.Create(connectionString);
        }

        [HttpGet]
        public ActionResult Get([FromQuery] int min, [FromQuery] int max)
        {
            if(min == max && min == 0){
                return Ok(dbContext.Product.ToArray());
            }
            if(max < min){
                max = int.MaxValue;
            }
            return Ok(dbContext.Product.Where( product => product.value > min && product.value < max));
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Product p = dbContext.Product.Find(id);
            if (p == null)
            {
                return NotFound(p);
            }
            return Ok(p);
        }


        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            dbContext.Product.Add(product);
            dbContext.SaveChanges();

            return Created($"api/products/{product.id}", product);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Product p = dbContext.Product.Find(id);
            dbContext.Product.Remove(p);
            dbContext.SaveChanges();
        }
    }
}
