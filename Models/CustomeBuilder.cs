using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1FinalCapstone.Models
{
    public partial class BikeBuilder
    {
        public string Budimage1 { get; set; }
        public string Budimage2 { get; set; }
        public string Budimage3 { get; set; }
        public byte[] ExistingImageData1 { get; internal set; }
        public byte[] ExistingImageData2 { get; internal set; }
        public byte[] ExistingImageData3 { get; internal set; }
    }
}