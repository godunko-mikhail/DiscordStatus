﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordStatus.UI
{
    public class InputModel
    {
        public string? AppId { get; set; }

        public string? LargeImage { get; set; }
        public string? LargeText { get; set; }
        public string? SmallImage { get; set; }
        public string? SmallText { get; set; }

        public string? Details { get; set; }
        public string? State { get; set; }
    }
}
