using JobPortal.Application.Features.Enquires.Commands;
using JobPortal.Infrastructure.Global;
using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Enquires.Handlers
{
    internal class CreateEnquiryHandler : IRequestHandler<CreateEnquiryCommand, APIResponse>
    {
        private readonly ApplicationDbContext _context;

        public CreateEnquiryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIResponse> Handle(CreateEnquiryCommand request, CancellationToken cancellationToken)
        {
            bool isDuplicate = await _context.Enquiries
                                             .AnyAsync(x => EF.Functions.Like(x.Email.Replace(" ", ""), request.Email.Replace(" ", "")) &&
                                                            EF.Functions.Like(x.Subject.Replace(" ", ""), request.Subject.Replace(" ", "")) &&
                                                            EF.Functions.Like(x.Message.Replace(" ", ""), request.Message.Replace(" ", "")),
                                                            cancellationToken);
            if (isDuplicate)
                return new(ResponseConstants.DuplicateEnquiry, 400);

            var entity = new Enquiry
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Subject = request.Subject,
                Message = request.Message,
            };

            _context.Enquiries.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new(ResponseConstants.Success, 200);
        }
    }
}
