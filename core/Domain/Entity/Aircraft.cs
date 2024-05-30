using System.ComponentModel;

namespace Domain.Entity
{
    public class Aircraft
    {
        [Description("aircraft_code")]
        public string AircraftCode { get; set; }

        [Description("model")]
        public string Model { get; set; }

        [Description("range")]
        public int Range { get; set; }
    }
}