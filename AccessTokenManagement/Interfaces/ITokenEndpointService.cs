// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Threading;
using System.Threading.Tasks;

namespace Example.Auth0.AuthenticationApi.AccessTokenManagement.Interfaces
{
    /// <summary>
    /// Abstraction for token endpoint operations
    /// </summary>
    public interface ITokenEndpointService
    {
        /// <summary>
        /// Requests a client access token.
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ClientAccessToken> RequestClientAccessToken(string clientName, CancellationToken cancellationToken);
    }
}