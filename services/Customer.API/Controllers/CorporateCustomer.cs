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
    [Route ("customer/api/v1/corporate-customers")]
    [ApiController]
    public class CorporateCustomerController : ControllerBase {
        private readonly Infrastructure.Contexts.CustomerContext _customerContext;
        private readonly ICustomerEventBusService _customerEventBusService;

        public CorporateCustomerController (Infrastructure.Contexts.CustomerContext context, ICustomerEventBusService customerEventBusService) {
            _customerContext = context ??
                throw new ArgumentNullException (nameof (context));
            _customerEventBusService = customerEventBusService ??
                throw new ArgumentNullException (nameof (context));

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [HttpGet]
        [ProducesResponseType (typeof (IEnumerable<CorporateCustomer>), (int) HttpStatusCode.OK)]
        [ProducesResponseType (typeof (string), (int) HttpStatusCode.OK)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync ([FromQuery] int? limit, [FromQuery] int? offset, [FromQuery] string sorts, [FromQuery] string fields, [FromQuery] string searches) {
            var items = await _customerContext.CorporateCustomer.AsQueryable<CorporateCustomer> ()
                .DynamicWhere (searches)
                .DynamicOrder (sorts)
                .DynamicSelect (fields)
                .DynamicTake (limit)
                .DynamicSkip (offset)
                .ToListAsync ();

            return Ok (items);
        }

        [HttpPost]
        [ProducesResponseType (typeof (CorporateCustomer), (int) HttpStatusCode.OK)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddAsync ([FromBody] CorporateCustomer newCustomer) {
            string validationResult = newCustomer.ValidateAndPrepeareForInsert ();

            if (!string.IsNullOrEmpty (validationResult)) {
                Error a = new Error ();

                a.Title = "Invalid fields";
                a.Message = validationResult;

                return BadRequest (a);
            }

            _customerContext.CorporateCustomer.Add (newCustomer);
            await _customerContext.SaveChangesAsync ();

            return Ok (newCustomer);
        }

        [HttpPut]
        [ProducesResponseType (typeof (CorporateCustomer), (int) HttpStatusCode.OK)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateAsync ([FromBody] CorporateCustomer customerToUpdate) {
            // var customerItem = await _customerContext.RetailCustomer.SingleOrDefaultAsync(i => i.Id == customerToUpdate.Id);

            //TODO return not found error
            // if (customerItem == null)
            // {
            //     return NotFound(new { Message = $"Item with id {customerToUpdate.Id} not found." });
            // }

            _customerContext.CorporateCustomer.Update (customerToUpdate);

            await _customerContext.SaveChangesAsync ();
            return Ok (customerToUpdate);
        }

        [HttpDelete]
        [Route ("{id}")]
        [ProducesResponseType ((int) HttpStatusCode.OK)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteAsync ([FromRoute] string id) {
            //TODO return not found error
            var customerItem = await _customerContext.CorporateCustomer.SingleOrDefaultAsync (i => i.Id == id);

            // if (customerItem == null)
            // {
            //     return NotFound(new { Message = $"Item with id {customerToUpdate.Id} not found." });
            // }

            _customerContext.CorporateCustomer.Remove (customerItem);

            await _customerContext.SaveChangesAsync ();
            return Ok ();
        }

        [HttpGet]
        [Route ("{id}")]
        [ProducesResponseType (typeof (CorporateCustomer), (int) HttpStatusCode.OK)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType (typeof (Error), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetbyIdAsync ([FromRoute] string id) {
            //TODO return not found error
            // var customerItem = await _customerContext.RetailCustomer.SingleOrDefaultAsync(i => i.Id == customerToUpdate.Id);

            // if (customerItem == null)
            // {
            //     return NotFound(new { Message = $"Item with id {customerToUpdate.Id} not found." });
            // }

            CorporateCustomer customer = await _customerContext.CorporateCustomer.Where (x => x.Id == id).SingleOrDefaultAsync ();

            return Ok (customer);
        }
    }
}