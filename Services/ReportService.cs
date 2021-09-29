using AspNetCore.Reporting;
using RdlcWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RdlcWebApi.Services
{
    public interface IReportService
    {
        byte[] GenerateReportAsync(string reportName);
    }

    public class ReportService : IReportService
    {
        public byte[] GenerateReportAsync(string reportName)
        {
            string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("WebAPI_RDLC.dll", string.Empty);
            string rdlcFilePath = string.Format("{0}ReportFiles\\{1}.rdlc", fileDirPath, reportName);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("utf-8");

            LocalReport report = new LocalReport(rdlcFilePath);

            // prepare data for report
            List<UserDto> userList = new List<UserDto>();

            var user1 = new UserDto { FirstName = "jp", LastName = "jan", Email = "jp@gm.com", Phone = "+976666661111", Age="23" };
            var user2 = new UserDto { FirstName = "jp2", LastName = "jan", Email = "jp2@gm.com", Phone = "+976666661111", Age = "33" };
            var user3 = new UserDto { FirstName = "jp3", LastName = "jan", Email = "jp3@gm.com", Phone = "+976666661111", Age = "43" };
            var user4 = new UserDto { FirstName = "jp4", LastName = "jan", Email = "jp4@gm.com", Phone = "+976666661111", Age = "53" };
            var user5 = new UserDto { FirstName = "jp5", LastName = "jan", Email = "jp5@gm.com", Phone = "+976666661111", Age = "63" };

            userList.Add(user1);
            userList.Add(user2);
            userList.Add(user3);
            userList.Add(user4);
            userList.Add(user5);

            report.AddDataSource("dsUsers", userList);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            var result = report.Execute(RenderType.Pdf, 1, parameters);

            return result.MainStream;            
        }


    }
}
