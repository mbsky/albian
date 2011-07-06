using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albian.Kernel
{
    public class BootService
    {
        public static void Start()
        {
            IKernelParser kernelParser = new KernelParser();
            kernelParser.ConfigParser("config\\Kernel.config");
        }
    }
}
