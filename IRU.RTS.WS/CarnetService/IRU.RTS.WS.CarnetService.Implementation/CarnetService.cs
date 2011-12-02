using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Data.SqlClient;
using IRU.Common.WCF.Wsdl.Schema;
using IRU.Common.WCF.Wsdl.Output;
using IRU.RTS.WS.Common.Business;
using IRU.RTS.WS.Common.Data;
using IRU.RTS.WS.Common.Data.Current;
using IRU.RTS.WS.CarnetService.Interface;

namespace IRU.RTS.WS.CarnetService.Implementation
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://rts.iru.org/services/CarnetService-1")]
    [WsdlReplacer("IRU.RTS.WS.CarnetService.Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "CarnetService_1.CarnetService-1.wsdl", false)]
    [XsdReplacer(new string[] {"IRU.RTS.WS.CarnetService.Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"}, new string[] {"CarnetService_1.rts-carnet-1.xsd", "CarnetService_1.tir-carnet-1.xsd", "CarnetService_1.tir-actor-1.xsd", "CarnetService_1.iso-3166-1-alpha-3.xsd"})]
    public class CarnetService : CarnetServiceSEI
    {
        private getStoppedCarnetsResponse _stoppedCarnetsResponse;

        private void GetInvalidatedCarnetsExecuted(object sender, DbDataReaderEventArgs e)
        {
            DbQuery dq = (DbQuery)sender;

            int iRec = 0;
            while (e.DbDataReader.Read())
            {
                iRec = dq.GetValue<int>(e.DbDataReader, "RowNumber");

                StoppedCarnetType sc = new StoppedCarnetType();

                sc.TIRCarnetNumber = dq.GetValue<TIRCarnet>(e.DbDataReader, "Number").CarnetNumber;
                sc.ExpiryDate = dq.GetValue<DateTime>(e.DbDataReader, "ExpiryDate");
                uint? uiAssocId = dq.GetValue<uint?>(e.DbDataReader, "IssuingAssociation");
                if (uiAssocId.HasValue)
                {
                    sc.Association = new Association();
                    sc.Association.id = uiAssocId.Value;
                    sc.Association.name = dq.GetValue<string>(e.DbDataReader, "IssuingAssociationName");
                    if (sc.Association.name != null)
                        sc.Association.name = sc.Association.name.Trim();
                }
                string sHolderId = dq.GetValue<string>(e.DbDataReader, "Holder");
                if (!String.IsNullOrEmpty(sHolderId))
                {
                    sc.Holder = new HaulierType();
                    sc.Holder.id = sHolderId.Trim();
                }
                sc.DeclarationDate = dq.GetValue<DateTime>(e.DbDataReader, "DateOfDeclaration");
                sc.InvalidationDate = dq.GetValue<DateTime>(e.DbDataReader, "DateOfInvalidation");
                sc.InvalidationStatus = dq.GetValue<CarnetInvalidationStatus>(e.DbDataReader, "MotiveCode").AsInvalidationStatusType();

                _stoppedCarnetsResponse.stoppedCarnets.StoppedCarnet.Add(sc);
            }
            _stoppedCarnetsResponse.stoppedCarnets.count = _stoppedCarnetsResponse.stoppedCarnets.StoppedCarnet.Count;

            if ((e.DbDataReader.NextResult()) && (e.DbDataReader.Read()))
            {
                _stoppedCarnetsResponse.total.count = dq.GetValue<int>(e.DbDataReader, "CountOfFoundStoppedCarnets");
                _stoppedCarnetsResponse.total.from = dq.GetValue<DateTime>(e.DbDataReader, "MinOfDateOfInvalidation");
                _stoppedCarnetsResponse.total.fromSpecified = !_stoppedCarnetsResponse.total.from.Equals(DateTime.MinValue);
                _stoppedCarnetsResponse.total.to = dq.GetValue<DateTime>(e.DbDataReader, "MaxOfDateOfInvalidation");
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

            SqlConnection scCurrent = new SqlConnection(Properties.Settings.Default.CurrentDB);
            using (DbCurrentQuery sq = new DbCurrentQuery(scCurrent, Properties.Settings.Default.SQLCommandTimeout))
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
