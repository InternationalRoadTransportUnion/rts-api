SET ENVIRONMENT=PREPROD
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="TCHQHostService_%ENVIRONMENT%" /DisplayName="Query Host Service (%ENVIRONMENT%)" "TCHQ/TCHQHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="WSST_Processor Host Service_%ENVIRONMENT%" /DisplayName="WSST Processor Host Service (%ENVIRONMENT%)" "WSST_In/WSST_ProcessorHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="WSST_File Rcvr Host Service_%ENVIRONMENT%" /DisplayName="WSST File Receiver (%ENVIRONMENT%)" "WSST_Ex/WSST_FileRcvrHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="WSRQHostService_%ENVIRONMENT%" /DisplayName="WSRQ Query Host Service (%ENVIRONMENT%)" "WSRQ/WSRQHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="WSRE_Processor Host Service_%ENVIRONMENT%" /DisplayName="WSRE Processor Host Service (%ENVIRONMENT%)" "WSST_In/WSRE_ProcessorHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="WSRE_File Rcvr Host Service_%ENVIRONMENT%" /DisplayName="WSRE File Receiver (%ENVIRONMENT%)" "WSST_Ex/WSRE_FileRcvrHostService.exe" /u