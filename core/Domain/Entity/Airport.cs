using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Airport
    {
        [Description("airport_code")]
        public string AirportCode { get; set; }

        [Description("airport_name")]
        public string AirportName { get; set; }

        [Description("city")]
        public string City { get; set; }

        [Description("coordinates")]
        public string Coordinates { get; set; }

        [Description("timezone")]
        public string Timezone { get; set; }
    }
}