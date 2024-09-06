using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace LLMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LMSController : ControllerBase
    {
        List<Admin> admindata = new List<Admin>() { new Admin { id = 1, name = "shaik",email="abcd1@gmail.com", password = "abcd1234" } };
        List<Book> bookdata = new List<Book>() {
            new Book { id = 1, btitle="Test", bcatag ="Cat1",bauthor="Test", bcopies=3,bpub = "T",pubname="TT",bisbn="",bdate= DateTime.Now,bstatus="Old" },
            new Book { id = 2, btitle="Test2", bcatag ="Cat2",bauthor="Test", bcopies=3,bpub = "T",pubname="TT",bisbn="",bdate= DateTime.Now,bstatus="Old" },
            new Book { id = 3, btitle="Test3", bcatag ="Cat3",bauthor="Test", bcopies=3,bpub = "T",pubname="TT",bisbn="",bdate= DateTime.Now,bstatus="true0" },
            new Book { id = 4, btitle="Test4", bcatag ="Cat4",bauthor="Test", bcopies=3,bpub = "T",pubname="TT",bisbn="",bdate= DateTime.Now,bstatus="true0" }
        };
        // GET: Admin
        List<Members> memberdata = new List<Members>() { new Members() {
                id = 1,
                uname = "tmp",
                uadmid = 21,
                umail = "abcd@gmail.com",
                udep = "admin",
                upassword = "abcd1234",
                ustatus = 1,
                urecstatus = 1,
                urecj ="",
                ureqj = ""
            } };

        
        public LMSController()
        {
           
        }











        [HttpGet("members")]
        public ActionResult<List<Members>> Get([FromQuery] int ustatus, [FromQuery] int urecstatus)
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
            //var reclst = bookdata.Where(x => x.id % 2 == 0).Select(x => x).ToList();
           // var reqlst = bookdata.Where(x => x.id % 2 == 1).Select(x => x).ToList();
            //product.urecj = JsonSerializer.Serialize(reclst);
           // product.ureqj = JsonSerializer.Serialize(reqlst);
            return Ok(product);
        }
        [HttpPut("members/{id}")]
        public async Task<IActionResult> Get(Members curr)
        {
            if (memberdata == null) return BadRequest();
            var index = memberdata.FindIndex(b => b.id == curr.id);
            if (index != -1)
            {
                // Replace the "Banana" fruit with a new "Blueberry" fruit
                memberdata[index] = curr;
            }
            return Ok(memberdata[index]);
        }
        [HttpDelete("members/{id}")]
        public async Task<IActionResult> deleteMember(int id)
        {
            var member = memberdata.Where(x => x.id == id).FirstOrDefault();

            memberdata.Remove(member);
            return Ok(member);
        }
        [HttpPut("members/{id}")]
        public async Task<IActionResult> updateMember(int id, Members _mem)
        {
            if (_mem == null) return BadRequest();
            var index = memberdata.FindIndex(b => b.id == _mem.id);
            if (index != -1)
            {
                // Replace the "Banana" fruit with a new "Blueberry" fruit
                memberdata[index] = _mem;
            }
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
        public ActionResult<List<Book>> GetAllbooks([FromQuery] string bstatus)
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
        public async Task<IActionResult> updateBook(int id,Book _book)
        {
            if (_book == null) return BadRequest();
            var index = bookdata.FindIndex(b => b.id == _book.id);
            if (index != -1)
            {
                // Replace the "Banana" fruit with a new "Blueberry" fruit
                bookdata[index] = _book;
            }
            return Ok(bookdata[index]);
        }
        [HttpPost("books")]
        public async Task<IActionResult> putBook( Book _book)
        {
            if (_book == null) return BadRequest();
            bookdata.Add(_book);
            return Ok(_book);
        }
        [HttpDelete("books/{id}")]
        public async Task<IActionResult> deleteBook(int id)
        {
            var book = bookdata.Where(x => x.id == id).FirstOrDefault();

            bookdata.Remove(book);
            return Ok();
        }



    }
}
