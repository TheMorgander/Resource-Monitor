using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Models.Enums
{
    public enum Resource
    {
        CPULoad,

        CPUTemperature,

        GPULoad,

        GPUTemperature,

        RAMLoad,

        DiskReadThroughput,

        DiskWriteThroughput,

        NetworkUploadThroughput,

        NetworkDownloadThroughput
    }
}
