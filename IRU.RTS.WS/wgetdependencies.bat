@echo off

REM Download dependencies on artifactory and rename them accordingly

mkdir Dependencies

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/wcf/security/wss/1.1.0.0/wss-1.1.0.0-3.5.dll
move wss-1.1.0.0-3.5.dll Dependencies\IRU.Common.WCF.Security.WSS.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/wcf/wsdl/1.1.0.0/wsdl-1.1.0.0-3.5.dll
move wsdl-1.1.0.0-3.5.dll Dependencies\IRU.Common.WCF.Wsdl.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/1.1/iru-schemas-1.1-iso-3166-1-alpha-3.xsd
move iru-schemas-1.1-iso-3166-1-alpha-3.xsd CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\iso-3166-1-alpha-3.xsd

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/1.1/iru-schemas-1.1-tir-actor.xsd
move iru-schemas-1.1-tir-actor.xsd CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\tir-actor-1.xsd

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/1.1/iru-schemas-1.1-tir-carnet.xsd
move iru-schemas-1.1-tir-carnet.xsd CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\tir-carnet-1.xsd