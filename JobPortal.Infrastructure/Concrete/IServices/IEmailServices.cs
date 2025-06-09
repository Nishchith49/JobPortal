namespace JobPortal.Infrastructure.Concrete.IServices
{
    public interface IEmailServices
    {
        Task SendOtp(string OTP, string email);

        Task SendMultipleEmail(string subject, List<string> email, string content, List<string> attachments);
    }
}
