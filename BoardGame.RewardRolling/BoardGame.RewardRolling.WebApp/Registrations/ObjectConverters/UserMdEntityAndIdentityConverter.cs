using AutoMapper;
using BoardGame.RewardRolling.Core.Auth;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Registrations.ObjectConverters
{
    public class UserMdEntityToIdentityConverter : ITypeConverter<MdUser, ApplicationUser>
    {
        public ApplicationUser Convert(MdUser source, ApplicationUser destination, ResolutionContext context)
        {
            if (source == null)
                return null;
            destination = new ApplicationUser()
            {
                Id = source.Id.ToString(),
                UserName = source.UserName,
                Name = source.Name,
                PasswordSalt = source.PasswordSalt,
                HashedPassword = source.HashedPassword,
                AuthenticationType = "basic",
                ModifiedAt = source.ModifiedAt,
                CreatedAt = source.CreatedAt
            };
            return destination;
        }
    }

    public class UserIdentityToMdEntityConverter : ITypeConverter<ApplicationUser, MdUser>
    {
        public MdUser Convert(ApplicationUser source, MdUser destination, ResolutionContext context)
        {
            if (source == null)
                return null;

            destination = new MdUser()
            {
                Id = new ObjectId(source.Id),
                UserName = source.UserName,
                HashedPassword = source.HashedPassword,
                PasswordSalt = source.PasswordSalt,
                Name = source.Name,
                CreatedAt = source.CreatedAt,
                ModifiedAt = source.ModifiedAt
            };

            return destination;
        }
    }
}
