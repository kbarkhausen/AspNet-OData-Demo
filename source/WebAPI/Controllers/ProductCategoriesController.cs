using System.Linq;
using System.Web.Http;
using System.Web.OData;
using Cmc.Core.ComponentModel;
using Cmc.Core.Diagnostics;
using SimpleODataApiWithEf.Data;
using SimpleODataApiWithEf.Models;

namespace SimpleODataApiWithEf.Controllers
{
    public class ProductCategoriesController : ODataController
    {
        readonly ProductsContext _db = new ProductsContext();
        private ILogger _logger;

        public ProductCategoriesController()
        {
            _logger = ServiceLocator.Default.GetInstance<ILoggerFactory>().GetLogger(this);
        }

        [EnableQuery]
        public IQueryable<ProductCategory> Get()
        {
            using (new LogScope(_logger))
            {
                return _db.ProductCategories;
            }
        }

        [EnableQuery]
        public SingleResult<ProductCategory> Get([FromODataUri] int key)
        {
            using (new LogScope(_logger))
            {
                IQueryable<ProductCategory> result = _db.ProductCategories.Where(p => p.Id == key);
                return SingleResult.Create(result);
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}