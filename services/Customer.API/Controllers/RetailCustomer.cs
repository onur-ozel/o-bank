using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Customer.API.Infrastructure.Contexts;
using Customer.API.Infrastructure.ViewModels;
using Customer.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Controllers {
    [Route ("api/v1/[controller]")]
    [ApiController]
    public class RetailCustomerController : ControllerBase {

        private CustomerContext _customerContext;
        // private readonly CatalogSettings _settings;
        // private readonly ICatalogIntegrationEventService _catalogIntegrationEventService;

        // public RetailCustomerController (CustomerContext context, IOptionsSnapshot<CatalogSettings> settings, ICatalogIntegrationEventService catalogIntegrationEventService) {
        public RetailCustomerController (CustomerContext context) {
            _customerContext = context ??
                throw new ArgumentNullException (nameof (context));
            // _catalogIntegrationEventService = catalogIntegrationEventService ??
            //     throw new ArgumentNullException (nameof (catalogIntegrationEventService));
            // _settings = settings.Value;

            // context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        /// GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route ("items")]
        [ProducesResponseType (typeof (PaginatedItemsViewModel<RetailCustomerItem>), (int) HttpStatusCode.OK)]
        [ProducesResponseType (typeof (IEnumerable<RetailCustomerItem>), (int) HttpStatusCode.OK)]
        [ProducesResponseType ((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ItemsAsync ([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0) {
            var totalItems = await _customerContext.RetailCustomerItems
                .LongCountAsync ();

            var itemsOnPage = await _customerContext.RetailCustomerItems
                .OrderBy (c => c.No)
                .Skip (pageSize * pageIndex)
                .Take (pageSize)
                .ToListAsync ();

            var model = new PaginatedItemsViewModel<RetailCustomerItem> (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok (model);
        }

        // GET api/v1/[controller]/items/{id}
        [HttpGet]
        [Route ("items/{id}")]
        [ProducesResponseType ((int) HttpStatusCode.NotFound)]
        [ProducesResponseType ((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType (typeof (RetailCustomerItem), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<RetailCustomerItem>> ItemByIdAsync (string id) {
            if (string.IsNullOrWhiteSpace (id)) {
                return BadRequest ();
            }

            var item = await _customerContext.RetailCustomerItems.SingleOrDefaultAsync (ci => ci.Id == id);

            if (item != null) {
                return item;
            }

            return NotFound ();
        }

        //POST api/v1/[controller]/items
        [HttpPost]
        [Route ("items")]
        [ProducesResponseType ((int) HttpStatusCode.Created)]
        public async Task<ActionResult> CreateItemAsync ([FromBody] RetailCustomerItem customer) {
            _customerContext.RetailCustomerItems.Add (customer);

            //TODO Handle Errors and validate object
            //FIXME When No is not empty, exception throws
            await _customerContext.SaveChangesAsync ();

            return CreatedAtAction (nameof (ItemByIdAsync), new { id = customer.Id }, null);
        }

        //PUT api/v1/[controller]/items
        [HttpPut]
        [Route ("items")]
        [ProducesResponseType ((int) HttpStatusCode.NotFound)]
        [ProducesResponseType ((int) HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateItemAsync ([FromBody] RetailCustomerItem customerToUpdate) {
            var customerItem = await _customerContext.RetailCustomerItems.SingleOrDefaultAsync (i => i.Id == customerToUpdate.Id);

            if (customerItem == null) {
                return NotFound (new { Message = $"Item with id {customerToUpdate.Id} not found." });
            }

            _customerContext.RetailCustomerItems.Update (customerToUpdate);

            await _customerContext.SaveChangesAsync ();

            return CreatedAtAction (nameof (ItemByIdAsync), new { id = customerToUpdate.Id }, null);
        }

        // DELETE api/v1/items/{id}
        [HttpDelete]
        [Route ("items/{id}")]
        [ProducesResponseType ((int) HttpStatusCode.NoContent)]
        [ProducesResponseType ((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteItemAsync (string id) {
            var customer = _customerContext.RetailCustomerItems.SingleOrDefault (x => x.Id == id);

            if (customer == null) {
                return NotFound ();
            }

            _customerContext.RetailCustomerItems.Remove (customer);

            await _customerContext.SaveChangesAsync ();

            return NoContent ();
        }
    }
}