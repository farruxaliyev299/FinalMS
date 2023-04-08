using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;
using FinalMS.DuendeIS.Data;
using FinalMS.DuendeIS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinalMS.DuendeIS;


public static class SeedData
{
    public const string Admin = "Admin";
    public const string Customer = "Customer";

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource("resource_catalog")     {Scopes={"catalog_fullpermission"}},
        new ApiResource("resource_photo_stock") {Scopes={"photo_stock_fullpermission"}},
        new ApiResource("resource_basket")      {Scopes={"basket_fullpermission"}},
        new ApiResource("resource_discount")    {Scopes={"discount_fullpermission"}},
        new ApiResource("resource_order")       {Scopes={"order_fullpermission"}},
        new ApiResource("resource_payment")     {Scopes={"payment_fullpermission"}},
        new ApiResource("resource_gateway")     {Scopes={"gateway_fullpermission"}},
        new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
    };

    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
    {
        new IdentityResources.Email(),
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResource(){ Name="roles", DisplayName="Roles", Description="User Roles", UserClaims=new []{ "role" } }
    };

    public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
    {
        new ApiScope("catalog_fullpermission","Catalog API full access"),
        new ApiScope("photo_stock_fullpermission","Photo Stock API full access"),
        new ApiScope("basket_fullpermission","Basket API full access"),
        new ApiScope("discount_fullpermission","Discount API full access"),
        new ApiScope("order_fullpermission","Order API full access"),
        new ApiScope("payment_fullpermission","Payment API full access"),
        new ApiScope("gateway_fullpermission","Gateway API full access"),
        new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
    };

    public static IEnumerable<Client> Clients => new Client[]
    {
        new Client
        {
            ClientName="Asp.Net Core MVC",
            ClientId="WebMvcClient",
            ClientSecrets= {new Secret("secret".Sha256())},
            AllowedGrantTypes= GrantTypes.ClientCredentials,
            AllowedScopes=
            { 
                "catalog_fullpermission", 
                "photo_stock_fullpermission", 
                "gateway_fullpermission", 
                IdentityServerConstants.LocalApi.ScopeName 
            }
        },
        new Client
        {
            ClientName="Asp.Net Core MVC",
            ClientId="WebMvcClientForUser",
            AllowOfflineAccess= true,
            ClientSecrets= {new Secret("secret".Sha256())},
            AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,
            AllowedScopes=
            {
                "catalog_fullpermission",
                "basket_fullpermission",
                "photo_stock_fullpermission",
                "order_fullpermission", 
                "gateway_fullpermission",
                IdentityServerConstants.StandardScopes.Email, 
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile, 
                IdentityServerConstants.StandardScopes.OfflineAccess, 
                IdentityServerConstants.LocalApi.ScopeName,
                "roles"
            },
            AccessTokenLifetime=1*60*60,
            RefreshTokenExpiration=TokenExpiration.Absolute,
            AbsoluteRefreshTokenLifetime= (int) (DateTime.Now.AddDays(60)- DateTime.Now).TotalSeconds,
            RefreshTokenUsage= TokenUsage.ReUse
        },
        new Client
        {
            ClientName="Token Exchange",
            ClientId="TokenExchange",
            ClientSecrets= {new Secret("secret".Sha256())},
            AllowedGrantTypes= { OidcConstants.GrantTypes.TokenExchange },
            AllowedScopes=
            {
                "discount_fullpermission",
                "payment_fullpermission",
                IdentityServerConstants.StandardScopes.OpenId,
            }
        },
    };
}

