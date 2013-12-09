mkdir RTS
mkdir RTS\TCHQ
mkdir RTS\WSRQ
mkdir RTS\WSRQNR
mkdir RTS\WSST_Ex
mkdir RTS\WSST_In
msbuild WSTCHQ_BE/WSTCHQ_BE.sln /p:Configuration=Release;Platform="Any CPU" /t:rebuild
xcopy /e /y "WSTCHQ_BE\TCHQHostService\bin\*.*" "RTS\TCHQ"
msbuild WSST_BE/WSST_BE.sln /p:Configuration=Release;Platform="Any CPU" /t:rebuild
xcopy /e /y "WSST_BE\WSST_ProcessorHostService\bin\*.*" "RTS\WSST_In"
xcopy /e /y "WSST_BE\WSST_FileRcvrHostService\bin\*.*" "RTS\WSST_Ex"
msbuild WSRQ_BE/WSRQ_BE.sln /p:Configuration=Release;Platform="Any CPU" /t:rebuild
xcopy /e /y "WSRQ_BE\WSRQHostService\bin\*.*" "RTS\WSRQ"
xcopy /e /y "WSRQ_BE\WSRQNewRequest_ProcessorHostService\bin\*.*" "RTS\WSRQNR"
msbuild WSRE_BE/WSRE_BE.sln /p:Configuration=Release;Platform="Any CPU" /t:rebuild
xcopy /e /y "WSRE_BE\WSRE_ProcessorHostService\bin\*.*" "RTS\WSST_In"
xcopy /e /y "WSRE_BE\WSRE_FileRcvrHostService\bin\*.*" "RTS\WSST_Ex"
del /f /s /q .\RTS\*.config
del /f /s /q .\RTS\*.xml
del /f /s /q .\RTS\*.vshost.*
del /f /s /q .\RTS\*.install*