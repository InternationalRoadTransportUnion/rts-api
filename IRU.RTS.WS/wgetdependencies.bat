@echo off

REM Download dependencies on artifactory and rename them accordingly

mkdir Dependencies

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/wcf/security/wss/1.1.0.0/wss-1.1.0.0-3.5.dll
move wss-1.1.0.0-3.5.dll Dependencies\IRU.Common.WCF.Security.WSS.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/wcf/wsdl/1.1.0.0/wsdl-1.1.0.0-3.5.dll
move wsdl-1.1.0.0-3.5.dll Dependencies\IRU.Common.WCF.Wsdl.dll