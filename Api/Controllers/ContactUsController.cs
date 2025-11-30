using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Services.ContactUs;

[ApiController]
[Route("api/[controller]")]
public class ContactUsController : ControllerBase
{
    private readonly IContactUsService _contactUsService;

    public ContactUsController(
        IContactUsService contactUsService
    )
    {
        _contactUsService = contactUsService;
    }

    [HttpPost("create-contact-us-request")]
    public async Task<ActionResult> CreateContactUsRequest([FromBody] ContactUsRequest contactUsRequest)
    {
        ContactUsRequest createdRequest = await _contactUsService.CreateContactUsRequestAsync(contactUsRequest);
        return Ok(createdRequest);
    }
}