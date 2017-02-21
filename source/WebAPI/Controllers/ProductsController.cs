using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using Cmc.Core.ComponentModel;
using Cmc.Core.Diagnostics;
using SimpleODataApiWithEf.Data;
using SimpleODataApiWithEf.Models;

namespace SimpleODataApiWithEf.Controllers
{
    public class ProductsController : ODataController
    {
        readonly ProductsContext _db = new ProductsContext();
        private ILogger _logger;

        public ProductsController()
        {
            _logger = ServiceLocator.Default.GetInstance<ILoggerFactory>().GetLogger(this);
        }

        [EnableQuery]
        //[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.)]
        public IQueryable<Product> Get()
        {
            using (new LogScope(_logger))
            {
                return _db.Products;
            }
        }

        [EnableQuery]
        public SingleResult<Product> Get([FromODataUri] int key)
        {
            using (new LogScope(_logger))
            {
                IQueryable<Product> result = _db.Products.Where(p => p.Id == key);
                return SingleResult.Create(result);
            }
        }

        public async Task<IHttpActionResult> Post(Product product)
        {
            using (new LogScope(_logger))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return Created(product);
            }
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Product> product)
        {
            using (new LogScope(_logger))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = await _db.Products.FindAsync(key);
                if (entity == null)
                {
                    return NotFound();
                }
                product.Patch(entity);
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(key))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return Updated(entity);
            }
        }
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Product update)
        {
            using (new LogScope(_logger))
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (key != update.Id)
                {
                    return BadRequest();
                }
                _db.Entry(update).State = EntityState.Modified;
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(key))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return Updated(update);
            }
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            using (new LogScope(_logger))
            {
                var product = await _db.Products.FindAsync(key);
                if (product == null)
                {
                    return NotFound();
                }
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        private bool ProductExists(int key)
        {
            return _db.Products.Any(p => p.Id == key);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}