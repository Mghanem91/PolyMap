using PolygonAPI.Models;
using PolygonAPI.Utilities.Entities;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolygonAPI.Services
{
    public class PolymapServices : IPolymapServices
    {

        private readonly MapPolygonContext _context;

        public PolymapServices(MapPolygonContext ctx)
        {
            _context = ctx;
        }


        public FileEntity GetPdfFileForPolygon(string mapUrl, int polygonId)
        {
            var myHtml = $"<img style='width:2480;height:3500' src='{mapUrl}' />";
            var doc = new HtmlToPdf().ConvertHtmlString(myHtml);
            var data = doc.Save();
            doc.Close();
            return new FileEntity { FileName = polygonId + ".pdf", FileStream = data, FileType = "pdf" };
        }



        public async Task RemovePolygon(int Id)
        {
            _context.Polygons.Remove(new Polygons { Id = Id });
            await _context.SaveChangesAsync();
        }

        public async Task<List<Polygons>> ListPolygonsByIds(int[] polygonsIds)
        {
            return await Task.Run(() => _context.Polygons.Where(a => polygonsIds.Contains(a.Id)).ToList());
        }

        public async Task<Polygons> CreatePolygon(Polygons polygon)
        {
             await _context.Polygons.AddAsync(polygon);
                _context.SaveChanges();
            return polygon;
         }


    }
}
