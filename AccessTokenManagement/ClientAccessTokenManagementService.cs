// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Example.Auth0.AuthenticationApi.AccessTokenManagement.Interfaces;

namespace Example.Auth0.AuthenticationApi.AccessTokenManagement
{
    /// <summary>
    /// Implements basic token management logic
    /// </summary>
    public class ClientAccessTokenManagementService : IClientAccessTokenManagementService
    {
        private static readonly ConcurrentDictionary<string, Lazy<Task<string>>> ClientTokenRequestDictionary =
            new ConcurrentDictionary<string, Lazy<Task<string>>>();

        private readonly ITokenEndpointService _tokenEndpointService;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="tokenEndpointService"></param>
        /// <param name="clientAccessTokenCache"></param>
        public ClientAccessTokenManagementService(ITokenEndpointService tokenEndpointService, IClientAccessTokenCache clientAccessTokenCache)
        {
            _tokenEndpointService = tokenEndpointService;
            _clientAccessTokenCache = clientAccessTokenCache;
        }

        /// <inheritdoc/>
        public async Task<string> GetClientAccessTokenAsync(string clientName, ClientAccessTokenParameters parameters, CancellationToken cancellationToken)
        {
            if (parameters.ForceRenewal == false)
            {
                var item = await _clientAccessTokenCache.GetAsync(clientName, parameters, cancellationToken);
                if (item != null)
                {
                    return item.AccessToken;
                }
            }

            try
            {
                return await ClientTokenRequestDictionary.GetOrAdd(clientName, _ =>
                {
                    return new Lazy<Task<string>>(async () =>
                    {
                        var response = await _tokenEndpointService.RequestClientAccessToken(clientName, cancellationToken);
                        
                        await _clientAccessTokenCache.SetAsync(clientName, response.AccessToken, response.Expiration, parameters, cancellationToken);
                        return response.AccessToken;
                    });
                }).Value;
            }
            finally
            {
                ClientTokenRequestDictionary.TryRemove(clientName, out _);
            }
        }

        /// <inheritdoc/>
        public Task DeleteClientAccessTokenAsync(string clientName, ClientAccessTokenParameters parameters, CancellationToken cancellationToken)
        {
            return _clientAccessTokenCache.DeleteAsync(clientName, parameters, cancellationToken);
        }
    }
}