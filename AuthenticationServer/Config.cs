
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using  IdentityServer7;
using  IdentityServer7.Models;
using  IdentityServer7.Storage.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),

            };

        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("ShopApi",new List<string>(){"name","role" }),

            };


        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                        new Client
                        {
                            ClientId = "MobileClient",
                            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                            ClientSecrets =
                            {
                                new Secret("secret".Sha256())
                            },
                            AccessTokenLifetime=60,
                            AllowOfflineAccess = true,
                            AllowedScopes = { "ShopApi", "openid","profile" }
                        },
                        new Client
                            {
                                ClientId = "client",
                                // no interactive user, use the clientid/secret for authentication
                                AllowedGrantTypes = GrantTypes.ClientCredentials,
                                // secret for authentication
                                ClientSecrets =
                                {
                                    new Secret("secret".Sha256())
                                },
                                // scopes that client has access to
                                AllowedScopes = { "ShopApi"},
                                Claims=new List<ClientClaim>
                                {
                                    new ClientClaim("role","admin"),
                                    new ClientClaim("name","client")
                                }

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
                                    "ShopApi","openid","profile"
                                }
                            },
                                new Client
                                {
                                    ClientId = "mvc",
                                    ClientName = "MVC Client",
                                    AllowedGrantTypes = GrantTypes.Hybrid,

                                    ClientSecrets =
                                    {
                                        new Secret("secret".Sha256())
                                    },

                                    RedirectUris           = { "http://localhost:7000/signin-oidc" },
                                    PostLogoutRedirectUris = { "http://localhost:7000/signout-callback-oidc" },

                                    AllowedScopes =
                                    {
                                        IdentityServerConstants.StandardScopes.OpenId,
                                        IdentityServerConstants.StandardScopes.Profile,
                                        "ShopApi"
                                    },

                                    //AllowOfflineAccess = true
                                },


                    };
         };




}
