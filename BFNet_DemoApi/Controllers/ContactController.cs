using BFNet_DemoApi.Models;
using BFNet_DemoApi.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using Tools.Connections;

namespace BFNet_DemoApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ContactController : ControllerBase
    {
        private readonly Connection _connection;

        public ContactController(Connection connection)
        {
            _connection = connection;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Command command = new Command("Select Id, LastName, FirstName FROM Contact", false);
            IEnumerable<Contact> contacts = _connection.ExecuteReader(command, dr => new Contact() { Id = (int)dr["Id"], LastName = (string)dr["LastName"], FirstName = (string)dr["FirstName"] }, true);

            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Command command = new Command("Select Id, LastName, FirstName FROM Contact WHERE Id = @Id", false);
            command.AddParameter("Id", id);

            Contact? contact = _connection.ExecuteReader(command, dr => new Contact() { Id = (int)dr["Id"], LastName = (string)dr["LastName"], FirstName = (string)dr["FirstName"] }, true).SingleOrDefault();

            if (contact is null)
                return NotFound();

            return Ok(contact);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostContact contact)
        {
            Command command = new Command("Insert into Contact (LastName, FirstName) OUTPUT Inserted.Id VALUES (@LastName, @FirstName)", false);
            command.AddParameter("LastName", contact.LastName);
            command.AddParameter("FirstName", contact.FirstName);

            int id = (int)_connection.ExecuteScalar(command)!;
            return Ok(new Contact() { Id = id, LastName = contact.LastName, FirstName = contact.FirstName });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PutContact contact)
        {
            if (id != contact.Id)
                return BadRequest();

            Command command = new Command("Update Contact Set LastName = @LastName, FirstName = @FirstName WHERE Id = @Id", false);
            command.AddParameter("Id", id);
            command.AddParameter("LastName", contact.LastName);
            command.AddParameter("FirstName", contact.FirstName);

            return (_connection.ExecuteNonQuery(command) == 1) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Command command = new Command("Delete From Contact WHERE Id = @Id", false);
            command.AddParameter("Id", id);

            return (_connection.ExecuteNonQuery(command) == 1) ? NoContent() : NotFound();
        }
    }
}
