using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
    public class BuggyController : APIBaseController
    {
        private readonly StoreContext _dbcontext;

        public BuggyController(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("NotFound")]
        // /api/Buggy/NotFound
        public ActionResult GetnotFoundRequest()
        {
            var product = _dbcontext.Products.Find(100);
            if(product is null ) 
                return NotFound( new ApiResponce(404));
            return Ok(product);
        }

        [HttpGet("ServerError")]
        public ActionResult GetServerError() 
        {
            var product = _dbcontext.Products.Find(100);
            var productreturn = product.ToString(); //error null to string (Null Reference Exeption)
            return Ok(productreturn);
        }

        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest() 
        { 
            return BadRequest();
        }    

        [HttpGet("BadRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }

    }
}
