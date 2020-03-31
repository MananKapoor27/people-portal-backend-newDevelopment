using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PeoplePortalDomainLayer.Entities.DTO.SuccessStatusDTO
{
    /// <summary>
    /// this class is used for generating success response
    /// </summary>    
    public class SuccessStatusDto
    {
        /// <summary>
        /// this property is used to get and set status
        /// </summary>        
        public string Status { get; set; } = "success";
        /// <summary>
        /// this property is used to get and set data in the success response
        /// </summary>       
        public Object Data { get; set; }
        /// <summary>
        /// this property is used to get and set records in the success response
        /// </summary>
        [JsonProperty("record", NullValueHandling = NullValueHandling.Ignore)]
        public RecordsDto Record { get; set; }
        /// <summary>
        /// this property is used to get and set filters in the success response
        /// </summary>
        [JsonProperty("filter", NullValueHandling = NullValueHandling.Ignore)]
        public List<FilterDto> Filter { get; set; }//because we can have filter on multiple filters 
        // we can add filter col inside filter list while applying filters inside controller
        /// <summary>
        /// this property is used to get and set search in the success response
        /// </summary>
        [JsonProperty("search", NullValueHandling = NullValueHandling.Ignore)]
        public SearchDto Search { get; set; }
        /// <summary>
        /// this property is used to get and set sort in the success response
        /// </summary>
        [JsonProperty("sort", NullValueHandling = NullValueHandling.Ignore)]
        public SortDto Sort { get; set; }
    }
}
