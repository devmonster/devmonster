using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Devmonster.me.Controllers
{
    public class SwitchController : Controller
    {

        string connectionString = "Server=tcp:appupconso.database.windows.net,1433;Initial Catalog=appupconso_Copy;Persist Security Info=False;User ID=ran;Password=..Shadowsong13..;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // GET: Switch
        public ActionResult Index()
        {
            string queryString = "SELECT ExeName, Sum(ActiveTime) [ActiveTime], Max(DateStamp) FROM AppDataAll WHERE PCName in ('PikaSwitch', 'Switch', 'Pokemon Switch') GROUP BY ExeName ORDER BY Max(dateStamp) DESC";

            SqlConnection conn = new SqlConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            conn.ConnectionString = connectionString;
            conn.Open();

            DataTable dt = new DataTable();

            command.Connection = conn;
            command.CommandType = CommandType.Text;
            command.CommandText = queryString;

            da.SelectCommand = command;
            da.Fill(dt);

            command.Dispose();
            conn.Dispose();

            List<Models.NintendoGameItem> allitems = new List<Models.NintendoGameItem>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    Models.NintendoGameItem detail = new Models.NintendoGameItem();

                    detail.GameName = row["ExeName"].ToString().Replace("&apos;", "'");

                    double seconds = Convert.ToDouble(row["ActiveTime"]);


                    string totalHours;

                    if (seconds < 3600)
                    {
                        totalHours = Convert.ToInt32(seconds / 60).ToString() + " minutes or more";
                    }           
                    else
                    {
                        totalHours = Convert.ToInt32(seconds / 60 / 60).ToString() + " hour" + ((seconds >= 3600 && seconds < 7200) ? "" : "s") + " or more";
                    }


                    detail.GameHours = totalHours;
                    detail.IsHighlighted = seconds >= 180000;

                    allitems.Add(detail);
                }
            }

            Models.SwitchViewModel model = new Models.SwitchViewModel();
            model.Items = allitems;

            return View(model);
        }
    }
}