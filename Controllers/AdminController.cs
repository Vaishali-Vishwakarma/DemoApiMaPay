using ApiMaPay.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ApiMaPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class adminController : ControllerBase
    {
        SqlConnection conn;
        private readonly IConfiguration _configuration;

        public adminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /*public AdminDetails GetAdmin()
        {
            AdminDetails admin = new AdminDetails();
            admin.result = new Result();

            try
            {
                conn = new SqlConnection(_configuration["ConnectionStrings:ApiMaPayConnectionString"]);
                    using (conn)
                    {
                        SqlCommand cmd = new SqlCommand("sp_getadmin", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@email", admin.useremail);
                        //cmd.Parameters.AddWithValue("@password", admin.password);
                        //cmd.Parameters.AddWithValue("@stmttype", "adminlogin");
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            admin.id = Convert.ToInt32(dt.Rows[0]["id"]);

                            admin.result.result = true;
                            admin.result.message = "success";
                        }
                        else
                        {
                            admindetails.result.result = false;
                            admindetails.result.message = "Invalid user";
                        }
                    }
                
            }
            catch (Exception ex)
            {
                admindetails.result.result = false;
                admindetails.result.message = "Error occurred: " + ex.Message.ToString();
            }
            return admindetails;
        }*/

        //, Route("[action]", Name ="Login")]
 
        
        [HttpPost]
        //public AdminDetails login(Admin admin)
        public IActionResult login(Admin admin)
        {
            AdminDetails admindetails = new AdminDetails();
            admindetails.result = new Result();
            try
            {
                if(admin != null && !string.IsNullOrWhiteSpace(admin.useremail) && !string.IsNullOrWhiteSpace(admin.password))
                {
                    conn = new SqlConnection(_configuration["ConnectionStrings:ApiMaPayConnectionString"]);
                    using (conn)
                    {
                        SqlCommand cmd = new SqlCommand("sp_adminlogin", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", admin.useremail);
                        cmd.Parameters.AddWithValue("@password", admin.password);
                        cmd.Parameters.AddWithValue("@stmttype", "adminlogin");
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if(dt != null && dt.Rows.Count > 0)
                        {
                            admindetails.id = Convert.ToInt32(dt.Rows[0]["id"]);

                            admindetails.result.result = true;
                            admindetails.result.message = "success";
                        }
                        else
                        {
                            admindetails.result.result = false;
                            admindetails.result.message = "Invalid user";
                            return BadRequest("Invalid user");
                        }
                    }
                }
                else
                {
                    admindetails.result.result = false;
                    admindetails.result.message = "Please enter email and password";
                    return BadRequest("Please enter email and password");
                }
            }
            catch (Exception ex)
            {
                admindetails.result.result = false;
                admindetails.result.message = "Error occurred: " + ex.Message.ToString();
            }
            return Ok(admindetails);
        }
    }
}
