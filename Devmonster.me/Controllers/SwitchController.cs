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

        string connectionString = "Server=tcp:appupconso.database.windows.net,1433;Initial Catalog=appupconso;Persist Security Info=False;User ID=ran;Password=..Shadowsong13..;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // GET: Switch
        public ActionResult Index()
        {
            string queryString = "SELECT ExeName, Sum(ActiveTime) [ActiveTime], Max(DateStamp) FROM AppDataAll WHERE PCName in ('PikaSwitch', 'Switch', 'Pokemon Switch') GROUP BY ExeName ORDER BY Max(dateStamp) DESC";
            string supportQuery = "IF EXISTS (SELECT * FROM NowPlaying) BEGIN SELECT TOP 1 GameName[Value], '1'[IsPlaying] FROM NowPlaying END ELSE BEGIN SELECT Max(DateStamp)[Value], '0'[IsPlaying] From AppHistory WHERE PCName in ('PikaSwitch', 'Switch', 'Pokemon Switch') END";

            SqlConnection conn = new SqlConnection();
            SqlCommand command = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            conn.ConnectionString = connectionString;
            conn.Open();

            DataTable dtMainList = new DataTable();
            DataTable dtSupport = new DataTable(); 

            command.Connection = conn;
            command.CommandType = CommandType.Text;
            command.CommandText = queryString;

            da.SelectCommand = command;
            da.Fill(dtMainList);

            command.CommandText = supportQuery;
            da.SelectCommand = command;
            da.Fill(dtSupport);

            command.Dispose();
            conn.Dispose();

            List<Models.NintendoGameItem> allitems = new List<Models.NintendoGameItem>();

            if (dtMainList != null && dtMainList.Rows.Count > 0)
            {
                foreach (DataRow row in dtMainList.Rows)
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
                        totalHours = Convert.ToInt32(seconds / 60 / 60).ToString() + " hour" + ((seconds >= 0 && seconds < 3600) ? "" : "s") + " or more";
                    }


                    detail.GameHours = totalHours;
                    detail.IsHighlighted = seconds >= 180000;

                    allitems.Add(detail);
                }
            }

            Models.SwitchViewModel model = new Models.SwitchViewModel();
            model.Items = allitems;

            if (dtSupport.Rows.Count > 0)
            {
                bool isPlaying = dtSupport.Rows[0]["IsPlaying"].ToString() == "1";
                string value = dtSupport.Rows[0]["Value"].ToString();

                if (isPlaying)
                {
                    model.isPlaying = true;
                    model.NoteText = value;
                }
                else
                {
                    model.isPlaying = false;

                    DateTime lastPlayed = DateTime.Parse(value);
                    DateTime now = DateTime.Now.AddHours(8);
                    double sinceMinutes = (now - lastPlayed).TotalMinutes;

                    if (sinceMinutes < 2)
                    {
                        model.NoteText = $"Last online just now";
                    }
                    else if (sinceMinutes < 60)
                    {
                        model.NoteText = $"Last online { Convert.ToInt32(sinceMinutes) } minutes ago";
                    }
                    else if (sinceMinutes >= 60 && sinceMinutes < 120)
                    {
                        model.NoteText = $"Last online an hour ago";
                    }
                    else if (sinceMinutes < 1440)
                    {
                        model.NoteText = $"Last online {  Convert.ToInt32((now - lastPlayed).TotalHours) } hours ago";
                    }
                    else if (sinceMinutes == 1440)
                    {
                        model.NoteText = $"Last online a day ago";
                    }
                    else
                    {
                        model.NoteText = $"Last online {  Convert.ToInt32((now - lastPlayed).TotalDays) } days ago";
                    }

                }
            }

            return View(model);
        }
    }
}