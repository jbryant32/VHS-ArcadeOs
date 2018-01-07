using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
namespace MameLauncher.Tools
{
    class InitDirectories
    {
        public async void Create()
        {
            var FrontEndAppFiles = Directory.Exists(@"C:\FrontEndAppFiles");
            var mameCmdFile = File.Exists(@"C:\FrontEndAppFiles\mameCmd.vhs");
            var mame = Directory.Exists(@"C:\mame");
            try
            {
                if (!FrontEndAppFiles)
                {
                    Directory.CreateDirectory(@"C:\FrontEndAppFiles");
                }

                if (!mameCmdFile)
                {

                    File.Create(@"C:\FrontEndAppFiles\mameCmd.vhs");
                }


                if (!mame)
                {
                    File.Create(@"C:\mame");
                    var Response = new HttpResponseMessage();
                    HttpClient client = new HttpClient();
                    Stream file = File.Open(@"C:\mame\mame.zip", FileMode.CreateNew);
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Response = await client.GetAsync("");

                    }
                    await Response.Content.CopyToAsync(file);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }
    }
}
