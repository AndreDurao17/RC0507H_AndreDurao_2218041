using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dapper.Contrib.Extensions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

using SantaShopC;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SantaShop.API.Controllers
{
    [Route("[controller]")]
    [ApiController]

    [Table("criancas")]
    public class crianca
    {
        [Key]
        public int crianca_id { get; set; }
        public int idade { get; set; }
        public string nome { get; set; }

    }

    [Table("presentes")]
    public class presentes
    {
        [Key]
        public int presenteid { get; set; }
        public int quantidade { get; set; }
        public string nome { get; set; }

    }

    [Table("comportamento")]
    public class comportamento
    {
        [Key]
        public int comportamento_id { get; set; }
        public bool quancondicaotidade { get; set; }
        public string descricao { get; set; }

    }


    public class PresentsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private string ConnectionString = "";

        private PresentsController(IConfiguration configRoot)
        {
            configuration = configRoot;

            ConnectionString = configuration.GetConnectionString("santa");

            string address = configuration["SMTPServer:Address"];
        }

        // GET: api/<PresentsController>
        [HttpGet]
        public IEnumerable<presentes> Get()
        {
            MySqlConnection cn = new MySqlConnection(ConnectionString);

            var res = cn.GetAll<presentes>();

            return res;
        }

        // GET api/<PresentsController>/5
        [HttpGet("{id}")]
        public presentes GetTheOne(int id)
        {
            MySqlConnection cn = new MySqlConnection(ConnectionString);

            var res = cn.Get<presentes>(id);

            return res;
        }

        // POST api/<PresentsController>
        [HttpPost]
        public presentes PostNew([FromBody] presentes present)
        {
            MySqlConnection cn = new MySqlConnection(ConnectionString);

            var idNewRec = cn.Insert<presentes>(present);

            var res = cn.Get<presentes>(idNewRec);

            return res;
        }

        // PUT api/<PresentsController>/5
        [HttpPut("{id}")]
        public ActionResult<presentes> Put(int id, [FromBody] presentes present)
        {
            MySqlConnection cn = new MySqlConnection(ConnectionString);

            var recLido = cn.Get<presentes>(id);

            if (recLido != null)
            {
                recLido.nome = present.nome;
                recLido.quantidade = present.quantidade;

                bool updated = cn.Update<presentes>(recLido);

                return Ok(recLido);
            }
            else
            {
                return NotFound();

            }
        }

        // DELETE api/<PresentsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            MySqlConnection cn = new MySqlConnection(ConnectionString);

            var res = cn.Get<presentes>(id);

            if (res != null)
            {
                bool recsDeleted = cn.Delete<presentes>(res);
                return Ok();
            }
            else
            {
                return NotFound();

            }


        }
    }
}

