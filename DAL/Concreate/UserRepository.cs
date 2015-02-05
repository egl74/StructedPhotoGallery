using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Concreate.Mapping;
using DAL.Interface.Abstract;
using DAL.Interface.Entities;
using ORM;
using UserImage = ORM.Model.UserImage;

namespace DAL.Concreate
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        #region IRepository

        public UserRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
            context.Database.CreateIfNotExists();
            //AddRoleName("Пользователь");
            //Debug.WriteLine("repository create!");
        }

        public IEnumerable<User> Data
        {
            get
            {
                IEnumerable<ORM.Model.User> result = this.context.Set<ORM.Model.User>();
                return result.Select(u => u.ToDal()).ToList();
            }
        }

        public void Add(User item)
        {
            var result = item.ToOrm();
            result.Role = new ORM.Model.Role();
            //result.Role.RoleName = "Пользователь";
            this.context.Set<ORM.Model.User>().Add(result);
            try
            {
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Debug.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            
        }

        public void Delete(User item)
        {
            var result = this.GetOrmUser(item.Id.ToGuid());
            if (result != null)
            {
                this.context.Set<ORM.Model.Role>().Remove(result.Role);
                this.context.Set<ORM.Model.User>().Remove(result);
                this.context.SaveChanges();
            }
        }

        public void Update(User item)
        {
            var result = this.GetOrmUser(item.Id.ToGuid());
            if (result != null)
            {
                result.Password = item.Password;
                this.context.SaveChanges();
            }
        }
        
        #endregion

        public User GetUserByEmail(string email)
        {
            User result = null;
            var user = this.GetOrmUser(u => u.Email == email);
            if (user != null)
            {
                result = user.ToDal();
            }
            return result;
        }

        public User GetUser(string id)
        {
            User result = null;
            var user = this.GetOrmUser(id.ToGuid());
            if (user != null)
            {
                result = user.ToDal();
            }
            return result;
        }

        public Role GetUserRole(string email)
        {
            Role result = new Role();
            var user = this.GetOrmUser(u => u.Email == email);
            if (user != null && user.Role != null)
            {
                var roles = user.Role;
                result = roles.ToDal();
            }
            return result;
        }

        public void AddUserRole(string email, string roleName)
        {
            //var role = this.GetOrmRole(r => r.RoleName == roleName);
            var user = this.GetOrmUser(u => u.Email == email);
            if (/*role != null && */user != null)
            {
                if (user.Role == null) user.Role = new ORM.Model.Role();
                user.Role.RoleName = roleName;
                try
                {
                    this.context.SaveChanges();
                }
                catch
                {
                    
                    throw;
                }
                
            }
        }

        public void ChangeUserRole(string email, string roleName)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserRole(string email, string roleName)
        {
            throw new NotImplementedException();
        }

        public void AddRoleName(string roleName)
        {
            var query = this.context.Set<ORM.Model.Role>().Where(r => r.RoleName == roleName);
            if (query.Count() == 0)
            {
                ORM.Model.Role roleEntity = new ORM.Model.Role();
                roleEntity.RoleName = roleName;
                context.Set<ORM.Model.Role>().Add(roleEntity);
            }  
        }

        public bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAllRoles()
        {
            IEnumerable<ORM.Model.Role> result = this.context.Set<ORM.Model.Role>();
            return result.Select(r => r.ToDal()).ToList();
        }

        public bool ValidateUser(string email, string password)
        {
            bool isValid = false;

            var user = this.GetOrmUser(u => u.Email == email && u.Password == password);

            if (user != null) isValid = true;

            return isValid;
        }

        public IEnumerable<Interface.Entities.UserImage> GetUserImages(string email)
        {
            IEnumerable < Interface.Entities.UserImage > result= new List<Interface.Entities.UserImage>();
            var user = this.GetOrmUser(u => u.Email == email);
            if (user != null && user.UserImages != null)
            {
                var roles = user.UserImages;
                result = roles.Select(r => r.ToDal()).ToList();
            }
            return result;
        }

        public void AddUserImage(string email, string filePath, string title, string description)
        {
            var image = new Interface.Entities.UserImage();
            image.PhotoFilePath = filePath;
            image.Title = title;
            image.Description = description;
            var user = this.GetOrmUser(u => u.Email == email);
            if (user != null)
            {
                if (user.UserImages == null) user.UserImages = new List<ORM.Model.UserImage>();
                user.UserImages.Add(image.ToOrm());
                context.SaveChanges();
            }
        }

        public void DeleteUserImage(string email, int id)
        {
            var image = this.GetOrmImage(r => r.ImageId == id);
            var user = this.GetOrmUser(u => u.Email == email);
            if (image != null && user != null)
            {
                user.UserImages.Remove(image);
                context.SaveChanges();
            }
        }

        public IEnumerable<Interface.Entities.UserImage> GetAllImages()
        {
            IEnumerable<ORM.Model.UserImage> result = this.context.Set<ORM.Model.UserImage>();
            return result.Select(r => r.ToDal()).ToList();
        }
        
        private ORM.Model.User GetOrmUser(Guid userId)
        {
            ORM.Model.User result = null;
            var query = this.context.Set<ORM.Model.User>().Where(u => u.UserId == userId);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }

        private ORM.Model.User GetOrmUser(Expression<Func<ORM.Model.User, bool>> predicat)
        {
            ORM.Model.User result = null;
            var query = this.context.Set<ORM.Model.User>().Where(predicat);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }

        private ORM.Model.Role GetOrmRole(Expression<Func<ORM.Model.Role, bool>> predicate)
        {
            ORM.Model.Role result = null;
            var query = this.context.Set<ORM.Model.Role>().Where(predicate);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }

        private ORM.Model.UserImage GetOrmImage(Expression<Func<ORM.Model.UserImage, bool>> predicate)
        {
            ORM.Model.UserImage result = null;
            var query = this.context.Set<ORM.Model.UserImage>().Where(predicate);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }
    }
}
