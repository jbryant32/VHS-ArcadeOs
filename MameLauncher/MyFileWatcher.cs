using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MameLauncher
{
    public class MyFileWatcher
    {
        readonly string _fileDir;
        private string FileDir { get { return _fileDir; } }
        private DateTime CurrentFileWriteTime { get; set; }
        private DateTime PreviouseFileWriteTime { get; set; }
        public event EventHandler FileChanged;
        public MyFileWatcher(string filedir)
        {
            _fileDir = filedir;

            PreviouseFileWriteTime = File.GetLastWriteTime(filedir);
            CurrentFileWriteTime = File.GetLastWriteTime(filedir);
        }

        public void Update()
        {
           
            PreviouseFileWriteTime = CurrentFileWriteTime;
            CurrentFileWriteTime = File.GetLastWriteTime(FileDir);


            if (PreviouseFileWriteTime != CurrentFileWriteTime)
            {
                if (FileChanged != null)
                {
                    FileChanged(this, new EventArgs());
                }
                PreviouseFileWriteTime = CurrentFileWriteTime;
            }
        }
    }
}
