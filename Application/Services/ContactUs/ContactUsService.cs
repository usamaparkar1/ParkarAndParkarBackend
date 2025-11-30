using Domain.Entities;

namespace Application.Services.ContactUs;

public class ContactUsService : IContactUsService
{
    public async Task<ContactUsRequest> CreateContactUsRequestAsync(ContactUsRequest contactUsRequest)
    {
        // Implementation logic to create a Contact Us request
        // This is a placeholder for actual data persistence logic
        await Task.Delay(100); // Simulating async work
        return contactUsRequest; // In a real scenario, this would return the saved entity with an ID
    }
}