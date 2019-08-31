using System.Collections.Generic;

namespace PolygonAPI.DTOs
{
    public class PolygonDTO
    {
        public List<CoordinatesDTO> CoordinateList { set; get; }
        public int UserId { set; get; }

    }
}
