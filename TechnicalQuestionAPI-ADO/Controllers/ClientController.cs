using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using TechnicalQuestionAPI_ADO.DTO;

namespace TechnicalQuestionAPI_ADO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ClientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        [Route("User details")]
        public IActionResult Userdetails()
        {
            // fetch connection information with database 
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //setup sql command 
            SqlCommand command = new SqlCommand("select userid,fullname,Specification,UserType from [user]", connection);
            //Execute Query Command
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();
            dataAdapter.Fill(datatable);
            return Ok(datatable);

        }
        [HttpGet]
        [Route("GetpersonInformation")]
        public IActionResult personInformation()
        {
            // fetch connection information with database 
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //setup sql command 
            SqlCommand command = new SqlCommand("select NationalityId,PersonalPhoto,Adress,EMAIL,[PASSWORD],PHONENUMBER from [user]", connection);
            //Execute Query Command
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();
            dataAdapter.Fill(datatable);
            return Ok(datatable);

        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Insertuser([FromBody] UserTController dto)
        {
            //Execute NON Query Command (# rows effeted)
            // fetch connection information with database 
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //setup sql command 
            string commandString = "INSERT INTO [dbo].[User]([FullName],[Age],[Specification],[Gender],[NationalityId],[UserType],[Bio],[PersonalPhoto],[Adress]),[EMAIL],[PASSWORD],[PHONENUMBER]VALUES(@FullName,@Age,@Specification,@Gender,@NationalityId,@UserType,@Bio,@PersonalPhoto,@Adress,@EMAIL,@PASSWORD,@PHONENUMBER)";
            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@FullName", dto.FullName);
            command.Parameters.AddWithValue("@Age", dto.Age);
            command.Parameters.AddWithValue("@Specification", dto.Specification);
            command.Parameters.AddWithValue("@Gender", dto.Gender);
            command.Parameters.AddWithValue("@NationalityId", dto.NationalityId);
            command.Parameters.AddWithValue("@UserType", dto.UserType);
            command.Parameters.AddWithValue("@Bio", dto.Bio);
            command.Parameters.AddWithValue("@PersonalPhoto", dto.PersonalPhoto);
            command.Parameters.AddWithValue("@Adress", dto.Adress);
            command.Parameters.AddWithValue("@EMAIL", dto.EMAIL);
            command.Parameters.AddWithValue("@PASSWORD", dto.PASSWORD);
            command.Parameters.AddWithValue("@PHONENUMBER", dto.PHONENUMBER);
            connection.Open();
            int rows = command.ExecuteNonQuery();
            connection.Close();
            if (rows > 0)
                return Ok();
            else
                return BadRequest("Insert Operation has beem Failed");
        }


        [HttpPut]
        [Route("[action]/{Id}")]
        public IActionResult UpdateUser([FromRoute] int Id, [FromBody] UserTController dto)
        {
            // fetch connection information with database 
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //setup sql command 
            string commandString = $"UPDATE [user] SET FullName ='{dto.FullName}' ,PASSWORD = '{dto.PASSWORD}',PHONENUMBER = '{dto.PHONENUMBER}' WHERE USERID = {Id}";
            SqlCommand command = new SqlCommand(commandString, connection);
            connection.Open();
            int rows = command.ExecuteNonQuery();
            connection.Close();
            if (rows > 0)
                return Ok();
            else
                return BadRequest("Insert Operation has beem Failed");
        }
        [HttpDelete]
        [Route("[action]/{Id}")]
        public IActionResult DeleteUser([FromRoute] int Id)
        {
            // fetch connection information with database 
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //setup sql command 
            string commandString = $"DELETE FROM [USER] WHERE USERID = {Id}";
            SqlCommand command = new SqlCommand(commandString, connection);
            connection.Open();
            int rows = command.ExecuteNonQuery();
            connection.Close();
            if (rows > 0)
                return Ok();
            else
                return BadRequest("Insert Operation has beem Failed");
        }






    }
}

