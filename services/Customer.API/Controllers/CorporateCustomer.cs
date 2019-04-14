using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Customer.API.Infrastructure.Contexts;
using Customer.API.Infrastructure.EventBuses;
using Customer.API.Infrastructure.Utils;
using Customer.API.Infrastructure.ViewModels;
using Customer.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Controllers
{
    [Route("customer/api/v1/corporate-customers")]
    [ApiController]
    public class CorporateCustomerController : ControllerBase
    {
        private readonly Infrastructure.Contexts.CustomerContext _customerContext;
        private readonly ICustomerEventBusService _customerEventBusService;

        public CorporateCustomerController(Infrastructure.Contexts.CustomerContext context, ICustomerEventBusService customerEventBusService)
        {
            _customerContext = context ??
                throw new ArgumentNullException(nameof(context));
            _customerEventBusService = customerEventBusService ??
                throw new ArgumentNullException(nameof(context));

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CorporateCustomer>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ItemsAsync([FromQuery] int limit, [FromQuery] int offset, [FromQuery] string sorts, [FromQuery] string fields, [FromQuery] string searches)
        {
            IEnumerable<CorporateCustomer> items = await _customerContext.CorporateCustomer.AsQueryable<CorporateCustomer>()
                .ParseSearches(searches)
                .ParseSort(sorts)
                .ToListAsync();

            return Ok(items);
        }

        // // GET api/v1/[controller]/items/{id}
        // /// <summary>
        // /// Gets corporate customer by unique customer id.
        // /// </summary>
        // /// <param name="id">Corporate customer unique id.</param>
        // /// <returns>Corporate customer.</returns>
        // [HttpGet]
        // [Route ("items/{id}")]
        // [ProducesResponseType ((int) HttpStatusCode.NotFound)]
        // [ProducesResponseType ((int) HttpStatusCode.BadRequest)]
        // [ProducesResponseType (typeof (CorporateCustomerItem), (int) HttpStatusCode.OK)]
        // public async Task<ActionResult<CorporateCustomerItem>> ItemByIdAsync (string id) {
        //     if (string.IsNullOrWhiteSpace (id)) {
        //         return BadRequest ();
        //     }

        //     var item = await _customerContext.CorporateCustomerItems.SingleOrDefaultAsync (ci => ci.Id == id);

        //     if (item != null) {
        //         return item;
        //     }

        //     return NotFound ();
        // }

        // //POST api/v1/[controller]/items
        // /// <summary>
        // /// Adds new corporate customer.
        // /// </summary>
        // /// <param name="customer">Corporate customer object.</param>
        // /// <returns>Added corporate customer id.</returns>
        // [Route ("items")]
        // [HttpPost]
        // [ProducesResponseType ((int) HttpStatusCode.Created)]
        // public async Task<ActionResult> CreateItemAsync ([FromBody] CorporateCustomerItem customer) {
        //     _customerContext.CorporateCustomerItems.Add (customer);

        //     await _customerContext.SaveChangesAsync ();

        //     return CreatedAtAction (nameof (ItemByIdAsync), new { id = customer.Id }, null);
        // }

        // //PUT api/v1/[controller]/items
        // /// <summary>
        // /// Updates corporate customer
        // /// </summary>
        // /// <param name="customerToUpdate">Corporate customer object.</param>
        // /// <returns>Updated corporate customer id.</returns>
        // [Route ("items")]
        // [HttpPut]
        // [ProducesResponseType ((int) HttpStatusCode.NotFound)]
        // [ProducesResponseType ((int) HttpStatusCode.Created)]
        // public async Task<ActionResult> UpdateItemAsync ([FromBody] CorporateCustomerItem customerToUpdate) {
        //     var customerItem = await _customerContext.CorporateCustomerItems.SingleOrDefaultAsync (i => i.Id == customerToUpdate.Id);

        //     if (customerItem == null) {
        //         return NotFound (new { Message = $"Item with id {customerToUpdate.Id} not found." });
        //     }

        //     _customerContext.CorporateCustomerItems.Update (customerToUpdate);

        //     await _customerContext.SaveChangesAsync ();

        //     return CreatedAtAction (nameof (ItemByIdAsync), new { id = customerToUpdate.Id }, null);
        // }

        // // DELETE api/v1/items/{id}
        // /// <summary>
        // /// Deletes corporate customer.
        // /// </summary>
        // /// <param name="id">Corporate customer unique id.</param>
        // /// <returns>Action result.</returns>
        // [Route ("items/{id}")]
        // [HttpDelete]
        // [ProducesResponseType ((int) HttpStatusCode.NoContent)]
        // [ProducesResponseType ((int) HttpStatusCode.NotFound)]
        // public async Task<ActionResult> DeleteItemAsync (string id) {
        //     var customer = _customerContext.CorporateCustomerItems.SingleOrDefault (x => x.Id == id);

        //     if (customer == null) {
        //         return NotFound ();
        //     }

        //     _customerContext.CorporateCustomerItems.Remove (customer);

        //     await _customerContext.SaveChangesAsync ();

        //     return NoContent ();
        // }
    }
}
