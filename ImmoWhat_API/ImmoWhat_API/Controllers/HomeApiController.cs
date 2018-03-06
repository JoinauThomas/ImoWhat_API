﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImmoWhat_API.Controllers
{
    [RoutePrefix("api/HomeApi")]
    public class HomeApiController : ApiController
    {
        [HttpGet]
        [Route("GetHighestYear")]
        public int GetHighestYear()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=ImmoWhat;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand("GetHighestYear", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        var x = cmd.ExecuteScalar();
                        con.Close();

                        return (int)x;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}