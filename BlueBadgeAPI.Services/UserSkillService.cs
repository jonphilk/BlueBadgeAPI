﻿using BlueBadgeAPI.Data;
using BlueBadgeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeAPI.Services
{
    public class UserSkillService
    {
        private readonly string _userId;

        public UserSkillService(string userSkillId)
        {
            _userId = userSkillId;
        }

        public bool UserSkillCreate(UserSkillCreate model)
        {
            var newUserSkill = new UserSkill()
            {
                Skill = model.Skill,
                UserId = _userId
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.UserSkills.Add(newUserSkill);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<UserSkillListItems> GetUserSkills()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .UserSkills
                    .Where(e => e.UserId == _userId)
                    .Select(
                        e =>
                        new UserSkillListItems
                        {
                            Skill = e.Skill,
                            UserSkillId = e.UserSkillId
                        }
                        );
                return query.ToArray();
            }
        }

        public UserSkillDetails GetUserSkillById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserSkills
                        .Single(e => e.UserId == _userId);
                return
                    new UserSkillDetails
                    {
                        UserSkillId = entity.UserSkillId,
                        Skill = entity.Skill
                    };
            }
        }

        public bool UpdateUserSkill(UserSkillDetails model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserSkills
                        .Single(e => e.UserSkillId == model.UserSkillId);
                model.Skill = entity.Skill;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteUserSkill(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserSkills
                        .Single(e => e.UserId == _userId);
                ctx.UserSkills.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
