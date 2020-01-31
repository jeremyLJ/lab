using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServerQuickStart
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("apiOne", "my api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client1",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret1".Sha256())
                    },

                    AllowedScopes = { "apiOne" }
                },
                new Client
                {
                   ClientId = "ro.client",
                   AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                   ClientSecrets =
                    {
                        new Secret("ro.secret".Sha256())
                    },
                   AllowedScopes={"apiOne"}
                },
                // OpenID Connect implicit flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName="MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    // where to redirect to after login
                    RedirectUris = {"http://localhost:5002/signin-oidc"},
                    // where to redirect to after logout
                    PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
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
                    Username = "jeremy",
                    Password ="password",
                    Claims = new[]
                    {
                        new Claim("name", "Jeremy"),
                        new Claim("website", "https://jeremy.liu.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "alice",
                    Password ="password"
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}
