using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using Dapper;
using System.Data;

namespace CleanArch.Infrastructure.Repositories;

public class MemberDapperRepository : IMemberDapperRepository
{
    private readonly IDbConnection _connection;

    public MemberDapperRepository(IDbConnection connection) => _connection = connection;

    public async Task<IEnumerable<Member>> GetMembers()
    {
        var query = "SELECT * FROM Members";
        return await _connection.QueryAsync<Member>(query);
    }

    public async Task<Member?> GetMemberById(int id)
    {
        var query = "SELECT * FROM Members WHERE Id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<Member>(query, new { Id = id });
    }
}