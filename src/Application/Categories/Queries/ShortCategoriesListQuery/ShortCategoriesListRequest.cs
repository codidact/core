using System.Collections.Generic;
using Codidact.Core.Application.Common.Contracts;

namespace Codidact.Core.Application.Categories.Queries.ShortCategoriesListQuery
{
    public class ShortCategoriesListRequest : IRequest<IEnumerable<ShortCategoryResult>>
    {
    }
}
