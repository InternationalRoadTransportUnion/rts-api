@echo off

REM Download dependencies on artifactory and rename them accordingly

mkdir Dependencies

set WCF_SECURITY_WSS_VERSION=1.5.1.0
set COMMON_ENTLIB_VERSION=1.0.1.0
set RTSDOTNET_CLIENT_VERSION=1.1.0.0
set COMMON_IRU_SCHEMAS_VERSION=1.3

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/wcf/security/wss/%WCF_SECURITY_WSS_VERSION%/wss-%WCF_SECURITY_WSS_VERSION%-3.5.dll
move /y wss-%WCF_SECURITY_WSS_VERSION%-3.5.dll Dependencies\IRU.Common.WCF.Security.WSS.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/wcf/wsdl/%WCF_SECURITY_WSS_VERSION%/wsdl-%WCF_SECURITY_WSS_VERSION%-3.5.dll
move /y wsdl-%WCF_SECURITY_WSS_VERSION%-3.5.dll Dependencies\IRU.Common.WCF.Wsdl.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/wcf/behaviors/%WCF_SECURITY_WSS_VERSION%/behaviors-%WCF_SECURITY_WSS_VERSION%-3.5.dll
move /y behaviors-%WCF_SECURITY_WSS_VERSION%-3.5.dll Dependencies\IRU.Common.WCF.Behaviors.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/entlib/%COMMON_ENTLIB_VERSION%/entlib-%COMMON_ENTLIB_VERSION%-3.5.dll
move /y entlib-%COMMON_ENTLIB_VERSION%-3.5.dll Dependencies\IRU.Common.EnterpriseLibrary.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/rts/rtsdotnet-client/%RTSDOTNET_CLIENT_VERSION%/rtsdotnet-client-%RTSDOTNET_CLIENT_VERSION%-3.5.dll
move /y rtsdotnet-client-%RTSDOTNET_CLIENT_VERSION%-3.5.dll Dependencies\RTSDotNETClient.dll

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/%COMMON_IRU_SCHEMAS_VERSION%/iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-iso-3166-1-alpha-3.xsd
move /y iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-iso-3166-1-alpha-3.xsd CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\iso-3166-1-alpha-3.xsd

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/%COMMON_IRU_SCHEMAS_VERSION%/iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-actor.xsd
move /y iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-actor.xsd CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\tir-actor-1.xsd

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/%COMMON_IRU_SCHEMAS_VERSION%/iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-carnet.xsd
move /y iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-carnet.xsd CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\tir-carnet-1.xsd

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/%COMMON_IRU_SCHEMAS_VERSION%/iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-iso-3166-1-alpha-3.xsd
move /y iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-iso-3166-1-alpha-3.xsd TerminationService\IRU.RTS.WS.TerminationService.Interface\TerminationService-1\iso-3166-1-alpha-3.xsd

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/%COMMON_IRU_SCHEMAS_VERSION%/iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-actor.xsd
move /y iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-actor.xsd TerminationService\IRU.RTS.WS.TerminationService.Interface\TerminationService-1\tir-actor-1.xsd

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release-local/org/iru/common/iru-schemas/%COMMON_IRU_SCHEMAS_VERSION%/iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-carnet.xsd
move /y iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-carnet.xsd TerminationService\IRU.RTS.WS.TerminationService.Interface\TerminationService-1\tir-carnet-1.xsd

REM Copy common schema needed in both solutions
copy CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\rts-carnet-1.xsd TerminationService\IRU.RTS.WS.TerminationService.Interface\TerminationService-1\rts-carnet-1.xsd