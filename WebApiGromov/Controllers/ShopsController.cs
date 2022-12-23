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
using WebApiGromov.Models;

namespace WebApiGromov.Controllers
{
    public class ShopsController : ApiController
    {
        private APIgromovEntities db = new APIgromovEntities();


        [ResponseType(typeof(List<ModelShop>))]
        public IHttpActionResult GetShop(string str, int selectItem)
        {
            List<ModelShop> Shops = db.Shop.ToList().ConvertAll(x => new ModelShop(x));
            switch (selectItem)
            {
                case 1:
                    Shops = Shops.OrderBy(x => x.Title).ToList();
                    break;
                case 2:
                    Shops = Shops.OrderByDescending(x => x.Title).ToList();
                    break;
            }
            if (!String.IsNullOrEmpty(str))
                Shops = Shops.Where(x => x.Title.ToString().ToLower().Contains(str.ToString().ToLower())).ToList();
            return Ok(Shops);
        }

        // GET: api/Shops
        [ResponseType(typeof(List<Shop>))]
        public IHttpActionResult GetShop()
        {
            return Ok(db.Shop.ToList().ConvertAll(x => new ModelShop(x)));
        }

        // GET: api/Shops/5
        [ResponseType(typeof(Shop))]
        public IHttpActionResult GetShop(int id)
        {
            Shop shop = db.Shop.Find(id);
            if (shop == null)
            {
                return NotFound();
            }

            return Ok(shop);
        }

        // PUT: api/Shops/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShop(int id, Shop shop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shop.ID)
            {
                return BadRequest();
            }

            db.Entry(shop).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopExists(id))
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

        // POST: api/Shops
        [ResponseType(typeof(Shop))]
        public IHttpActionResult PostShop(Shop shop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shop.Add(shop);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shop.ID }, shop);
        }

        // DELETE: api/Shops/5
        [ResponseType(typeof(Shop))]
        public IHttpActionResult DeleteShop(int id)
        {
            Shop shop = db.Shop.Find(id);
            if (shop == null)
            {
                return NotFound();
            }

            db.Shop.Remove(shop);
            db.SaveChanges();

            return Ok(shop);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShopExists(int id)
        {
            return db.Shop.Count(e => e.ID == id) > 0;
        }
    }
}