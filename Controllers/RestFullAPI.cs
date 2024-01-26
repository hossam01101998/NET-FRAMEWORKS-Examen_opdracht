using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2.Data;
using NET_FRAMEWORKS_EXAMEN_OPDRACHT.Models;

namespace NET_FRAMEWORKS_EXAMEN_OPDRACHT.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RestFullAPI : ControllerBase
    {
        private readonly Garage2Context _context;

        public RestFullAPI(Garage2Context context)
        {
            _context = context;
        }


        // create/edit
        [HttpPost]
        public JsonResult CreateEdit([FromBody] Appointment appointment)
        {
            if (appointment == null)
            {
                return new JsonResult("Error while creating appointment");
            }

            _context.Appointment.Add(appointment);
            _context.SaveChanges();

            return new JsonResult("Appointment created successfully");
        }

        //Get
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Appointment.Find(id);
            if (result == null)
            {
                return new JsonResult ("Appointment not found");
            }else
            {
                return new JsonResult(Ok(result));
            }
        }

        //Delete

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Appointment.Find(id);
            if (result == null)
            {
                return new JsonResult("Appointment not found");
            }
            else
            {
                _context.Appointment.Remove(result);
                _context.SaveChanges();
                return new JsonResult("Appointment deleted successfully");
            }
        }

        //GetAll

        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.Appointment.ToList();
            if (result == null)
            {
                return new JsonResult("Appointment not found");
            }
            else
            {
                return new JsonResult(Ok(result));
            }
        }
    }
}
