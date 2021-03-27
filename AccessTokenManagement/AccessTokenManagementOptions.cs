// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Example.Auth0.AuthenticationApi.AccessTokenManagement
{
    /// <summary>
    /// Options for the token management services
    /// </summary>
    public class AccessTokenManagementOptions
    {
        public static string Section { get; set; } = "AccessTokenManagement";

        /// <summary>
        /// Auth0 Domain
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Used to prefix the cache key
        /// </summary>
        public string CacheKeyPrefix { get; set; } = "AccessTokenManagement:";

        /// <summary>
        /// Value to subtract from token lifetime for the cache entry lifetime (defaults to 60 seconds)
        /// </summary>
        public int CacheLifetimeBuffer { get; set; } = 60;

        /// <summary>
        /// Options for client access tokens
        /// </summary>
        public List<ClientOptions> Clients { get; set; } = new List<ClientOptions>();

        /// <summary>
        /// Client access token options
        /// </summary>
        public class ClientOptions
        {
            public string Name { get; set; }

            public string ClientId { get; set; }

            public string ClientSecret { get; set; }

            public string Audience { get; set; }
        }
    }
}