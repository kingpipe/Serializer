using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializeAndDeserializeLogic
{
    [Serializable]
    class Folder
    {
        public Folder[] SubFolders { get; set; }
        public File[] files { get; set; }
        public string Name { get; set; }
    }
}
