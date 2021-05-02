using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetToolBox.TesterConsole
{
    public class TestFailedException : Exception
    {
        public TestFailedException(string expected, string got) : base($"Expected result: '{expected}', got: '{got}'")
        {

        }
    }
}
