using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using IRU.CryptEngine;

using IRU.RTS.Common.WCF;
using IRU.RTS.WSEGIS.CarnetingService;

namespace IRU.RTS.WSEGIS
{
    public class EGIS_CarnetingServiceHelper
    {
        private string _remoteAddress;

        public EGIS_CarnetingServiceHelper(string remoteAddress)
        {
            _remoteAddress = remoteAddress;
        }

        public Hashtable GetCarnetDetailsAndSafeTIRData(string tirCarnetNumber, bool findVoucherNumber)
        {
            Hashtable result = new Hashtable();

            // By default, the carnet does not exists => Status 5
            result.Add("Carnet_Number", (tirCarnetNumber ?? String.Empty).Trim());
            result.Add("Assoc_Short_Name", null);
            result.Add("Validity_Date", null);
            result.Add("No_Of_Terminations", null);
            result.Add("Query_Result_Code", "5");
            result.Add("Holder_ID", null);
            result.Add("Voucher_Number", null);
            result.Add("SafeTIRData", null);

            using (BasicHttpClient<IStandardCarnetingService> carnetingClient = new BasicHttpClient<IStandardCarnetingService>(_remoteAddress))
            {
                IStandardCarnetingService carnetingProxy = carnetingClient.GetProxy();

                int iTIR_Carnet_No = 0;
                if (IRU_CheckTIRNo.CheckForValidCarnetNo(tirCarnetNumber))
                {
                    if (IRU_CheckTIRNo.CheckForCheckChar(tirCarnetNumber) == "NUMERIC")
                    {
                        iTIR_Carnet_No = int.Parse(tirCarnetNumber.Trim());
                    }
                    else if (IRU_CheckTIRNo.CheckForCheckChar(tirCarnetNumber) == "ALPHA")
                    {
                        iTIR_Carnet_No = int.Parse((tirCarnetNumber.Trim().Substring(2)).ToString());
                    }

                    if (iTIR_Carnet_No != 0)
                    {
                        FullCarnetDetailsAndHistory carnetDetails = carnetingProxy.GetFullCarnetDetailsAndHistory(iTIR_Carnet_No);

                        if (carnetDetails != null)
                        {
                            if ((findVoucherNumber) && (carnetDetails.AdditionalGuarantees != null) && (carnetDetails.AdditionalGuarantees.Length > 0))
                            {
                                // There is a TIR+ guarantee
                                result["Voucher_Number"] = TIRVoucherUtils.getVoucherFromInt(carnetDetails.AdditionalGuarantees[0].Number);
                            }

                            if (carnetDetails.Stopped != null)
                            {
                                // If the carnet has been invalidated by the IRU => Status 3
                                result["Query_Result_Code"] = "3";
                            }
                            else if ((carnetDetails.History != null) && (carnetDetails.History.Count(h => h.IsCurrent) > 0))
                            {
                                CarnetHistoryEntry currentHistory = carnetDetails.History.Where(h => h.IsCurrent).FirstOrDefault();

                                switch (currentHistory.State.Id)
                                {
                                    case 0:
                                    case 95:
                                    case 5:
                                    case 9:
                                    case 101:
                                        if (carnetDetails.IssueEvent != null)
                                        {
                                            // If the carnet has been issued => Status 1
                                            result["Assoc_Short_Name"] = carnetDetails.IssueEvent.Association.Name.Trim();
                                            result["Validity_Date"] = (DateTime)carnetDetails.IssueEvent.ExpiryDate;
                                            result["Query_Result_Code"] = "1";
                                            result["Holder_ID"] = carnetDetails.IssueEvent.HolderCode.Trim();
                                        }
                                        else if (carnetDetails.Packet != null)
                                        {
                                            // If the carnet has not been issued => Status 2
                                            result["Assoc_Short_Name"] = carnetDetails.Packet.Association.Name.Trim();
                                            result["Query_Result_Code"] = "2";
                                        }

                                        break;
                                }

                                if (result["Assoc_Short_Name"] == null)
                                {
                                    // Otherwise, the carnet is not in circulation => Status 4
                                    result["Query_Result_Code"] = "4";
                                }
                            }

                            if (carnetDetails.Terminations != null)
                            {
                                List<FullTerminationDetails> terminations = carnetDetails.Terminations.Where(t =>
                                    !t.BlockedByDispatch
                                    && t.RecordState != null && t.RecordState > 0
                                    && t.RecordTypeId != null && t.RecordTypeId != 3
                                    //&& !String.Equals(t.DischargeType, "EXI")
                                    ).ToList();
                                result["No_Of_Terminations"] = terminations.Count();
                                result["SafeTIRData"] = terminations;
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
