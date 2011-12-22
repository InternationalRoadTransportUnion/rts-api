using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RTSDotNETClient
{
    public enum PFD
    {
        [XmlEnum("FD")]
        FinalDischarge,
        [XmlEnum("PD")]
        PartialDischarge
    }
    public enum CWR
    {
        [XmlEnum("OK")]
        OK,
        [XmlEnum("R")]
        WithReservation
    }
    public enum RBC
    {
        [XmlEnum("CR")]
        CarnetRetained,
        [XmlEnum("CNR")]
        CarnetNotRetained,
        [XmlEnum("VR")]
        VoletRetained,
        [XmlEnum("VNR")]
        VoletNotRetained
    }
}
