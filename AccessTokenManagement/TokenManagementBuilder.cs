// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace Example.Auth0.AuthenticationApi.AccessTokenManagement
{
    /// <summary>
    /// Builder object for the token management services
    /// </summary>
    public class TokenManagementBuilder
    {
        /// <summary>
        /// The underlying service collection
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="services"></param>
        public TokenManagementBuilder(IServiceCollection services)
        {
            Services = services;
        }
    }
}