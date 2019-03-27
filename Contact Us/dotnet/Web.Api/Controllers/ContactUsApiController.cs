﻿using System;
using Microsoft.AspNetCore.Mvc;
using Sabio.Services;
using Sabio.Web.Controllers;
using Microsoft.Extensions.Logging;
using Sabio.Web.Models.Responses;
using Sabio.Models.Requests.ContactUs;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/contactus")]
    [ApiController]
    public class ContactUsApiController : BaseApiController
    {
        private ILogger _logger;
        private IContactUsService _contactUs;

        public ContactUsApiController(ILogger<ContactUsApiController> logger, IContactUsService contactUs) : base(logger)
        {
            _logger = logger;
            _contactUs = contactUs;
        }

        [HttpPost]
        public ActionResult<ItemResponse<int>> Insert(ContactUsAddRequest model)
        {
            ActionResult result = null;
            try
            {
                int id = _contactUs.Insert(model);
                ItemResponse<int> response = new ItemResponse<int>();
                response.Item = id;
                result = Created201(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message));
            }
            return result;
        }
    }
}