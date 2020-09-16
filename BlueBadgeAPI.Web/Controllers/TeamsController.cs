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
            // When you get back from lunch, take out userId dependence
            //var userId = Guid.Parse(User.Identity.GetUserId());
            var TeamService = new TeamService();
            return TeamService;
        }

        /// <summary>
        /// Create a project team.
        /// </summary>
        public IHttpActionResult Post(TeamCreate Team)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTeamService();

            if (!service.TeamCreate(Team))
                return InternalServerError();

            return Ok("Team Created");
        }

        /// <summary>
        /// Get all teams.
        /// </summary>
        public IHttpActionResult Get()
        {
            TeamService TeamService = CreateTeamService();
            var teams = TeamService.GetTeams();
            return Ok(teams);
        }

        /// <summary>
        /// Get a team by Id.
        /// </summary>
        public IHttpActionResult Get(int id)
        {
            TeamService TeamService = CreateTeamService();
            var teams = TeamService.GetTeamById(id);
            return Ok(teams);
        }

        /// <summary>
        /// Update existing team.
        /// </summary>
        public IHttpActionResult Put(TeamDetails Team)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTeamService();

            if (!service.UpdateTeam(Team))
                return InternalServerError();

            return Ok("Team Updated");
        }

        /// <summary>
        /// Delete existing team.
        /// </summary>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateTeamService();

            if (!service.DeleteTeam(id))
                return InternalServerError();

            return Ok("Team Deleted");
        }
    }
}
