using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.latijn
{
    public class WoordDto
    {
        public int Id { get; set; }
        public CaputDto Caput { get; set; }
        public string Term { get; set; }
        public string Genus { get; set; }
        public List<VertalingDto> Vertalingen { get; set; }
        public List<AanvInfoDto> AanvInfo { get; set; }
    }
}