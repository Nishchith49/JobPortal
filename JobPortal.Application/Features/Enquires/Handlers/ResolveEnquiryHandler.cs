using JobPortal.Application.Features.Enquires.Commands;
using JobPortal.Infrastructure.Global;
using JobPortal.Infrastructure.Data;
using MediatR;

namespace JobPortal.Application.Features.Enquires.Handlers
{
    internal class ResolveEnquiryHandler : IRequestHandler<ResolveEnquiryCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public ResolveEnquiryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(ResolveEnquiryCommand request, CancellationToken cancellationToken)
        {
            var enquiry = await _context.Enquiries.FindAsync(request.Id);
            if (enquiry == null)
                return new(ResponseConstants.InvalidId, 400);

            enquiry.IsResolved = true;
            enquiry.ResolvedDate = DateTime.UtcNow;

            _context.Enquiries.Update(enquiry);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}
