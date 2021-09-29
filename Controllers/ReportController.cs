using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RdlcWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RdlcWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            string reportName = "UserDetails";
            var reportFileByteString = _reportService.GenerateReportAsync(reportName);
            return File(reportFileByteString, MediaTypeNames.Application.Octet, reportName + ".PDF");
        }

    }
}
