using JobPortal.Application.Features.DropDowns.Models;
using JobPortal.Application.Features.DropDowns.Queries;
using JobPortal.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.DropDowns.Handlers
{
    internal class GetJobDropDownHandler : IRequestHandler<GetJobDropDownQuery, List<DropDownModel>>
    {
        private readonly ApplicationDbContext _context;

        public GetJobDropDownHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DropDownModel>> Handle(GetJobDropDownQuery request, CancellationToken cancellationToken)
        {
            return await _context.Jobs
                                 .OrderBy(x => x.Title)
                                 .Select(x => new DropDownModel
                                 {
                                     Label = x.Title,
                                     Value = x.Id
                                 }).ToListAsync(cancellationToken);
        }
    }
}
