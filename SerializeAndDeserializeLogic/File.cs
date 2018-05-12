using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializeAndDeserializeLogic
{
    [Serializable]
    class File
    {
        public byte[] Data { get; set; }
        public string Name { get; set; }
    }
}
