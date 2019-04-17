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
using Newtonsoft.Json;

namespace Customer.API.Controllers {
    [Route ("customer/api/v1/customers/{customerNumber}/phones")]
    [ApiController]
    public class CustomerPhoneController : ControllerBase {
        private readonly Infrastructure.Contexts.CustomerContext _customerContext;
        private readonly ICustomerEventBusService _customerEventBusService;

        public CustomerPhoneController (Infrastructure.Contexts.CustomerContext context, ICustomerEventBusService customerEventBusService) {
            _customerContext = context ??
                throw new ArgumentNullException (nameof (context));
            _customerEventBusService = customerEventBusService ??
                throw new ArgumentNullException (nameof (context));

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [HttpGet]
        [ProducesResponseType (typeof (IEnumerable<CustomerPhone>), (int) HttpStatusCode.OK)]
        [ProducesResponseType (typeof (string), (int) HttpStatusCode.OK)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync ([FromRoute] int customerNumber, [FromQuery] int? limit, [FromQuery] int? offset, [FromQuery] string sorts, [FromQuery] string fields, [FromQuery] string searches) {
            if (string.IsNullOrEmpty (searches)) {
                searches = "customerNumber[=]" + customerNumber;
            } else {
                searches += ",customerNumber[=]" + customerNumber;
            }

            var items = await _customerContext.CustomerPhone.AsQueryable<CustomerPhone> ()
                .DynamicWhere (searches)
                .DynamicOrder (sorts)
                .DynamicSelect (fields)
                .DynamicTake (limit)
                .DynamicSkip (offset)
                .ToListAsync ();

            return Ok (items);
        }

        [HttpPost]
        [ProducesResponseType (typeof (CustomerPhone), (int) HttpStatusCode.OK)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddAsync ([FromRoute] int customerNumber, [FromBody] CustomerPhone newPhone) {
            _customerContext.CustomerPhone.Add (newPhone);
            await _customerContext.SaveChangesAsync ();

            return Ok (newPhone);
        }

        [HttpPut]
        [ProducesResponseType (typeof (CustomerPhone), (int) HttpStatusCode.OK)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateAsync ([FromRoute] int customerNumber, [FromBody] CustomerPhone phoneToUpdate) {
            // var customerItem = await _customerContext.RetailCustomer.SingleOrDefaultAsync(i => i.Id == customerToUpdate.Id);

            //TODO return not found error
            // if (customerItem == null)
            // {
            //     return NotFound(new { Message = $"Item with id {customerToUpdate.Id} not found." });
            // }

            _customerContext.CustomerPhone.Update (phoneToUpdate);

            await _customerContext.SaveChangesAsync ();
            return Ok (phoneToUpdate);
        }

        [HttpDelete]
        [Route ("{id}")]
        [ProducesResponseType ((int) HttpStatusCode.OK)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteAsync ([FromRoute] int customerNumber, [FromRoute] string id) {
            //TODO return not found error
            var phoneItem = await _customerContext.CustomerPhone.SingleOrDefaultAsync (i => i.CustomerNumber == customerNumber && i.Id == id);

            // if (customerItem == null)
            // {
            //     return NotFound(new { Message = $"Item with id {customerToUpdate.Id} not found." });
            // }

            _customerContext.CustomerPhone.Remove (phoneItem);

            await _customerContext.SaveChangesAsync ();
            return Ok ();
        }

        [HttpGet]
        [Route ("{id}")]
        [ProducesResponseType (typeof (CustomerPhone), (int) HttpStatusCode.OK)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetbyIdAsync ([FromRoute] int customerNumber, [FromRoute] string id) {
            //TODO return not found error
            // var customerItem = await _customerContext.RetailCustomer.SingleOrDefaultAsync(i => i.Id == customerToUpdate.Id);

            // if (customerItem == null)
            // {
            //     return NotFound(new { Message = $"Item with id {customerToUpdate.Id} not found." });
            // }

            CustomerPhone customer = await _customerContext.CustomerPhone.Where (x => x.CustomerNumber == customerNumber && x.Id == id).SingleOrDefaultAsync ();

            return Ok (customer);
        }

    }
}