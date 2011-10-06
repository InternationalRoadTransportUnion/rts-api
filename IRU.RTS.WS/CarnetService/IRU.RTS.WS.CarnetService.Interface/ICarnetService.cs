using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using IRU.RTS.WS.Common.Model;

namespace IRU.RTS.WS.CarnetService.Interface
{
    [ServiceContract(Namespace="http://rts.iru.org/services/CarnetService-1")]
    [XmlSerializerFormat()]
    public interface ICarnetService
    {
        [OperationContract(Action = "http://rts.iru.org/services/CarnetService-1#GetInvalidatedCarnets")]        
        stoppedCarnetsType GetInvalidatedCarnets(DateTime from, DateTime? to, int? offset, int? count);
    }
}
