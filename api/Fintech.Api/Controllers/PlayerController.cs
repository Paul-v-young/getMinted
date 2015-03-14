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
using Fintech.Data.Models;
using Fintech.Data.Repositories;

namespace Fintech.Api.Controllers
{
    public class PlayersController : ApiController
    {
        private Repository<Player> db = new Repository<Player>();

        // GET: api/Players
        public IQueryable<Player> GetPlayers()
        {
            return db.Table;
        }

        // GET: api/Players/5
        [ResponseType(typeof(Player))]
        public IHttpActionResult GetPlayer(int id)
        {
            Player Player = db.Find(id);
            if (Player == null)
            {
                return NotFound();
            }

            return Ok(Player);
        }

        // PUT: api/Players/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlayer(int id, Player Player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Player.Id)
            {
                return BadRequest();
            }

            try
            {
                db.Update(Player);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(Player);
        }

        // POST: api/Players
        [ResponseType(typeof(Player))]
        public IHttpActionResult PostPlayer(Player Player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                db.Add(Player);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(Player);
        }

        // DELETE: api/Players/5
        [ResponseType(typeof(Player))]
        public IHttpActionResult DeletePlayer(int id)
        {
            Player Player = db.Find(id);
            if (Player == null)
            {
                return NotFound();
            }

            try
            {
                db.Delete(Player);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}