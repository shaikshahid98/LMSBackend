using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LLMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LMSController : ControllerBase
    {
        List<Admin> admindata = new();
        List<Book> bookdata = new();
        // GET: Admin
        List<Members> memberdata = new();
        
        public LMSController()
        {
            Init();

        }

        public void Init()
        {
            using (StreamReader r = new StreamReader("Data\\AdminData.json"))
            {
                string json = r.ReadToEnd();
                admindata =  JsonSerializer.Deserialize<List<Admin>>(json);
            }
            using (StreamReader r = new StreamReader("Data\\BooksData.json"))
            {
                string json = r.ReadToEnd();
                bookdata = JsonSerializer.Deserialize<List<Book>>(json);
            }
            using (StreamReader r = new StreamReader("Data\\MembersData.json"))
            {
                string json = r.ReadToEnd();
                memberdata = JsonSerializer.Deserialize<List<Members>>(json);
            }
        }

        public void UpdateData(dynamic data, Datainfo info)
        {
            string json = JsonSerializer.Serialize(data);
            //write string to file
            switch (info)
            {
                case Datainfo.Admin:
                    System.IO.File.WriteAllText(@"Data\\AdminData.json", json);
                    break;
                case Datainfo.Member:
                    System.IO.File.WriteAllText(@"Data\\MembersData.json", json);
                    break;
                case Datainfo.Book:
                    System.IO.File.WriteAllText(@"Data\\BooksData.json", json);
                    break;
                default:
                    Console.WriteLine("No need to update data.");
                    break;
            }
            Init();
        }








        [HttpGet("members")]
        public ActionResult<List<Members>> Get([FromQuery] int? ustatus, [FromQuery] int? urecstatus)
        {
            if(ustatus ==1 )
            return Ok(memberdata.Where(x=> x.ustatus == ustatus).ToList());


            if (urecstatus == 1)
                return Ok(memberdata.Where(x => x.urecstatus == urecstatus).ToList());

            return Ok(memberdata);

        }
        [HttpGet("members/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest();

            var product = memberdata.Where(m => m.id == id).FirstOrDefault();
            if (product == null)
                return NotFound();
            
            return Ok(product);
        }
        [HttpPost("members/{id}")]
        public async Task<IActionResult> PostMember([FromBody]Members curr)
        {
            if (memberdata == null) return BadRequest();
            var index = memberdata.FindIndex(b => b.id == curr.id);
            if (index != -1)
            {
                // Replace the "Banana" fruit with a new "Blueberry" fruit
                memberdata[index] = curr;
            }
            UpdateData(memberdata, Datainfo.Member);
            return Ok(memberdata[index]);
        }
        [HttpDelete("members/{id}")]
        public async Task<IActionResult> deleteMember(int id)
        {
            var member = memberdata.Where(x => x.id == id).FirstOrDefault();

            memberdata.Remove(member);
            UpdateData(memberdata, Datainfo.Member);
            return Ok(member);
        }
        [HttpPost("members")]
        public async Task<IActionResult> PostNewMember([FromBody] Members member)
        {
            int accno = new Random().Next(1, 1000);
            member.id = accno;
            memberdata.Add(member);
            UpdateData(memberdata, Datainfo.Member);
            return Ok(member);
        }
        [HttpPut("members/{id}")]
        public IActionResult updateMember(int id,[FromBody] Members _mem)
        {
            if (_mem == null) return BadRequest();
            var index = memberdata.FindIndex(b => b.id == _mem.id);
            if (index != -1)
            {
                // Replace the "Banana" fruit with a new "Blueberry" fruit
                memberdata[index] = new Members() { 
                        id = _mem.id,
                    uname = _mem.uname,
                    uadmid = _mem.uadmid,
                    umail = _mem.umail,
                    udep = _mem.udep,
                    upassword = _mem.upassword,
                    ustatus = _mem.ustatus,
                    urecstatus = _mem.urecstatus,
                    ureqj = _mem.ureqj,
                    urecj = _mem.urecj
                };
            }
            UpdateData(memberdata, Datainfo.Member);
            return Ok(_mem);
        }












        [HttpGet("admins/GetAllAdmins")]
        public ActionResult<List<Members>> GetAllAdmins()
        {
            return Ok(admindata);

        }
        [HttpGet("admins/{id}")]
        public async Task<IActionResult> GetAdmin(int id)
        {
            if (id < 1)
                return BadRequest();
            var product = admindata.Where(m => m.id == id).ToList();
            if (product == null)
                return NotFound();
            return Ok(product);
        }






        [HttpGet("books")]
        public ActionResult<List<Book>> GetAllbooks([FromQuery] string? bstatus)
        {
            if(string.IsNullOrEmpty(bstatus))
            return Ok(bookdata);

            return Ok(bookdata.Where(x => x.bstatus == bstatus).ToList());

        }
        [HttpGet("books/{id}")]
        public async Task<IActionResult> GetBookbyID(int id)
        {
            if (id < 1)
                return BadRequest();
            var product = bookdata.Where(m => m.id == id).ToList();
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [HttpPut("books/{id}")]
        public async Task<IActionResult> updateBook(int id,[FromBody]Book _book)
        {
            if (_book == null) return BadRequest();
            var index = bookdata.FindIndex(b => b.id == _book.id);
            if (index != -1)
            {
                // Replace the "Banana" fruit with a new "Blueberry" fruit
                bookdata[index] = _book;
            }
            UpdateData(bookdata, Datainfo.Book);

            return Ok(bookdata[index]);
        }
        [HttpPost("books")]
        public async Task<IActionResult> putBook([FromBody] Book _book)
        {
            if (_book == null) return BadRequest();
            int accno = new Random().Next(1, 1000);
            _book.id = accno;
            bookdata.Add(_book);
            UpdateData(bookdata, Datainfo.Book);

            return Ok(_book);
        }
        [HttpDelete("books/{id}")]
        public async Task<IActionResult> deleteBook(int id)
        {
            var book = bookdata.Where(x => x.id == id).FirstOrDefault();

            bookdata.Remove(book);
            UpdateData(bookdata, Datainfo.Book);
            return Ok();
        }



    }
}
