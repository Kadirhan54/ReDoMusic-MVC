using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReDoMusic.Shared.Services
{
    public class GuidGeneratorService
    {
        public Guid Generate()
        {
            return Guid.NewGuid();

        }
    }
}
