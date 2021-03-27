using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Example.Auth0.AuthenticationApi.AccessTokenManagement.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Example.Auth0.AuthenticationApi.AccessTokenManagement
{
    /// <summary>
    /// Implements token endpoint operations using Auth0.AuthenticationApi
    /// </summary>
    public class Auth0TokenEndpointService : ITokenEndpointService
    {
        private readonly AccessTokenManagementOptions _accessTokenManagementOptions;
        private readonly ILogger<Auth0TokenEndpointService> _logger;

        public Auth0TokenEndpointService(IOptions<AccessTokenManagementOptions> accessTokenManagementOptions, ILogger<Auth0TokenEndpointService> logger)
        {
            _accessTokenManagementOptions = accessTokenManagementOptions.Value;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<ClientAccessToken> RequestClientAccessToken(string clientName, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Requesting client access token for client: {client}", clientName);

            var apiClient = new AuthenticationApiClient(new Uri(_accessTokenManagementOptions.Domain));
            var request = GetClientCredentialsRequest(clientName);

            var token = await apiClient.GetTokenAsync(request);
            return new ClientAccessToken
            {
                AccessToken = token.AccessToken,
                Expiration = DateTimeOffset.UtcNow.AddSeconds(token.ExpiresIn),
            };
        }

        private ClientCredentialsTokenRequest GetClientCredentialsRequest(string clientName)
        {
            var clientOptions = _accessTokenManagementOptions
                .Clients
                .FirstOrDefault(x => string.Equals(clientName, x.Name));

            if (clientOptions is null)
            {
                throw new InvalidOperationException($"No access token client configuration found for client: {clientName}");
            }
            
            _logger.LogDebug("Returning token client configuration for client: {client}", clientName);
            return new ClientCredentialsTokenRequest
            {
                ClientId = clientOptions.ClientId,
                ClientSecret = clientOptions.ClientSecret,
                Audience = clientOptions.Audience,
            };
        }
    }
}