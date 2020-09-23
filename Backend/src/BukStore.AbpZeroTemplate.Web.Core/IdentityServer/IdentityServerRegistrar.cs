﻿using Abp.IdentityServer4;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BukStore.AbpZeroTemplate.Authorization.Users;
using BukStore.AbpZeroTemplate.EntityFrameworkCore;

namespace BukStore.AbpZeroTemplate.Web.IdentityServer
{
    public static class IdentityServerRegistrar
    {
        public static void Register(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients(configuration))
                .AddAbpPersistedGrants<AbpZeroTemplateDbContext>()
                .AddAbpIdentityServer<User>();
        }
    }
}
