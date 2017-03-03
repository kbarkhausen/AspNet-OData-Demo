using System.Linq;
using System.Web.Http;
using System.Web.OData;
using SimpleODataApiWithEf.Data;
using SimpleODataApiWithEf.Models;

namespace SimpleODataApiWithEf.Controllers
{
    public class ProductCategoriesController : ODataController
    {
        readonly ProductsContext _db = new ProductsContext();

        public ProductCategoriesController()
        {
        }

        [EnableQuery]
        public IQueryable<ProductCategory> Get()
        {
                return _db.ProductCategories;
        }

        [EnableQuery]
        public SingleResult<ProductCategory> Get([FromODataUri] int key)
        {
                IQueryable<ProductCategory> result = _db.ProductCategories.Where(p => p.Id == key);
                return SingleResult.Create(result);
        }
        
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}