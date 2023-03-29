using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace NG6_R51.Controllers
{
    public class TPController : Controller
    {
        private MyDBContext db;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _HostEnvironment;
        public TPController(MyDBContext _db, Microsoft.AspNetCore.Hosting.IHostingEnvironment HostEnvironment)
        {
            db = _db;
            _HostEnvironment = HostEnvironment;
        }

        
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile files)
        {
            string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
            filename = this.EnsureCorrectFilename(filename);
            using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                await files.CopyToAsync(output);
            return Ok();
        }
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }
        private string GetPathAndFilename(string filename)
        {
            return Path.Combine(_HostEnvironment.WebRootPath, "Uploads", filename);
        }

        [HttpPost]
        public string AddTrainerPlayerVm([FromBody] TrainerPlayerVm md)
        {
            RemoveTrainerPlayerVm(md.trainer.trainerid);
            trainer m = new trainer() { trainerid = md.trainer.trainerid, trainername = md.trainer.trainername, location = md.trainer.location };
            db.trainers.Add(m);
            db.SaveChanges();
            foreach (var c in md.player)
            {
                player d = new player()
                {
                    playercode = c.playercode,
                    playername = c.playername,
                    trainerid = c.trainerid,
                    traincost = c.traincost,
                    earned = c.earned,
                    matchdate = DateTime.Parse(c.matchdate.ToShortDateString()),
                    picture = c.picture
                };
                db.players.Add(d);
            }
            db.SaveChanges();
            return "1";
        }
        [HttpPost]
        public string RemoveTrainerPlayerVm(string id)
        {
            List<player> st5 = db.players.Where(xx => xx.trainerid == id).ToList();
            db.players.RemoveRange(st5);
            db.SaveChanges();
            trainer st6 = db.trainers.Find(id);
            if (st6 != null)
            {
                db.trainers.Remove(st6);
            }
            db.SaveChanges();

            return "1";
        }

        public JsonResult GetAllTrainer()
        {
            var a = (from d in db.trainers select new { d.trainerid, d.trainername, d.location });
            return Json(a);
        }

        public JsonResult GetTrainer(string id)
        {
            var a = (from d in db.trainers where d.trainerid == id select new { d.trainerid, d.trainername, d.location });
            return Json(a);
        }
        public JsonResult GetPlayer(string id)
        {
            var a = (from d in db.players where d.trainerid == id select new { d.playercode, d.playername, d.traincost, d.earned, d.matchdate, d.picture });
            return Json(a);
        }
        public JsonResult GetAllIPlayer()
        {
            var a = (from d in db.players select new { d.playercode, d.playername, d.traincost, d.earned, d.matchdate, d.picture, d.trainerid });
            return Json(a);
        }
        public ActionResult ShowMe()
        {
            IEnumerable<trainer> s = db.trainers.ToList();
            return View(s);
        }

        public ActionResult ShowTrainer(string tid = "0")
        {
            List<trainer> s = db.trainers.Where(xx => xx.trainerid == tid).ToList();
            return View(s);
        }

        public ActionResult Create2(string sid = "0")
        {
            ViewBag.sid = sid;
            return View();
        }



        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    var files = Request.Form.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        IFormFile file = files[i];
                        string fname;

                        fname = file.FileName;

                        // Get the complete folder path and store the file inside it.  
                        //fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        string webRootPath = _HostEnvironment.WebRootPath;
                        string fname1 = "";
                        fname1 = Path.Combine(webRootPath, "Uploads/" + fname);
                        file.CopyTo(new FileStream(fname1, FileMode.Create));
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public string GenerateCodeDP()
        {
            string a1 = "";
            string b1 = "";

            try
            {
                var a = (from det in db.trainers select det.trainerid.Substring(3)).Max();
                int b = int.Parse(a.ToString()) + 1;
                if (b < 10)
                {
                    b1 = "000" + b.ToString();
                }
                else if (b < 100)
                {
                    b1 = "00" + b.ToString();
                }
                else if (b < 1000)
                {
                    b1 = "0" + b.ToString();
                }
                else
                {
                    b1 = b.ToString();
                }
                a1 = "DP-" + b1.ToString();
            }
            catch (Exception ex)
            {
                a1 = "t-001";
            }
            return a1;
        }
        public string GenerateCodePlayer()
        {
            string a1 = "";
            string b1 = "";

            try
            {
                var a = (from det in db.players select det.playercode.Substring(3)).Max();
                int b = int.Parse(a.ToString()) + 1;
                if (b < 10)
                {
                    b1 = "000" + b.ToString();
                }
                else if (b < 100)
                {
                    b1 = "00" + b.ToString();
                }
                else if (b < 1000)
                {
                    b1 = "0" + b.ToString();
                }
                else
                {
                    b1 = b.ToString();
                }
                a1 = "IT-" + b1.ToString();
            }
            catch (Exception ex)
            {
                a1 = "p-001";
            }
            return a1;
        }

    }
}

