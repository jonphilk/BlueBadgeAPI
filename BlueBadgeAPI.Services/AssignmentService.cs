﻿using BlueBadgeAPI.Data;
using BlueBadgeAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeAPI.Services
{
    public class AssignmentService
    { 
        public AssignmentService()
        {
        }
        public bool AssignmentCreate(AssignmentCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var userEntity =
                    ctx
                        .Users
                        .Single(u => u.UserName == model.UserName);

                var newAssignment = new Assignment()
                {
                    UserId = userEntity.Id,
                    ProjectId = model.ProjectId,
                    TeamId = model.TeamId
                };

                ctx.Assignments.Add(newAssignment);
                return ctx.SaveChanges() == 1;

            }

        }
        public IEnumerable<AssignmentListItems> GetAssignments()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var collection = new List<AssignmentListItems>();
                foreach (var item in ctx.Assignments)
                {
                    var newAssignmentListItems = new AssignmentListItems
                    {
                        AssignmentId = item.AssignmentId,
                        UserId = item.UserId,
                        ProjectId = item.ProjectId,
                        TeamId = item.TeamId
                    };
                    collection.Add(newAssignmentListItems);
                }
                return collection;
            }
        }

        public AssignmentDetails GetAssignmentById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Assignments
                        .Single(e => e.AssignmentId == id);
                return
                    new AssignmentDetails
                    {
                        AssignmentId = entity.AssignmentId,
                        UserId = entity.UserId,
                        ProjectId = entity.ProjectId,
                        TeamId = entity.TeamId
                    };
            }
        }
        
        public bool UpdateAssignment(AssignmentEdit model)
        {

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Assignments
                        .Single(e => e.AssignmentId == model.AssignmentId);
                {

                    entity.ProjectId = model.ProjectId;
                    entity.TeamId = model.TeamId;

                }
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAssignment(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Assignments
                        .Single(e => e.AssignmentId == id);
                ctx.Assignments.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
