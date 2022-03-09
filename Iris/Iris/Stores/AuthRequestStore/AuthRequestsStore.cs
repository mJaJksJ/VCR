using Iris.Database;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Iris.Stores.AuthRequestStore;

/// <inheritdoc cref="IAuthRequestsStore"/>
public class AuthRequestsStore : IAuthRequestsStore, IDisposable
{
    private const int Lifetime = 1 * 60;
    private const int CleanUpPeriod = 5 * 60 * 1000;

    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly Timer _timer;

    private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<AuthRequestsStore>();

    /// <summary>
    /// .ctor
    /// </summary>
    public AuthRequestsStore(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;

        try
        {
            RunCleanup();
        }
        catch (Exception e)
        {
            Log.Error(e, "AuthorizationRequestStore cleanup error");
        }

        _timer = new Timer(_ => RunCleanup(), null, CleanUpPeriod, CleanUpPeriod);
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

    /// <inheritdoc/>
    public void Dispose()
    {
        _timer.Dispose();
    }

    private void RunCleanup()
    {
        try
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            var requests = context.AuthRequests.Where(_ => _.IssuedDateTime.AddSeconds(Lifetime) < DateTime.Now);

            context.AuthRequests.RemoveRange(requests);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            Log.Error(e, "Error clean up auth requests");
        }
    }
}