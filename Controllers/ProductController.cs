using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using myShoppingApp.Models;
using System.IO;
using System.Web;
using myShoppingApp.ViewModal;

namespace myShoppingApp.Controllers
{
    public class ProductController : ApiController
    {
        myShoppingAppEntities db = new myShoppingAppEntities();

        // GET: api/Products
        [AllowAnonymous]
        public IEnumerable<ProductCategory> GetAllProductsByCategory()
        {
            var allProduct = db.Products.ToList();
            IEnumerable<ProductCategory> allProductByCategory = from p in allProduct
                                                                group p by p.Category into sl
                                                                select new ProductCategory()
                                                                {
                                                                    Category = sl.Key,
                                                                    Products = sl.Select(x => new Product
                                                                    {
                                                                        Id = x.Id,
                                                                        Name = x.Name,
                                                                        BillingAddress = x.BillingAddress,
                                                                        SellerId = x.SellerId,
                                                                        Description = x.Description,
                                                                        Quantity = x.Quantity,
                                                                        SellerName = x.SellerName,
                                                                        ImageName = x.ImageName,
                                                                        ImageFile = Utility.ImageToByteArray(x.ImageName),
                                                                        UnitPrice = x.UnitPrice,
                                                                        Category = x.Category,
                                                                    }).ToList()
                                                                };

            return allProductByCategory;
        }

        [HttpPost]
        [Route("api/Product/CreateProduct")]
        public object CreateProduct()
        {
            //var result = db.Products.Where(res => res.Name == pro.Name).SingleOrDefault();
            //int x = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                string imageName = null;
                var httpRequest = HttpContext.Current.Request;
                //Save Image
                var postedFile = httpRequest.Files["Image"];
                //Create custom filename
                imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmdd") + Path.GetExtension(postedFile.FileName);
                var filePath = HttpContext.Current.Server.MapPath("~/Image/" + imageName);
                postedFile.SaveAs(filePath);

                Product product = new Product();
                product.Name = httpRequest["Name"];
                product.Description = httpRequest["Description"];
                product.BillingAddress = httpRequest["BillingAddress"];
                product.UnitPrice = Convert.ToInt32(httpRequest["UnitPrice"]);
                product.Category = httpRequest["Category"];
                product.Quantity = Convert.ToInt32(httpRequest["Quantity"]);
                product.ImageName = imageName;
                product.TC = httpRequest["TC"];
                product.SellerId = httpRequest["SellerId"];
                product.SellerName = httpRequest["SellerName"];
                db.Products.Add(product);
                int x = db.SaveChanges();

                return x;
            }
            catch (Exception ex)
            {
                return ex;
            }

        }

    }
}
