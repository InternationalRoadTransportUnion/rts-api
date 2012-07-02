using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using IRU.RTS.WS.TerminationService.Interface;
using IRU.RTS.WS.Common.Subscribers;
using RTSDotNETClient;
using RTSDotNETClient.WSST;

namespace IRU.RTS.WS.TerminationService.Implementation.RTSClient
{
    public class RTSClientWrapper
    {
        private SafeTIRTransmissionClient _rtsWSST;
        private X509Certificate2 _serverCert, _clientCert;

        public RTSClientWrapper(string callerSubscriberId, Uri webServiceWSST, string rtsSubscriberId)
        {
            _rtsWSST = new SafeTIRTransmissionClient();
            
            _rtsWSST.WebServiceUrl = webServiceWSST.ToString();
            
            DateTime dtNow = DateTime.Now;
            X509Certificate2Collection x2cClients = CertificatesStore.GetCertificates(CertStore.RTS, CertUsage.Client);
            X509Certificate2Collection x2cServers = CertificatesStore.GetCertificates(CertStore.RTS, CertUsage.Server);
            x2cClients = x2cClients.Find(X509FindType.FindByTimeValid, dtNow, false);
            x2cServers = x2cServers.Find(X509FindType.FindByTimeValid, dtNow, false);
            x2cClients = x2cClients.FindBySubscriberId(rtsSubscriberId);
            x2cServers = x2cServers.FindBySubscriberId(rtsSubscriberId);
            if (x2cClients.Count == 0)
                throw new IndexOutOfRangeException(String.Format("RTS client certificate of subscriber [{0}] not found.", rtsSubscriberId));
            if (x2cClients.Count > 1)
                throw new Exception(String.Format("Too many valid RTS client certificates of subscriber [{0}] found.", rtsSubscriberId));
            if (x2cServers.Count == 0)
                throw new IndexOutOfRangeException(String.Format("RTS server certificate of subscriber [{0}] not found.", rtsSubscriberId));
            if (x2cServers.Count > 1)
                throw new Exception(String.Format("Too many valid RTS server certificates of subscriber [{0}] found.", rtsSubscriberId));
            if (!String.Equals(x2cClients[0].Thumbprint, x2cServers[0].Thumbprint))
                throw new NotSupportedException(String.Format("Mismatch between RTS client certificate and server certificate of subscriber [{0}].", rtsSubscriberId));

            _serverCert = x2cServers[0];
            _clientCert = x2cClients[0];

            _rtsWSST.PublicCertificate = _serverCert;            
        }

        private Record ConvertTIROperationTerminationToRecord(TIROperationTerminationType termination, bool isNewRecord)
        {
            Record res = new Record();

            res.TNO = termination.TIRCarnetNumber;
            res.VPN = termination.VoletPageNumber;
            res.ICC = termination.Customs.CountryCode;
            res.COF = termination.CustomsOffice;
            res.CNL = termination.CustomsLedgerEntryReference;
            res.DCL = termination.CustomsLedgerEntryDate;
            res.RND = termination.CertificateOfTerminationReference;
            res.DDI = termination.CertificateOfTerminationDate;
            res.PFD = termination.IsFinal ? "FD" : "PD";
            res.CWR = termination.IsWithReservation ? CWR.WithReservation : CWR.OK;
            res.COM = termination.CustomsComment;
            res.PIC = termination.PackageCountSpecified ? (int)termination.PackageCount : 0 /* TODO: Handle Null value correctly */;
            res.UPG = isNewRecord ? UPG.New : UPG.CancelDelete;

            return res;
        }

        public transmitTIROperationTerminationsResponse SendTerminations(transmitTIROperationTerminationsRequest terminations)
        {
            transmitTIROperationTerminationsResponse res = new transmitTIROperationTerminationsResponse();

            Query query = new Query();
            
            query.Body.SubscriberID = _rtsWSST.PublicCertificate.SubscriberId();
            query.Body.Version = "1.0.0";
            query.Body.UploadType = UploadType.DataUpload;
            query.Body.SentTime = terminations.transmissionTime;
            query.Body.SenderMessageID = String.Format("{0}@{1}", terminations.transmissionId, _clientCert.SubscriberId());
            query.Body.SafeTIRRecords = new List<Record>();

            for (int i = 0; i < terminations.TIROperationTerminations.count; i++)
            {
                object termination = terminations.TIROperationTerminations.Items[i];
                switch (terminations.TIROperationTerminations.ItemsElementName[i])
                {
                    case ItemsChoiceType.NewTIROperationTermination:
                        query.Body.SafeTIRRecords.Add(ConvertTIROperationTerminationToRecord((TIROperationTerminationType)termination, true));
                        break;
                    case ItemsChoiceType.UpdatedTIROperationTermination:
                        query.Body.SafeTIRRecords.Add(ConvertTIROperationTerminationToRecord(((UpdatedTIROperationTerminationType)termination).CancelledTIROperationTermination, false));
                        query.Body.SafeTIRRecords.Add(ConvertTIROperationTerminationToRecord(((UpdatedTIROperationTerminationType)termination).NewTIROperationTermination, true));
                        break;
                    case ItemsChoiceType.CancelledTIROperationTermination:
                        query.Body.SafeTIRRecords.Add(ConvertTIROperationTerminationToRecord((TIROperationTerminationType)termination, false));
                        break;
                }
            }
            
            query.Body.TCN = query.Body.SafeTIRRecords.Count;

            try
            {
                _rtsWSST.Send(query);

                res.success = true;
                res.transmissionTime = DateTime.Now;
            }
            catch (RTSWebServiceException)
            {
                res.success = false;
                res.transmissionTime = DateTime.Now;
            }

            return res;
        }
    }
}
