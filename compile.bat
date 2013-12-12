rmdir /Q /S RTS
SET CONFIG=Release
msbuild WSTCHQ_BE/TCHQHostService/TCHQHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/TCHQ/
msbuild WSRQ_BE/WSRQHostService/WSRQHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/WSRQ/
msbuild WSRQ_BE/WSRQNewRequest_ProcessorHostService/WSRQNewRequest_ProcessorHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/WSRQNR/
msbuild WSST_BE/WSST_ProcessorHostService/WSST_ProcessorHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/WSST_In/
msbuild WSST_BE/WSST_FileRcvrHostService/WSST_FileRcvrHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/WSST_Ex/
msbuild WSRE_BE/WSRE_ProcessorHostService/WSRE_ProcessorHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/WSST_In/
msbuild WSRE_BE/WSRE_FileRcvrHostService/WSRE_FileRcvrHostService.csproj /p:Configuration=%CONFIG% /t:rebuild /p:OutputPath=./../../RTS/WSST_Ex/
rem del /f /s /q .\RTS\*.config
rem del /f /s /q .\RTS\*.xml