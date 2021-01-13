using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using myShoppingApp.Models;

namespace myShoppingApp.Controllers
{
    public class RegistrationController : ApiController
    {
        myShoppingAppEntities db = new myShoppingAppEntities();
        [HttpPost]
        [Route("api/Registration/CreateUsers")]
        public object CreateUsers(Registration reg) {
            var result = db.Registrations.Where(res => res.email == reg.email).SingleOrDefault();
            int x = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (result == null)
                {
                    db.Registrations.Add(new Registration()
                    {
                        username = reg.username,
                        password = reg.password,
                        email = reg.email,
                        role = reg.role,
                        gender = reg.gender,
                        phone = reg.phone

                    });
                  x =  db.SaveChanges();
                }
                else
                {
                    return "Exists";
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
            return x;
        }

        [HttpPost]
        [Route("api/Registration/LoginUser")]
        public object LoginUser(Registration reg)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = from x in db.Registrations.Where(x => x.email == reg.email && x.password == reg.password) select x;
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }

        }
    }
}
