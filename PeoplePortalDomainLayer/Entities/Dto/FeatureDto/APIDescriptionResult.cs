using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeoplePortalDomainLayer.Entities.Dto.FeatureDto
{
    public class APIDescriptionResult
    {
        public string Method { get; set; }
        public string RelativePath { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
    }
}