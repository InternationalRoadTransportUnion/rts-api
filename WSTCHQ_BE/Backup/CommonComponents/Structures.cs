using System;

namespace IRU.RTS.CommonComponents
{
	/// <summary>
	/// Summary description for Structures.
	/// </summary>
	public struct  safeTIRUploadParams
	{
		public string MessageTag;
		public byte[] safeTIRUploadData;
		public string CopyToID;
		public string MessageID;
	}

}
