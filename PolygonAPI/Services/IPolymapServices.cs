using PolygonAPI.Models;
using PolygonAPI.Utilities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolygonAPI.Services
{
    public interface IPolymapServices
    {
        Task<List<Polygons>> ListPolygonsByIds(int[] ids);
        Task<Polygons> CreatePolygon(Polygons polygon);
        FileEntity GetPdfFileForPolygon(string mapUrl, int polygonId);
        Task RemovePolygon(int Id);
    }
}