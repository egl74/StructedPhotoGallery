using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BLL.Interface.Abstract;
using BLL.Interface.Entities;

namespace MvcUI.Infrastructure
{
    public class PhotoGalleryRoleProvider : RoleProvider
    {
        private readonly IUserRolesQueryService userRolesQueryService;

        private readonly IUserQueryService queryService;

        public PhotoGalleryRoleProvider()
            : this(
                (IUserRolesQueryService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserRolesQueryService)),
                (IUserQueryService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserQueryService))
                ) { }

        public PhotoGalleryRoleProvider(IUserRolesQueryService userRolesQueryService, IUserQueryService queryService)
        {
            this.userRolesQueryService = userRolesQueryService;
            this.queryService = queryService;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return userRolesQueryService.IsUserInRole(username, roleName);
        }

        public override string[] GetRolesForUser(string email)
        {
            string[] role = new string[] { };
                try
                {
                    // Получаем пользователя
                    User user = queryService.GetUserByEmail(email);
                    if (user != null)
                    {
                        role = new string[] {user.Role.Name};
                    }
                }
                catch
                {
                    role = new string[] { };
                }
            return role;
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}