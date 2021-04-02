using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dapper.Contrib.Extensions;

using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

using SantaShopC;

namespace SantaShopC.Controllers
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

    public class ComportamentoController : Controller
    {
        private const string ConnectionString = "server=127.0.0.1;uid=root;database=santashop";

        // GET: api/<PresentsController>
        [HttpGet]
        public IEnumerable<crianca> Get()
        {
            MySqlConnection cn = new MySqlConnection(ConnectionString);

            var res = cn.GetAll<crianca>();

            return res;
        }

        // GET api/<PresentsController>/5
        [HttpGet("{id}")]
        public crianca Get(int id)
        {
            MySqlConnection cn = new MySqlConnection(ConnectionString);

            var res = cn.Get<crianca>(id);

            return res;
        }

        // POST api/<PresentsController>
        [HttpPost]
        public crianca Post([FromBody] crianca present)
        {
            MySqlConnection cn = new MySqlConnection(ConnectionString);

            var idNewRec = cn.Insert<crianca>(present);

            var res = cn.Get<crianca>(idNewRec);

            return res;
        }

        // PUT api/<PresentsController>/5
        [HttpPut("{id}")]
        public ActionResult<crianca> Put(int id, [FromBody] crianca crianca)
        {
            MySqlConnection cn = new MySqlConnection(ConnectionString);

            var recLido = cn.Get<crianca>(id);

            if (recLido != null)
            {
                recLido.idade = crianca.idade;
                recLido.nome = crianca.nome;

                bool updated = cn.Update<crianca>(recLido);

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

            var res = cn.Get<crianca>(id);

            if (res != null)
            {
                bool recsDeleted = cn.Delete<crianca>(res);
                return Ok();
            }
            else
            {
                return NotFound();

            }


        }
    }
}
