﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Example.Auth0.AuthenticationApi.AccessTokenManagement
{
    /// <summary>
    /// Additional optional parameters for a client access token request
    /// </summary>
    public class ClientAccessTokenParameters
    {
        /// <summary>
        /// Force renewal of token.
        /// </summary>
        public bool ForceRenewal { get; set; }

        /// <summary>
        /// Specifies the resource parameter.
        /// </summary>
        public string Resource { get; set; }
    }
}