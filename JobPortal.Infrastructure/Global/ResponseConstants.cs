namespace JobPortal.Infrastructure.Global
{
    public static class ResponseConstants
    {
        public const string Success = "Success";
        public const string UserExists = "User Already Exists";
        public const string UserNotExists = "User Does Not Exists";
        public const string InvalidPassword = "Please enter the valid password";
        public const string InvalidId = "Invalid Id";

        public const string DuplicateApplicant = "An applicant with the same email or phone has already applied for this job.";
        public const string InvalidStatus = "Invalid status";

        public const string DuplicateCompany = "A company with the same name already exists.";

        public const string DuplicateEnquiry = "A similar enquiry has already been submitted.";

        public const string CompanyNotFound = "Client company not found.";
        
        public const string DuplicateJob = "A job with the same title and location already exists for this company.";
    }

    public static class S3Directories
    {
        public const string Instructor = "instructor_images";
    }

    public static class TemplateName
    {
        public const string Otp = "otp.html";
        public const string ForgotPassword = "ForgotPassword.html";
    }

    public static class TemplateReplaceStrings
    {
        public const string ReplaceOTP = "{OTP}";
    }
    public static class EmailSubject
    {
        public const string OtpSubject = "OTP to login";
    }
}
