rmdir /Q /S RTS
SET CONFIG=Release
msbuild WSTCHQ_BE/TCHQHostService/TCHQHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/TCHQ/
msbuild WSRQ_BE/WSRQHostService/WSRQHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/WSRQ/
msbuild WSRQ_BE/WSRQNewRequest_ProcessorHostService/WSRQNewRequest_ProcessorHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/WSRQNR/
msbuild WSST_BE/WSST_ProcessorHostService/WSST_ProcessorHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/WSST_In/
msbuild WSST_BE/WSST_FileRcvrHostService/WSST_FileRcvrHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/WSST_Ex/
msbuild WSRE_BE/WSRE_ProcessorHostService/WSRE_ProcessorHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/WSRE_In/
msbuild WSRE_BE/WSRE_FileRcvrHostService/WSRE_FileRcvrHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/WSRE_Ex/
msbuild WSTVQR_BE/TVQRHostService/TVQRHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/TVQR/
msbuild TIREPD_RTS/TIREDPG2BRecvrHostService/TIREDPG2BRecvrHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/TIREPDG2B_SVC/

msbuild RTS_WS/TCHQ_WS/TCHQ_WS.sln /t:rebuild /p:Configuration=%CONFIG%;OutDir=.\..\..\RTS\TCHQ_WS\bin\;WebProjectOutputDir=.\..\..\..\RTS\TCHQ_WS\
msbuild RTS_WS/WSRE_WS/WSRE_WS.sln /t:rebuild /p:Configuration=%CONFIG%;OutDir=.\..\..\RTS\WSRE_WS\bin\;WebProjectOutputDir=.\..\..\RTS\WSRE_WS\
msbuild RTS_WS/WSRQ_WS/WSRQ_WS.sln /t:rebuild /p:Configuration=%CONFIG%;OutDir=.\..\..\RTS\WSRQ_WS\bin\;WebProjectOutputDir=.\..\..\RTS\WSRQ_WS\
msbuild RTS_WS/WSST_WS/WSST_WS.sln /t:rebuild /p:Configuration=%CONFIG%;OutDir=.\..\..\RTS\WSST_WS\bin\;WebProjectOutputDir=.\..\..\..\RTS\WSST_WS\
msbuild RTS_WS/TVQR_WS/TVQR_WS.sln /t:rebuild /p:Configuration=%CONFIG%;OutDir=.\..\..\RTS\TVQR_WS\bin\;WebProjectOutputDir=.\..\..\..\RTS\TVQR_WS\
msbuild TIREPDB2G/TIREPDB2G/TIREPDB2G.csproj /t:rebuild /p:Configuration=%CONFIG%;OutDir=.\..\..\RTS\TIREPDB2G\bin\;WebProjectOutputDir=.\..\..\RTS\TIREPDB2G\
msbuild TIREPDB2G/TIREPDG2B/TIREPDG2B.csproj /t:rebuild /p:Configuration=%CONFIG%;OutDir=.\..\..\RTS\TIREPDG2B\bin\;WebProjectOutputDir=.\..\..\RTS\TIREPDG2B\

rem del /f /s /q .\RTS\*.config
rem del /f /s /q .\RTS\*.xml