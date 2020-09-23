﻿using Abp.Application.Services;
using Abp.Dependency;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.Threading;
using BukStore.AbpZeroTemplate;
using BukStore.AbpZeroTemplate.Authorization.Users;
using BukStore.AbpZeroTemplate.Helpers;
using BukStore.AbpZeroTemplate.MultiTenancy;
using BSWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BSWebsite.AbpZeroTemplate.Application
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class BSWebsiteAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected IProcedureHelper procedureHelper;

        protected BSWebsiteAppServiceBase()
        {
            LocalizationSourceName = AbpZeroTemplateConsts.LocalizationSourceName;
            procedureHelper = IocManager.Instance.IocContainer.Resolve<IProcedureHelper>();
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected async Task<string> GetCurrentUserName()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if(user==null)
            {
                return "no-user";
            }

            return user.Name;
        }


        protected void SetAuditInsert(FullAuditModel entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = GetCurrentUser().Name;
            entity.IsDelete = false;
        }

        protected void SetAuditEdit(FullAuditModel entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = GetCurrentUser().Name;
        }

        protected virtual User GetCurrentUser()
        {
            return AsyncHelper.RunSync(GetCurrentUserAsync);
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
            }
        }

        protected virtual Tenant GetCurrentTenant()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetById(AbpSession.GetTenantId());
            }
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}