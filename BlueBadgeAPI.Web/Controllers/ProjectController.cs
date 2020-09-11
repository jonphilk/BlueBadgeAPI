﻿using BlueBadgeAPI.Data;
using BlueBadgeAPI.Models;
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
    public class ProjectController : ApiController
    {
        string log = "";
        private ProjectService CreateProjectService()
        {
            var userId = User.Identity.GetUserId();
            var projectService = new ProjectService(userId);
            return projectService;
        }
        
        //Post
        public IHttpActionResult Post(ProjectCreate project)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateProjectService();

            if (!service.ProjectCreate(project))
                return InternalServerError();
            string newLog = log + "Created New Project";
            log = newLog;
            return Ok(log);

        }

        //Get
        public IHttpActionResult Get()
        {
            ProjectService projectService = CreateProjectService();
            var projects = projectService.GetProjects();
            string newLog = log + "Got Projects";
            log = newLog;
            return Ok(log);
        }
        public IHttpActionResult GetAll()
        {
            ProjectService projectService = CreateProjectService();
            var projects = projectService.GetAllProjects();
            return Ok(projects);
        }

        //Get
        public IHttpActionResult Get(int id)
        {
            ProjectService projectService = CreateProjectService();
            var projects = projectService.GetProjectById(id);
            return Ok(projects);
        }

        //Put
        public IHttpActionResult Put(ProjectEdit project)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateProjectService();

            if (!service.UpdateProject(project))
                return InternalServerError();

            return Ok();
        }

        
        //Delete
        public IHttpActionResult Delete(int id)
        {
            var service = CreateProjectService();

            if (!service.DeleteProject(id))
                return InternalServerError();

            return Ok();
        }
    }
}
