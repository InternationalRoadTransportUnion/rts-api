@echo off

REM Download dependencies on artifactory and rename them accordingly

mkdir Dependencies

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/wcf/security/wss/1.3.6.0/wss-1.3.6.0-3.5.dll
move /y wss-1.3.6.0-3.5.dll Dependencies\IRU.Common.WCF.Security.WSS.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/wcf/wsdl/1.3.6.0/wsdl-1.3.6.0-3.5.dll
move /y wsdl-1.3.6.0-3.5.dll Dependencies\IRU.Common.WCF.Wsdl.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/wcf/behaviors/1.3.6.0/behaviors-1.3.6.0-3.5.dll
move /y behaviors-1.3.6.0-3.5.dll Dependencies\IRU.Common.WCF.Behaviors.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/entlib/1.0.1.0/entlib-1.0.1.0-3.5.dll
move /y entlib-1.0.1.0-3.5.dll Dependencies\IRU.Common.EnterpriseLibrary.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/rts/rtsdotnet-client/1.0.0.0/rtsdotnet-client-1.0.0.0-3.5.dll
move /y rtsdotnet-client-1.0.0.0-3.5.dll Dependencies\RTSDotNETClient.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/1.3/iru-schemas-1.3-iso-3166-1-alpha-3.xsd
move /y iru-schemas-1.3-iso-3166-1-alpha-3.xsd CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\iso-3166-1-alpha-3.xsd

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/1.3/iru-schemas-1.3-tir-actor.xsd
move /y iru-schemas-1.3-tir-actor.xsd CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\tir-actor-1.xsd

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/1.3/iru-schemas-1.3-tir-carnet.xsd
move /y iru-schemas-1.3-tir-carnet.xsd CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\tir-carnet-1.xsd

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/1.3/iru-schemas-1.3-iso-3166-1-alpha-3.xsd
move /y iru-schemas-1.3-iso-3166-1-alpha-3.xsd TerminationService\IRU.RTS.WS.TerminationService.Interface\TerminationService-1\iso-3166-1-alpha-3.xsd

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/1.3/iru-schemas-1.3-tir-actor.xsd
move /y iru-schemas-1.3-tir-actor.xsd TerminationService\IRU.RTS.WS.TerminationService.Interface\TerminationService-1\tir-actor-1.xsd

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/1.3/iru-schemas-1.3-tir-carnet.xsd
move /y iru-schemas-1.3-tir-carnet.xsd TerminationService\IRU.RTS.WS.TerminationService.Interface\TerminationService-1\tir-carnet-1.xsd

REM Copy common schema needed in both solutions
copy CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\rts-carnet-1.xsd TerminationService\IRU.RTS.WS.TerminationService.Interface\TerminationService-1\rts-carnet-1.xsd