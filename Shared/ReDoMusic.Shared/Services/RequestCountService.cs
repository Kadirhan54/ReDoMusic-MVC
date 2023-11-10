using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReDoMusic.Shared.Services
{
    public class RequestCountService
    {
        public int RequestCount { get; set; } = 0;

        public void IncrementRequestCount () { RequestCount++; }    
    }
}
