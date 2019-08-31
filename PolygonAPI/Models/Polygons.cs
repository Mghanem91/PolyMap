using System;
using System.Collections.Generic;

namespace PolygonAPI.Models
{
    public partial class Polygons
    {
        public int Id { get; set; }
        public string LocationCoordinates { get; set; }
        public DateTime InsertDate { get; set; }
        public int? UserId { set; get; }
    }
}
