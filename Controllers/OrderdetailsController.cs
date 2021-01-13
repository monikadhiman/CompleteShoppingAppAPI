using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using myShoppingApp.Models;

namespace myShoppingApp.Controllers
{
    public class OrderdetailsController : ApiController
    {
        private myShoppingAppEntities db = new myShoppingAppEntities();

        // GET: api/Orderdetails
        public IQueryable<Orderdetail> GetOrderdetails()
        {
            return db.Orderdetails;
        }

        // GET: api/Orderdetails/5
        [ResponseType(typeof(Orderdetail))]
        public IHttpActionResult GetOrderdetail(int id)
        {
            Orderdetail orderdetail = db.Orderdetails.Find(id);
            if (orderdetail == null)
            {
                return NotFound();
            }

            return Ok(orderdetail);
        }

        // PUT: api/Orderdetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderdetail(int id, Orderdetail orderdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderdetail.OrderId)
            {
                return BadRequest();
            }

            db.Entry(orderdetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderdetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("api/OrderDetails/CreateOrder")]
        public object CreateOrder(Orderdetail orderdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            orderdetail.IsConfirmed = false;
            orderdetail.OrderDate = DateTime.Now;
            orderdetail.Status = "Placed";

            db.Orderdetails.Add(orderdetail);
            int y= db.SaveChanges();
            //db.Orderdetails.Add(new Orderdetail()
            //{
            //    CustomerId = orderdetail.CustomerId,
            //    CustomerName = orderdetail.CustomerName,
            //    DeliveryAddress = orderdetail.DeliveryAddress,
            //    Phone = orderdetail.Phone,
            //    OrderDate = DateTime.Now,
            //    OrderPayMethod = orderdetail.OrderPayMethod,
            //    PaymentReferenceId = orderdetail.PaymentReferenceId,
            //    IsConfirmed = true,
            //    Status = "Placed"


            //});

            

            //return CreatedAtRoute("DefaultApi", new { id = orderDetail.OrderID }, orderDetail);
            return y;
        }

        // DELETE: api/Orderdetails/5
        [ResponseType(typeof(Orderdetail))]
        public IHttpActionResult DeleteOrderdetail(int id)
        {
            Orderdetail orderdetail = db.Orderdetails.Find(id);
            if (orderdetail == null)
            {
                return NotFound();
            }

            db.Orderdetails.Remove(orderdetail);
            db.SaveChanges();

            return Ok(orderdetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderdetailExists(int id)
        {
            return db.Orderdetails.Count(e => e.OrderId == id) > 0;
        }
    }
}