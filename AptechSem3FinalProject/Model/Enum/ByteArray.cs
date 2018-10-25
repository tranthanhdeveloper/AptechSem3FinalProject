using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    public class ByteArray
    {
        private byte[] MainKey = { 111, 131, 14, 235, 17, 130, 227, 17, 245, 115, 8, 68, 4, 17, 121, 216, 70, 186, 50, 101, 248, 231, 195, 219, 12, 198, 198, 133, 53, 167, 96, 115 };
        private byte[] MainIVKey = { 78, 38, 138, 91, 214, 31, 162, 127, 88, 173, 152, 127, 2, 84, 202, 91 };
        
        public class AESKMain : ByteArray
        {
            public byte[] GetKey()
            {
                return MainKey;
            }
            public byte[] GetIVKey()
            {
                return MainIVKey;
            }
        }
    }
}
