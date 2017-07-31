using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architector.Visa.InviniVision1000
{

    public class VisaClient
    {

        public string[] FindResources(string pattern)
        {
            // TODO
            return new string[] { pattern };
        }

        public void Open(string address)
        {
            // TODO
        }

        public string QueryString(string query)
        {
            // TODO
            return query;
        }

        public byte[] QueryBinary(string query)
        {
            // TODO
            return new byte[] { };
        }

        /*
        public void Shutdown(Action success, Action<Exception> error, Action complete)
        {
            this.Invoke(new ShutdownEntry(success, error, complete));
        }
         */

    }

}
