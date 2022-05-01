using Iris.Database;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Iris.Stores.AuthRequestStore;

/// <inheritdoc cref="IAuthRequestsStore"/>
public class AuthRequestsStore : IAuthRequestsStore
{
    private const int Lifetime = 1 * 60;
    private const int CleanUpPeriod = 5 * 60 * 1000;

    private readonly IServiceScopeFactory _serviceScopeFactory;

    /// <summary>
    /// .ctor
    /// </summary>
    public AuthRequestsStore(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;

        RunCleanup();

        _ = new Timer(_ => RunCleanup(), null, CleanUpPeriod, CleanUpPeriod);
    }

    /// <inheritdoc/>
    public AuthRequestOperation CreateRequest()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        var request = new AuthRequestOperation
        {
            Id = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())),
            IssuedDateTime = DateTime.Now
        };

        context.AuthRequests.Add(request);
        context.SaveChanges();

        return request;
    }

    /// <inheritdoc/>
    public AuthRequestOperation FindRequest(string id)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        return context.AuthRequests
            .AsNoTracking()
            .FirstOrDefault(_ => _.Id == id && _.IssuedDateTime.AddSeconds(Lifetime) > DateTime.Now);
    }

    private void RunCleanup()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        var requests = context.AuthRequests.Where(_ => _.IssuedDateTime.AddSeconds(Lifetime) < DateTime.Now);

        context.AuthRequests.RemoveRange(requests);
        context.SaveChanges();

    }
}