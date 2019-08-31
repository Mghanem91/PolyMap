using Newtonsoft.Json;
using PolygonAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolygonAPI.Utilities
{
    public class PolygonUtilities
    {
        public static string GetPoints(List<CoordinatesDTO> polygon)
        {
            var points = "";
            foreach (var point in polygon)
            {
                points = points + point.lat + "," + point.lng + "|";
            }
            return points.TrimEnd('|');
        }

        public static string GetCenterPoint(List<CoordinatesDTO> polygon)
        {
            //center should be calculated by spsfic alghorithm ??
            var firstPoint = polygon.First();
            return firstPoint.lat + "," + firstPoint.lng;
        }
        //TODO read from configrations file.
        private const string GoogleMapStaticUrl = "https://maps.googleapis.com/maps/api/staticmap?scale=2&key={0}&size=1500x2500&";

       public static string GetMapUrl(string coordinates,string apiKey)
        {
            var coordinateList = JsonConvert.DeserializeObject<List<CoordinatesDTO>>(coordinates);
            //TODO colors and other params to be added to configrations
            string queries = $"center={PolygonUtilities.GetCenterPoint(coordinateList)}&path=fillcolor:green|weight:5|{PolygonUtilities.GetPoints(coordinateList)}";
            return string.Format(GoogleMapStaticUrl, apiKey) + queries;

        }
    }
}
