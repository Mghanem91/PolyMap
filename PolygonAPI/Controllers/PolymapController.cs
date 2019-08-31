using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PolygonAPI.DTOs;
using PolygonAPI.Models;
using PolygonAPI.Services;
using PolygonAPI.Utilities;
using PolygonAPI.Utilities.Entities;

namespace PolygonAPI.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PolymapController : ControllerBase
    {
        private readonly IPolymapServices _polymapService;

        public PolymapController(IPolymapServices serv)
        {
            _polymapService = serv;
        }

        // GET api/values
        //[HttpPost]
        [HttpGet]
        public async Task<IActionResult> Export(string data)
        {
            int[] arr = null;
            try
            {
                arr = Array.ConvertAll(data.Split(","), id => int.Parse(id));
            }
            catch (NullReferenceException ex)
            {
                //TODO log ex
                return StatusCode(500, "data parameter cannot be empty");
            }
            catch (FormatException ex)
            {
                //TODO log ex
                return StatusCode(500, "data should be numeric polygons ids seprated  by comma");
            }

            ConcurrentBag<FileEntity> fileList = new ConcurrentBag<FileEntity>();

            var polygonsRows = await _polymapService.ListPolygonsByIds(arr);

           

            Parallel.ForEach(polygonsRows, polygon =>
           {
               //TODO api key should retrived from database or configration file.
               fileList.Add(_polymapService.GetPdfFileForPolygon(PolygonUtilities.GetMapUrl(polygon.LocationCoordinates, "{api_key}"), polygon.Id));
           });

            var response = PrepareFileContentRersult(fileList);
            if(response != null)
            {
                return response;
            }

             return StatusCode(404, "All polygon ids provided is not exists.");

        }

        private FileContentResult PrepareFileContentRersult(ConcurrentBag<FileEntity> fileList)
        {
            if (fileList.Count > 0)
            {
                const string contentType = "application/zip";
                HttpContext.Response.ContentType = contentType;
                var result = new FileContentResult(fileList.Compress(), contentType)
                {
                    FileDownloadName = "Polygons.zip"
                };
                return result;
            }
            return null;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PolygonDTO polygonDto)
        {
            var valid = polygonDto != null && polygonDto.CoordinateList.Count > 0; // TODO extension method or read only property(isvalid)
            if (valid)
            {
                try
                {
                    var polygon = new Polygons { InsertDate = DateTime.Now, LocationCoordinates = JsonConvert.SerializeObject(polygonDto.CoordinateList), UserId = polygonDto.UserId };
                    await _polymapService.CreatePolygon(polygon);
                    return StatusCode(200, "Polygon created successfully");

                }
                catch (Exception ex)
                {
                    //TODO log the critical exception
                    return StatusCode(500, "Error occured during save");
                }
            }
            //log invalid request
            return StatusCode(500, "Invalid request body.");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _polymapService.RemovePolygon(id);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                //TODO log the critical exception
                return StatusCode(500, "Error occured during delete process");
            }
        }
    }
}

