﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Web.Dtos.Expert.Team
{
    public class FinishTeamDto
    {
        public long TeamId { get; set; }
        public int Mark { get; set; }
        public string Comment { get; set; }
    }
}