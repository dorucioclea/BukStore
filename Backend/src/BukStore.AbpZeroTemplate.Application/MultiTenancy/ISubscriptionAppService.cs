﻿using System.Threading.Tasks;
using Abp.Application.Services;

namespace BukStore.AbpZeroTemplate.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task UpgradeTenantToEquivalentEdition(int upgradeEditionId);
    }
}
