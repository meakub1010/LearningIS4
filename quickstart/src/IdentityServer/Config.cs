// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[] { new ApiResource("afcpayroll", "AFC Payroll") { ApiSecrets = { new Secret("secret".Sha256()) } } };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[] {
                new Client{
                    ClientId = "console-client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "afcpayroll" }
                },
                new Client{
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = {
                        new Secret("ro-secret".Sha256())
                    },
                    AllowedScopes = { "afcpayroll" },

                    AccessTokenLifetime = 10,
                    IdentityTokenLifetime = 30,
                    AccessTokenType = AccessTokenType.Reference,
                    
                    AllowOfflineAccess = true,

                    AbsoluteRefreshTokenLifetime = 10,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Absolute
                },
                new Client{
                    ClientId = "standard.aspnet.client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = {
                        new Secret("standard.aspnet-secret".Sha256())
                    },
                    RedirectUris = { "http://localhost:5000/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5000/" },
                    FrontChannelLogoutUri =  "http://localhost:5000/signout-oidc",

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "afcpayroll" }
                },
                new Client{
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = 
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                new Client
                {
                    ClientId = "mvc-hybrid",
                    ClientName = "MVC Hybrid",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    //RedirectUris           = { "http://localhost:5002/signin-oidc" },
                    //PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    RedirectUris =           { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/" },
                    FrontChannelLogoutUri =  "http://localhost:5002/signout-oidc",



                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "afcpayroll"
                    },
                    AllowOfflineAccess = false, // no refresh token
                    IdentityTokenLifetime = 30,
                    //AccessTokenLifetime = 90
                },
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =           { "http://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:5003" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "afcpayroll"
                    },
                    RequireConsent = false,
                    AllowOfflineAccess = true, // sent refresh token if client request offline_access scope

                    AccessTokenLifetime = 30,
                    IdentityTokenLifetime = 5,
                    AccessTokenType = AccessTokenType.Reference,

                    //RefreshTokenExpiration = TokenExpiration.Absolute,
                    //RefreshTokenUsage = TokenUsage.OneTimeOnly,

                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    AlwaysSendClientClaims = true,
                    SlidingRefreshTokenLifetime = 40,
                    AbsoluteRefreshTokenLifetime = 180 // 3 mins
                }
                ,
                new Client{
                     ClientId = "vbnet",
                    ClientName = "VbNet Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =           { "http://localhost:5005/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5005/Default.aspx" },
                    AllowedCorsOrigins =     { "http://localhost:5005" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "afcpayroll"
                    },
                    RequireConsent = false
                }
            };
        }
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "1",
                        Username = "alice",
                        Password = "password",
                        Claims = new []
                        {
                            new Claim(JwtClaimTypes.Name, "alice"),
                            new Claim(JwtClaimTypes.Role, "OpAdmin"),
                            new Claim(JwtClaimTypes.WebSite, "https://alice.com"),
                            new Claim(JwtClaimTypes.BirthDate, DateTime.Now.ToShortDateString()),
                            new Claim("email", "alice@gmail.com"),
                            new Claim("email_verified", "true")
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "2",
                        Username = "bob",
                        Password = "password",
                        Claims = new []
                        {
                            new Claim(JwtClaimTypes.Name, "bob"),
                            new Claim(JwtClaimTypes.Role, "UpkAdmin"),
                            new Claim(JwtClaimTypes.WebSite, "https://bob.com"),
                            new Claim(JwtClaimTypes.BirthDate, DateTime.Now.ToShortDateString()),
                            new Claim("email", "bob@gmail.com"),
                            new Claim("email_verified", "true")
                        }
                    }
            };
        }

    }
}