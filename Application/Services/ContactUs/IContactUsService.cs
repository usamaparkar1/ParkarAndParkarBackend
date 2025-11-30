using Domain.Entities;

namespace Application.Services.ContactUs;

public interface IContactUsService
{
    /// <summary>
    /// Create Contact Us Request
    /// </summary>
    /// <param name="contactUSRequest"></param>
    /// <returns></returns>
    Task<ContactUsRequest> CreateContactUsRequestAsync(ContactUsRequest contactUsRequest);
}