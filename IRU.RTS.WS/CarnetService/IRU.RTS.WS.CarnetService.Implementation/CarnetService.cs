using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Description;
using IRU.Common.WCF.Wsdl.Schema;
using IRU.Common.WCF.Wsdl.Output;
using IRU.RTS.WS.Common.Model;
using IRU.RTS.WS.Common.Data.Current;
using IRU.RTS.WS.CarnetService.Interface;

namespace IRU.RTS.WS.CarnetService.Implementation
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://rts.iru.org/services/CarnetService-1")]
    [XsdReplacer("IRU.RTS.WS.Common.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "rts.iru.org.model.rts-carnet-1.xsd")]
    [WsdlFormatter(WsdlFormatter.Options.RelativeUrls)]
    public class CarnetService : ICarnetService
    {
        #region ICarnetService Members

        public stoppedCarnetsType GetInvalidatedCarnets(DateTime from, DateTime? to, int? offset, int? count)
        {
            stoppedCarnetsType lsc = new stoppedCarnetsType();

            using (SQLCurrentQuery sq = new SQLCurrentQuery())
            {
                if (from.Kind == DateTimeKind.Unspecified)
                    from = DateTime.SpecifyKind(from, DateTimeKind.Local);
                from = from.ToLocalTime();

                DateTime dtTo = to ?? DateTime.Now.Date.AddDays(1.0);
                if (dtTo.Kind == DateTimeKind.Unspecified)
                    dtTo = DateTime.SpecifyKind(dtTo, DateTimeKind.Local);
                dtTo = dtTo.ToLocalTime();

                int iCount = count ??  Properties.Settings.Default.MaxCountOfCarnets;
                if (iCount > Properties.Settings.Default.MaxCountOfCarnets)
                    iCount = Properties.Settings.Default.MaxCountOfCarnets;

                sq.GetInvalidatedCarnets(from, dtTo, Properties.Settings.Default.MinTIRCarnetNumber, offset ?? 0, iCount, ref lsc);
            }

            return lsc;
        }

        #endregion
    }
}
