using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using IRU.Common.WCF.Wsdl.Schema;
using IRU.Common.WCF.Wsdl.Output;
using IRU.Common.EnterpriseLibrary.Data;
using IRU.RTS.WS.Common.Data.Current;
using IRU.RTS.WS.CarnetService.Interface;
using IRU.RTS.WS.CarnetService.Implementation.Business;

namespace IRU.RTS.WS.CarnetService.Implementation
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://rts.iru.org/services/CarnetService-1")]
    [WsdlReplacer("IRU.RTS.WS.CarnetService.Interface, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null", "CarnetService_1.CarnetService-1.wsdl", false)]
    [XsdReplacer(new string[] {"IRU.RTS.WS.CarnetService.Interface, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null"}, new string[] {"CarnetService_1.rts-carnet-1.xsd", "CarnetService_1.tir-carnet-1.xsd", "CarnetService_1.tir-actor-1.xsd", "CarnetService_1.iso-3166-1-alpha-3.xsd"})]
    public class CarnetService : CarnetServiceSEI
    {
        private getStoppedCarnetsResponse _stoppedCarnetsResponse;

        private void GetInvalidatedCarnetsExecuted(object sender, DataReaderEventArgs e)
        {
            int iRec = 0;
            while (e.DataReader.Read())
            {
                iRec = e.DataReader.GetValue<int>("RowNumber");

                StoppedCarnetType sc = new StoppedCarnetType();

                sc.TIRCarnetNumber = new TIRCarnet(e.DataReader.GetValue<string>("Number")).CarnetNumber;
                sc.ExpiryDate = e.DataReader.GetValue<DateTime>("ExpiryDate");
                uint? uiAssocId = e.DataReader.GetValue<uint?>("IssuingAssociation");
                if (uiAssocId.HasValue)
                {
                    sc.Association = new Association();
                    sc.Association.id = uiAssocId.Value;
                    sc.Association.name = e.DataReader.GetValue<string>("IssuingAssociationName");
                    if (sc.Association.name != null)
                        sc.Association.name = sc.Association.name.Trim();
                }
                string sHolderId = e.DataReader.GetValue<string>("Holder");
                if (!String.IsNullOrEmpty(sHolderId))
                {
                    sc.Holder = new HaulierType();
                    sc.Holder.id = sHolderId.Trim();
                }
                sc.DeclarationDate = e.DataReader.GetValue<DateTime>("DateOfDeclaration");
                sc.InvalidationDate = e.DataReader.GetValue<DateTime>("DateOfInvalidation");
                sc.InvalidationStatus = e.DataReader.GetValue<int>("MotiveCode").AsCarnetInvalidationStatus().AsInvalidationStatusType();

                _stoppedCarnetsResponse.stoppedCarnets.StoppedCarnet.Add(sc);
            }
            _stoppedCarnetsResponse.stoppedCarnets.count = _stoppedCarnetsResponse.stoppedCarnets.StoppedCarnet.Count;

            if ((e.DataReader.NextResult()) && (e.DataReader.Read()))
            {
                _stoppedCarnetsResponse.total.count = e.DataReader.GetValue<int>("CountOfFoundStoppedCarnets");
                _stoppedCarnetsResponse.total.from = e.DataReader.GetValue<DateTime>("MinOfDateOfInvalidation");
                _stoppedCarnetsResponse.total.fromSpecified = !_stoppedCarnetsResponse.total.from.Equals(DateTime.MinValue);
                _stoppedCarnetsResponse.total.to = e.DataReader.GetValue<DateTime>("MaxOfDateOfInvalidation");
                _stoppedCarnetsResponse.total.toSpecified = !_stoppedCarnetsResponse.total.to.Equals(DateTime.MinValue);
                _stoppedCarnetsResponse.stoppedCarnets.endReached = (iRec == _stoppedCarnetsResponse.total.count);
            }
        }

        #region CarnetServiceSEI Members

        public override queryCarnetResponse queryCarnet(queryCarnetRequest request)
        {
            throw new NotImplementedException();
        }

        public override getStoppedCarnetsResponse getStoppedCarnets(getStoppedCarnetsRequest request)
        {
            _stoppedCarnetsResponse = new getStoppedCarnetsResponse();

            using (DbQueries sq = new DbQueries())
            {
                if (request.from.Kind == DateTimeKind.Unspecified)
                    request.from = DateTime.SpecifyKind(request.from, DateTimeKind.Local);
                request.from = request.from.ToLocalTime();

                DateTime dtTo = request.to ?? DateTime.Now.Date.AddDays(1.0);
                if (dtTo.Kind == DateTimeKind.Unspecified)
                    dtTo = DateTime.SpecifyKind(dtTo, DateTimeKind.Local);
                dtTo = dtTo.ToLocalTime();

                uint iCount = request.maxCount ?? Properties.Settings.Default.MaxCountOfCarnets;
                if (iCount > Properties.Settings.Default.MaxCountOfCarnets)
                    iCount = Properties.Settings.Default.MaxCountOfCarnets;

                _stoppedCarnetsResponse.total = new stoppedCarnetsTypeTotal();
                _stoppedCarnetsResponse.total.count = 0;
                _stoppedCarnetsResponse.stoppedCarnets = new stoppedCarnetsTypeStoppedCarnets();
                _stoppedCarnetsResponse.stoppedCarnets.offset = request.offset ?? 0;
                _stoppedCarnetsResponse.stoppedCarnets.endReached = true;
                _stoppedCarnetsResponse.stoppedCarnets.StoppedCarnet = new List<StoppedCarnetType>();

                sq.GetInvalidatedCarnets(request.from, dtTo, Properties.Settings.Default.MinTIRCarnetNumber, _stoppedCarnetsResponse.stoppedCarnets.offset, iCount, GetInvalidatedCarnetsExecuted);
            }

            return _stoppedCarnetsResponse;
        }

        #endregion
    }

    internal static class InvalidationStatusTypeExtension
    {
        public static InvalidationStatusType AsInvalidationStatusType(this CarnetInvalidationStatus val)
        {
            switch (val)
            {
                case CarnetInvalidationStatus.DESTROYED:
                    return InvalidationStatusType.DESTROYED;                
                case CarnetInvalidationStatus.LOST:
                    return InvalidationStatusType.LOST;                
                case CarnetInvalidationStatus.STOLEN:
                    return InvalidationStatusType.STOLEN;                
                case CarnetInvalidationStatus.RETAINED:
                    return InvalidationStatusType.RETAINED;                
                case CarnetInvalidationStatus.EXCLUDED:
                    return InvalidationStatusType.EXCLUDED;                
                case CarnetInvalidationStatus.INVALIDATED:
                    return InvalidationStatusType.INVALIDATED;                
                default:
                    return InvalidationStatusType.INVALIDATED;
            }            
        }
    }
}
