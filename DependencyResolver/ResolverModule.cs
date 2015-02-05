using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Concreate.Services;
using BLL.Concreate.Services.Role_services;
using BLL.Interface.Abstract;
using DAL.Concreate;
using DAL.Interface.Abstract;
using Ninject.Modules;
using ORM;

namespace DependencyResolver
{
    public class ResolverModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<PhotoGalleryDbContext>().InSingletonScope();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IUserCreationService>().To<UserCreationService>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IUserQueryService>().To<UserQueryService>();
            Bind<IUserSecurityService>().To<UserSecurityService>();
            Bind<IRoleQueryService>().To<RoleQueryService>();
            Bind<IUserRolesManagementService>().To<UserRolesManagementService>();
            Bind<IUserRolesQueryService>().To<UserRolesQueryService>();
            Bind<IImageService>().To<ImageService>();
        }
    }
}
