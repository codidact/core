using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codidact.Core.Application.Common.Contracts;
using Codidact.Core.Application.Common.Interfaces;
using Codidact.Core.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Codidact.Core.Application.Categories.Queries.ShortCategoriesListQuery
{
    /// <summary>
    /// Returns a list of categories with minimal data
    /// </summary>
    public class ShortCategoriesListQuery : IRequestHandler<ShortCategoriesListRequest, IEnumerable<ShortCategoryResult>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<ShortCategoriesListQuery> _logger;

        public ShortCategoriesListQuery(IApplicationDbContext context, ILogger<ShortCategoriesListQuery> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<IEnumerable<ShortCategoryResult>> Handle(ShortCategoriesListRequest message)
        {
            _logger.LogInformation($"{DateTime.UtcNow.ToString("u")} - Starting to handle request for Questions List");

            return Task.FromResult(_context.Categories.Select(MapCategoryToShortCategory).AsEnumerable());
        }

        private ShortCategoryResult MapCategoryToShortCategory(Category category)
        {
            return new ShortCategoryResult
            {
                Name = category.DisplayName
            };
        }
    }
}
