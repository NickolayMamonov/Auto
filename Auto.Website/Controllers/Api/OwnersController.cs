using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Auto.Data;
using Auto.Data.Entities;
using Auto.Website.Models;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;


namespace Auto.Website.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IAutoDatabase db;

        public OwnersController(IAutoDatabase db)
        {
            this.db = db;
        }

        private dynamic Paginate(string url, int index, int count, int total)
        {
            dynamic links = new ExpandoObject();
            links.self = new { href = url };
            links.final = new { href = $"{url}?index={total - (total % count)}&count={count}" };
            links.first = new { href = $"{url}?index=0&count={count}" };
            if (index > 0) links.previous = new { href = $"{url}?index={index - count}&count={count}" };
            if (index + count < total) links.next = new { href = $"{url}?index={index + count}&count={count}" };
            return links;
        }

        // GET: api/owners
        [HttpGet]
        [Produces("application/hal+json")]
        public IActionResult Get(int index = 0, int count = 10)
        {
            var items = db.ListOwners().Skip(index).Take(count);
            var total = db.CountOwners();
            var _links = Paginate("/api/owners", index, count, total);
            var _actions = new
            {
                create = new
                {
                    method = "POST",
                    type = "application/json",
                    name = "Create a new owner",
                    href = "/api/owners"
                },
                delete = new
                {
                    method = "DELETE",
                    name = "Delete a owner",
                    href = "/api/owners/{id}"
                }
            };
            var result = new
            {
                _links,
                _actions,
                index,
                count,
                total,
                items
            };
            return Ok(result);
        }

        // GET api/owners/iwillms@hotmail.com
        [HttpGet("{email}")]
        public IActionResult Get(string email)
        {
            var owner = db.FindOwnerByEmail(email);
            if (owner == default) return NotFound();
            var json = owner.ToDynamic();
            json._links = new
            {
                self = new { href = $"/api/vehicles/{email}" },
                vehicle = new { href = $"/api/vehicle/{owner.OwnersVehicle.Registration}" }
            };
            json._actions = new
            {
                update = new
                {
                    method = "PUT",
                    href = $"/api/owners/{email}",
                    accept = "application/json"
                },
                delete = new
                {
                    method = "DELETE",
                    href = $"/api/owners/{email}"
                }
            };
            return Ok(json);
        }

        // PUT api/owners/update/iwillms@hotmail.com
        [HttpPut]
        [Route("update/{email}")]
        public IActionResult Put(string email, [FromBody] OwnerDto ownerDto)
        {
            var findVehicle = db.FindVehicle(ownerDto.OwnersVehicle);
            var newOwner = new Owner
            {
                Firstname = ownerDto.Firstname,
                Midname = ownerDto.Midname,
                Lastname = ownerDto.Lastname,
                Email = email,
                OwnersVehicle = findVehicle

            };
            db.UpdateOwner(newOwner,email);
            return Get(email);
        }

        // DELETE api/owners/iwillms@hotmail.com
        [HttpDelete]     
        [Route("delete/{email}")]
        public IActionResult Delete(string email)
        {
            var owner = db.FindOwnerByEmail(email);
            if (owner == default) return NotFound();
            db.DeleteOwner(owner);
            return NoContent();
        }
    }
}
