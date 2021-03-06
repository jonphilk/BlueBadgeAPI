﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeAPI.Data
{
    public class NeededSkill
    {
        [Key]
        public int NeededSkillId { get; set; }

        [Required]
        public string Skill { get; set; }

        [ForeignKey(nameof(MotherProject))]
        public int ProjectId { get; set; }
        public virtual Project MotherProject { get; set; }
    }
}
