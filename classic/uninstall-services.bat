SET ENVIRONMENT=PREPROD
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="TCHQHostService_%ENVIRONMENT%" /DisplayName="Query Host Service (%ENVIRONMENT%)" "RTS/TCHQ/TCHQHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="WSST_Processor Host Service_%ENVIRONMENT%" /DisplayName="WSST Processor Host Service (%ENVIRONMENT%)" "RTS/WSST_In/WSST_ProcessorHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="WSST_File Rcvr Host Service_%ENVIRONMENT%" /DisplayName="WSST File Receiver (%ENVIRONMENT%)" "RTS/WSST_Ex/WSST_FileRcvrHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="WSRQHostService_%ENVIRONMENT%" /DisplayName="WSRQ Query Host Service (%ENVIRONMENT%)" "RTS/WSRQ/WSRQHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="WSRQ New Request Processor Host Service_%ENVIRONMENT%" /DisplayName="WSRQ New Request Processor Host Service (%ENVIRONMENT%)" "RTS/WSRQNR/WSRQNewRequest_ProcessorHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="WSRE_Processor Host Service_%ENVIRONMENT%" /DisplayName="WSRE Processor Host Service (%ENVIRONMENT%)" "RTS/WSRE_In/WSRE_ProcessorHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="WSRE_File Rcvr Host Service_%ENVIRONMENT%" /DisplayName="WSRE File Receiver (%ENVIRONMENT%)" "RTS/WSRE_Ex/WSRE_FileRcvrHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="TVQRHostService_%ENVIRONMENT%" /DisplayName="WSTVQR Host Service (%ENVIRONMENT%)" "RTS/TVQR/TVQRHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="EPD-G2BRecvrHostService_%ENVIRONMENT%" /DisplayName="TIR EPD G2B Receiver Host Service (%ENVIRONMENT%)" "RTS/TIREPDG2B_SVC/TIREDPG2BRecvrHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="EGISHostService_%ENVIRONMENT%" /DisplayName="WSEGIS Host Service (%ENVIRONMENT%)" "RTS/EGIS/EGISHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="RTS Alert and Reporting Host Service_%ENVIRONMENT%" /DisplayName="RTS Alert and Reporting Host Service (%ENVIRONMENT%)" "RTS/AlertProcessor/RTSAlertHostService.exe" /u
"C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil" /ServiceName="CryptoHostService_%ENVIRONMENT%" /DisplayName="CryptoHostService (%ENVIRONMENT%)" "RTS/CryptoHost/CryptoHostService.exe" /u