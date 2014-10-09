using System;
using System.Collections.Generic;
using System.Text;

namespace CIFCreation
{
    // <summary>
    /// Structure for CIF Creation
    /// </summary>
    public class CIFStruct
    {
        public string SUBSCRIBERID;
        public string REQUESTID;//corresponds to RID tag in CIF
        public string  REQUESTREPLYTYPE;//corresponds to UPG tag in CIF//optional
        public string TNO;
        public string ICC;
        public string DCL;//datetime
        public string CNL;
        public string COF;
        public string DDI;//datetime
        public string RND;//datetime
        public string PFD;
        public string TCO;//optional
        public string CWR;//optional
        public string VPN;
        public string COM;//optional
        public string RBC;//optional
        public string PIC;//optional-int
        public string UPG;//optional-int

    }

}
