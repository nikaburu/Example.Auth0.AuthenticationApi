// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Auth0.AuthenticationApi.AccessTokenManagement.Interfaces
{
    /// <summary>
    /// Abstraction for caching client access tokens
    /// </summary>
    public interface IClientAccessTokenCache
    {
        /// <summary>
        /// Caches a client access token
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="accessToken"></param>
        /// <param name="expiration"></param>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetAsync(string clientName, string accessToken, DateTimeOffset expiration,
            ClientAccessTokenParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a client access token from the cache
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ClientAccessToken> GetAsync(string clientName, ClientAccessTokenParameters parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a client access token from the cache
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(string clientName, ClientAccessTokenParameters parameters,
            CancellationToken cancellationToken = default);
    }
}