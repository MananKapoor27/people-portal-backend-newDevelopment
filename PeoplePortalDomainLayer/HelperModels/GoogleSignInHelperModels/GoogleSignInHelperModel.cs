﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PeoplePortalDomainLayer.HelperModels.GoogleSignInHelperModels
{
    public class GoogleAuthRequestData
    {
        public string grant_type { get; set; }

        public string code { get; set; }

        public string client_id { get; set; }

        public string client_secret { get; set; }

        public string redirect_uri { get; set; }
    }

    public class GoogleAuthResponseData
    {
        public string access_token { get; set; }

        public string expires_in { get; set; }
        
        public string scope { get; set; }
        
        public string token_type { get; set; }
        
        public string id_token { get; set; }
    }

    public class GoogleSignInDto
    {
        public string tokenString { get; set; }
    }
}
