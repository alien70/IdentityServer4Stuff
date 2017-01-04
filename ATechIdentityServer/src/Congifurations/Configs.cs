using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using static IdentityServer4.IdentityServerConstants;

namespace ATechIdentityServer.Configurations
{
    internal class Configs
    {
        public static IEnumerable<ApiResource> ApiResources
        {
            get
            {
                return new List<ApiResource>
                {
                    new ApiResource("api1", "My API")
                };
            }
        }

        public static IEnumerable<Client> Clients
        {
            get
            {
                return new List<Client>
                {
                    // Api protection
                    new Client
                    {
                        ClientId = "client",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,

                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedScopes = { "api1" }
                    },                    
                    // OpenID Connect implicit flow client (MVC)
                    new Client
                    {
                        ClientId = "mvc",
                        ClientName = "MVC Client",
                        AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                        RequireConsent = false,

                        // secret for authentication
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },

                        // where to redirect to after login
                        RedirectUris = { "http://localhost:5002/signin-oidc" },

                        // where to redirect to after logout
                        PostLogoutRedirectUris = { "http://localhost:5002" },

                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "api1"
                        },

                        AllowOfflineAccess = true
                    }
                };
            }
        }

        public static IEnumerable<IdentityResource> IdentityResources
        {
            get
            {
                return new List<IdentityResource>
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
                    new IdentityResources.Email(),
                    new IdentityResource("dataeventrecordsscope",new []{ "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin" , "dataEventRecords.user" } ),
                    new IdentityResource("securedfilesscope",new []{ "role", "admin", "user", "securedFiles", "securedFiles.admin", "securedFiles.user"})
                };
            }
        }
    }
}