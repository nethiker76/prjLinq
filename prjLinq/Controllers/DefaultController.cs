using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using prjLinq.Models;
using GroupViewModelSample.Models;

namespace prjLinq.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Prefix = "login")]LoginViewModels model)
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModels model)
        {
            return View();
        }

        public ActionResult Combine()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Combine(GroupViewModels model)
        {
            return View();
        }
    
    //constr連結字串
    string constr = @"Data Source=(localdb)\MSSQLLocalDB;" +
            "AttachDbFilename=|DataDirectory|dbStudent.mdf;" +
            "Integrated Security=True";

       // public object model { get; private set; }

        //executeSql方法可傳入SQL字串
        private void executeSql(string sql)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = constr;
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        //querysql傳入SQL字串
        private DataTable querysql(string sql)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = constr;
            SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            return ds.Tables[0];
        }
        public ActionResult queryindex()
        {
            DataTable dt = querysql("select * from tstudent");
            return View(dt);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create
            (string fStuId, string fName, string fEmail, int fScore)
        {
            if (ModelState.IsValid)
            {
                string sql = "insert into tStudent(fStuId,fName,fEmail,fScore) values('"
                    + fStuId.Replace("'", "''") + "',N'"
                    + fName.Replace("'", "''") + "','"
                    + fEmail.Replace("'", "''") + "','"
                    + fScore + "')";
                executeSql(sql);
                return RedirectToAction("queryindex");
            }
            return View();
        }

        public ActionResult Edit(string id)
        {
            string sql = "SELECT * FROM tStudent WHERE fStuId='"
                + id.Replace("'", "''") + "'";
            DataTable dt = querysql(sql);
            return View(dt);
        }

        [HttpPost]
        public ActionResult Edit(string fStuId, string fName, string fEmail, int fScore)
        {
            string sql = "UPDATE tStudent SET fName=N'" + fName
                + "', fEmail='" + fEmail
                + "', fScore=" + fScore
                + " WHERE fStuid='" + fStuId.Replace("'", "''") + "'";
            executeSql(sql);
            return RedirectToAction("queryindex");
        }

        public ActionResult delete(string id)
        {
            string sql = "Delete from tStudent where fStuid='" + id.Replace("'","''") + "'";
            executeSql(sql);
            DataTable dt = querysql("select * from tStudent");
            return RedirectToAction("queryindex");
        }
        // GET: Default
        public ActionResult showproduct()
        {
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbConn"].ConnectionString);
            conn.Open();
            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand("select top 20  productname,quantityperunit,unitprice from products", conn);
            try
            {

                dr = cmd.ExecuteReader();
                Response.Write("<table width='90%' border=1>");
                while (dr.Read())
                {
                    Response.Write("<tr>");
                    Response.Write("<td>" + dr["productname"] + "</td>");
                    Response.Write("<td>" + dr["quantityperunit"] + "</td>");
                    Response.Write("<td>" + dr["unitprice"] + "</td>");
                    Response.Write("</tr>");
                }
                Response.Write("</table>");
                ViewBag.tet = Content("fssdfdsfdsfsd");
                ViewData["namdsa"] = Content("fssdfdsfdsfsd");
                TempData["dfsfdsfs"] = Content("fssdfdsfdsfsd");
              }
              catch (Exception ex)
              {
                  Response.Write("Error Message----<br>" + ex.ToString() + "</br>");
                  //throw;
              }
              finally
              {
                  // == 第四，釋放資源、關閉資料庫的連結。
                  if (dr != null)
                  {
                      cmd.Cancel();
                      dr.Close();
                  }
              }
              if (conn.State == ConnectionState.Open)
              {
                  conn.Close();
                  conn.Dispose();
              }
            return View();
        }
        public string showproduct2()
        {
            NorthwindEntities1 db = new NorthwindEntities1();
            var result = db.Products.Where(m => m.UnitPrice > 30).OrderBy(m => m.UnitPrice).ThenByDescending(m => m.UnitsInStock);
            //var result = from m in db.Products where m.UnitPrice > 30 orderby m.UnitsInStock descending select m;
            var result1 = db.Products;
            string show1 = "";
            
            show1 += "總數量 : "+ result1.Count();
            show1 += "均價 : " + result1.Average(m=>m.UnitPrice);
            show1 += "最高價 : " + result1.Max(m => m.UnitPrice);
            show1 += "總價 : " + result1.Sum(m => m.UnitPrice) + "<hr>";

            string show = "";
            foreach (var m in result)
            {
                show += "品名 :" + m.ProductName + "<br>";
                show += "價格 : " + m.UnitPrice + "<br>";
                show += "庫存量 : " + m.UnitsInStock + "<hr>";
            }
            string show3 = show1 + show;
            return show3;
        }

    }
}