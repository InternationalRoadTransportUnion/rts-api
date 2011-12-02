@echo off

REM Download dependencies on artifactory and rename them accordingly

mkdir Dependencies

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/wcf/security/wss/1.2.0.0/wss-1.2.0.0-3.5.dll
move /y wss-1.2.0.0-3.5.dll Dependencies\IRU.Common.WCF.Security.WSS.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/wcf/wsdl/1.2.0.0/wsdl-1.2.0.0-3.5.dll
move /y wsdl-1.2.0.0-3.5.dll Dependencies\IRU.Common.WCF.Wsdl.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/wcf/behaviors/1.2.0.0/behaviors-1.2.0.0-3.5.dll
move /y behaviors-1.2.0.0-3.5.dll Dependencies\IRU.Common.WCF.Behaviors.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/1.1/iru-schemas-1.1-iso-3166-1-alpha-3.xsd
move /y iru-schemas-1.1-iso-3166-1-alpha-3.xsd CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\iso-3166-1-alpha-3.xsd

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/1.1/iru-schemas-1.1-tir-actor.xsd
move /y iru-schemas-1.1-tir-actor.xsd CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\tir-actor-1.xsd

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/1.1/iru-schemas-1.1-tir-carnet.xsd
move /y iru-schemas-1.1-tir-carnet.xsd CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\tir-carnet-1.xsd