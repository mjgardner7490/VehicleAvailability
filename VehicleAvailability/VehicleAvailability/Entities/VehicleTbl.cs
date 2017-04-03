using System;
using System.Collections.Generic;

namespace VehicleAvailability.Entities
{
    public partial class VehicleTbl
    {
        public string VehicleVin { get; set; }
        public string VehicleName { get; set; }
        public string VehicleCity { get; set; }
        public string VehicleStatus { get; set; }
        public DateTime VehicleLastupdate { get; set; }
    }
}
