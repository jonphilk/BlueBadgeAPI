﻿using BlueBadgeAPI.Models;
using BlueBadgeAPI.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlueBadgeAPI.Web.Controllers
{
    public class UserSkillsController : ApiController
    {
        private UserSkillService CreateUserSkillService()
        {
            var userSkillId = (User.Identity.GetUserId());
            var UserSkillService = new UserSkillService(userSkillId);
            return UserSkillService;
        }
        private LogService CreateLogService()
        {
            var logService = new LogService();
            return logService;
        }

        //Post
        [Route("api/UserSkills")]
        [HttpPost]
        /// <summary>
        /// Add skill to a user account.
        /// </summary>
        public IHttpActionResult Post(UserSkillCreate userSkill)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateUserSkillService();

            if (!service.UserSkillCreate(userSkill))
                return InternalServerError();

            string newLog = "Skill Created";
            var logService = CreateLogService();
            logService.LogCreate(newLog);

            return Ok(newLog);
        }

        /// <summary>
        /// Get all user skills.
        /// </summary>
        [Route("api/UserSkills")]
        public IHttpActionResult Get()
        {
            UserSkillService UserSkillService = CreateUserSkillService();
            var userSkill = UserSkillService.GetUserSkills();

            string newLog = "All Skills Recieved";
            var logService = CreateLogService();
            logService.LogCreate(newLog);

            return Ok(userSkill);
        }

        /// <summary>
        /// Update existing user skill.
        /// </summary>
        [Route("api/UserSkills")]
        public IHttpActionResult Put(UserSkillDetails userSkill)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateUserSkillService();

            if (!service.UpdateUserSkill(userSkill))
                return InternalServerError();

            string newLog = "Skill Updated";
            var logService = CreateLogService();
            logService.LogCreate(newLog);

            return Ok(newLog);
        }

        /// <summary>
        /// Delete existing user skill.
        /// </summary>
        [Route("api/UserSkills/{userSkillId}")]
        public IHttpActionResult Delete(int userSkillId)
        {
            var service = CreateUserSkillService();

            if (!service.DeleteUserSkill(userSkillId))
                return InternalServerError();

            string newLog = "Skill Delete";
            var logService = CreateLogService();
            logService.LogCreate(newLog);

            return Ok(newLog);
        }

        public LogListItems LogData()
        {
            var logOne = new LogListItems();
            return logOne;
        }
    }
}
