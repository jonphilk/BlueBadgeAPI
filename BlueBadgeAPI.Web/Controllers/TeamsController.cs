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
    public class TeamsController : ApiController
    {
        private TeamService CreateTeamService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var TeamService = new TeamService(userId);
            return TeamService;
        }

        private LogService CreateLogService()
        {
            var logService = new LogService();
            return logService;
        }

        //Post
        [Route("api/Teams")]
        [HttpPost]
        public IHttpActionResult Post(TeamCreate Team)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTeamService();

            if (!service.TeamCreate(Team))
                return InternalServerError();

            string newLog = "Team Created";
            var logService = CreateLogService();
            logService.LogCreate(newLog);

            return Ok(newLog);
        }

        //Get
        public IHttpActionResult Get()
        {
            TeamService TeamService = CreateTeamService();
            var Teams = TeamService.GetTeams();

            string newLog = "All Teams Recieved";
            var logService = CreateLogService();
            logService.LogCreate(newLog);

            return Ok(Teams);
        }
        //public IHttpActionResult GetAll()
        //{
        //    TeamService TeamService = CreateTeamService();
        //    var Teams = TeamService.GetTeams();

        //    string newLog = "Team Deleted";
        //    var logService = CreateLogService();
        //    logService.LogCreate(newLog);

        //    return Ok(newLog);
        //}

        //Get
        public IHttpActionResult Get(int id)
        {
            TeamService TeamService = CreateTeamService();
            var Teams = TeamService.GetTeamById(id);

            string newLog = "Team Recieved By Id";
            var logService = CreateLogService();
            logService.LogCreate(newLog);

            return Ok(Teams);
        }

        //Put
        public IHttpActionResult Put(TeamDetails Team)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTeamService();

            if (!service.UpdateTeam(Team))
                return InternalServerError();

            string newLog = "Team Updated";
            var logService = CreateLogService();
            logService.LogCreate(newLog);

            return Ok(newLog);
        }


        //Delete
        public IHttpActionResult Delete(int id)
        {
            var service = CreateTeamService();

            if (!service.DeleteTeam(id))
                return InternalServerError();

            string newLog = "Team Deleted";
            var logService = CreateLogService();
            logService.LogCreate(newLog);

            return Ok(newLog);
        }
    }
}
