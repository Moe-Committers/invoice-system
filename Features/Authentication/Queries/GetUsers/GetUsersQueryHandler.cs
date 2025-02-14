using invoice_system.Database;
using invoice_system.Utils.DTOs.Authentication;
using invoice_system.Utils.Helpers.ResHelpers;
using invoice_system.Utils.Extensions;
using MediatR;

namespace invoice_system.Features.Authentication.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ApiResponse<List<UserDto>>>
{
    private readonly Db _db;

    public GetUsersQueryHandler(Db db)
    {
        _db = db;
    }

    public async Task<ApiResponse<List<UserDto>>> Handle(GetUsersQuery request, CancellationToken ct)
    {
        var query = _db.Users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Age = u.Age,
            Role = u.Role.Name,
            Status = u.Status,
            CreatedAt = u.CreatedAt,
            Avatar = u.Avatar
        }).AsQueryable();

        if (!string.IsNullOrEmpty(request.Search))
        {
            query = query.Where(u => u.Name.ToLower().Contains(request.Search)
            || u.Age == int.Parse(request.Search)
            || u.Email.ToLower().Contains(request.Search));
        }

        if (request.Id.HasValue)
        {
            query = query.Where(u => u.Id == request.Id);
        }

        if (request.FromDate.HasValue)
        {
            query = query.Where(u => u.CreatedAt >= request.FromDate);
        }

        if (request.ToDate.HasValue)
        {
            query = query.Where(u => u.CreatedAt <= request.ToDate);
        }

        query = request.Sort?.ToLower() switch
        {
            "name" => request.IsAscending
                ? query.OrderBy(u => u.Name)
                : query.OrderByDescending(b => b.Name),
            "age" => request.IsAscending
                ? query.OrderBy(u => u.Age)
                : query.OrderByDescending(u => u.Age),
            "created" => request.IsAscending
                ? query.OrderBy(u => u.CreatedAt)
                : query.OrderByDescending(u => u.CreatedAt),
            _ => query.OrderByDescending(u => u.CreatedAt)
        };

        return await query.UsePaginate(request.Page, request.PageSize, ct);
    }
}