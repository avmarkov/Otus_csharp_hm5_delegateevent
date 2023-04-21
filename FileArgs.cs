using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hm5_delegateevent
{
    public class FileArgs : EventArgs
    {
        public string FilePath { get; set; }
        public FileArgs(string filePath = "")
        {
            FilePath = filePath;
        }
    }
}
