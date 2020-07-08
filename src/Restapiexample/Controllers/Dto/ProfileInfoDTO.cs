using System.Collections.Generic;
using Newtonsoft.Json;

namespace Compuletra.RestApiExample.Controllers.Dto {
    public class ProfileInfoDTO {
        [JsonProperty("display-ribbon-on-profiles")]
        public string DisplayRibbonOnProfiles { get; set; }

        [JsonProperty("activeProfiles")]            
        public List<string> ActiveProfiles { get; set; }

        public ProfileInfoDTO(string displayRibbonOnProfiles, List<string> activeProfiles)
        {
            DisplayRibbonOnProfiles = displayRibbonOnProfiles;
            ActiveProfiles = activeProfiles;

        }
    }
}
