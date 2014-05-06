using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using IRU.Common.WCF.Wsdl.Metadata;
using IRU.RTS.WS.VoucherService.Interface;

namespace IRU.RTS.WS.VoucherService.Implementation
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://rts.iru.org/services/VoucherService-1")]
    [MetadataFixer(new string[] { "VoucherService_1.*.wsdl", "VoucherService_1.*.xsd" }, true)]
    public class VoucherService : VoucherServiceSEI
    {
        public override QueriedVoucherType queryVoucher(string voucherNumber)
        {
            throw new NotImplementedException();
        }
    }
}
