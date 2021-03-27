// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using Example.Auth0.AuthenticationApi.AccessTokenManagement.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Example.Auth0.AuthenticationApi.AccessTokenManagement
{
    /// <summary>
    /// Extension methods for IServiceCollection to register the token management services
    /// </summary>
    public static class TokenManagementServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the token management services to DI
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static TokenManagementBuilder AddAccessTokenManagement(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddTransient<IClientAccessTokenCache, ClientAccessTokenCache>();
            services.Configure<AccessTokenManagementOptions>(configuration.GetSection(AccessTokenManagementOptions.Section));

            services.AddDistributedMemoryCache();
            
            services.TryAddTransient<IClientAccessTokenManagementService, ClientAccessTokenManagementService>();
            services.TryAddTransient<IClientAccessTokenCache, ClientAccessTokenCache>();
            services.TryAddTransient<ITokenEndpointService, Auth0TokenEndpointService>();

            return new TokenManagementBuilder(services);
        }
    }
}