using JobPortal.Application.Features.Enquires.Models;
using JobPortal.Application.Features.Enquires.Queries;
using JobPortal.Infrastructure.Global;
using JobPortal.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.Enquires.Handlers
{
    internal class GetAllEnquiriesHandler : IRequestHandler<GetAllEnquiriesQuery, PagedResponse<List<EnquiryModel>>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllEnquiriesHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<List<EnquiryModel>>> Handle(GetAllEnquiriesQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Enquiries
                                       .Where(x => string.IsNullOrWhiteSpace(request.FormattedSearchString()) ||
                                                   x.Name.ToLower().Replace(" ", string.Empty).Contains(request.FormattedSearchString()) ||
                                                   x.Email.ToLower().Replace(" ", string.Empty).Contains(request.FormattedSearchString()) ||
                                                   x.Phone.ToLower().Replace(" ", string.Empty).Contains(request.FormattedSearchString()) ||
                                                   x.Subject.ToLower().Replace(" ", string.Empty).Contains(request.FormattedSearchString()))
                                       .GroupBy(x => 1)
                                       .Select(x => new PagedResponseWithQuery<List<EnquiryModel>>
                                       {
                                            TotalRecords = x.Count(),
                                            Data = x.Select(entity => new EnquiryModel
                                            {
                                                 Id = entity.Id,
                                                 Name = entity.Name,
                                                 Email = entity.Email,
                                                 Phone = entity.Phone,
                                                 Subject = entity.Subject,
                                                 Message = entity.Message,
                                                 CreatedDate = entity.CreatedDate,
                                                 IsResolved = entity.IsResolved,
                                                 ResolvedDate = entity.ResolvedDate
                                            })
                                            .Skip(request.PageSize * request.PageIndex)
                                            .Take(request.PageSize)
                                            .ToList()
                                       })
                                       .FirstOrDefaultAsync(cancellationToken);

            return new(ResponseConstants.Success, 200, entity?.Data, request.PageIndex, request.PageSize, entity?.TotalRecords ?? 0);
        }
    }
}
