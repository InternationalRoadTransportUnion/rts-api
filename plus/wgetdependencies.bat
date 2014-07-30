@echo off

REM Download dependencies on artifactory and rename them accordingly

mkdir Dependencies

set REPO_LIBS_RELEASE_LOCAL_URL=http://repo.iruworld.org/libs-release-local
set COMMON_WCF_VERSION=1.6.9.0
set COMMON_ENTLIB_VERSION=1.1.1.0
set RTSDOTNET_CLIENT_VERSION=1.6.2
set COMMON_IRU_SCHEMAS_VERSION=1.5
set TIRPLUS_MODEL_VERSION=1.1

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/common/wcf/security/wss/%COMMON_WCF_VERSION%/wss-%COMMON_WCF_VERSION%-3.5.dll
move /y wss-%COMMON_WCF_VERSION%-3.5.dll Dependencies\IRU.Common.WCF.Security.WSS.dll

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/common/wcf/wsdl/%COMMON_WCF_VERSION%/wsdl-%COMMON_WCF_VERSION%-3.5.dll
move /y wsdl-%COMMON_WCF_VERSION%-3.5.dll Dependencies\IRU.Common.WCF.Wsdl.dll

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/common/wcf/behaviors/%COMMON_WCF_VERSION%/behaviors-%COMMON_WCF_VERSION%-3.5.dll
move /y behaviors-%COMMON_WCF_VERSION%-3.5.dll Dependencies\IRU.Common.WCF.Behaviors.dll

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/common/entlib/%COMMON_ENTLIB_VERSION%/entlib-%COMMON_ENTLIB_VERSION%-3.5.dll
move /y entlib-%COMMON_ENTLIB_VERSION%-3.5.dll Dependencies\IRU.Common.EnterpriseLibrary.dll

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/rts/classic/rtsdotnet-client/%RTSDOTNET_CLIENT_VERSION%/rtsdotnet-client-%RTSDOTNET_CLIENT_VERSION%-3.5.dll
move /y rtsdotnet-client-%RTSDOTNET_CLIENT_VERSION%-3.5.dll Dependencies\RTSDotNETClient.dll

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/common/iru-schemas/%COMMON_IRU_SCHEMAS_VERSION%/iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-iso-3166-1-alpha-3.xsd
move /y iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-iso-3166-1-alpha-3.xsd IRU.RTS.WS\CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\iso-3166-1-alpha-3.xsd

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/common/iru-schemas/%COMMON_IRU_SCHEMAS_VERSION%/iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-actor.xsd
move /y iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-actor.xsd IRU.RTS.WS\CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\tir-actor-1.xsd

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/common/iru-schemas/%COMMON_IRU_SCHEMAS_VERSION%/iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-carnet.xsd
move /y iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-carnet.xsd IRU.RTS.WS\CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\tir-carnet-1.xsd

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/common/iru-schemas/%COMMON_IRU_SCHEMAS_VERSION%/iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-iso-3166-1-alpha-3.xsd
move /y iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-iso-3166-1-alpha-3.xsd IRU.RTS.WS\TerminationService\IRU.RTS.WS.TerminationService.Interface\TerminationService-1\iso-3166-1-alpha-3.xsd

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/common/iru-schemas/%COMMON_IRU_SCHEMAS_VERSION%/iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-actor.xsd
move /y iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-actor.xsd IRU.RTS.WS\TerminationService\IRU.RTS.WS.TerminationService.Interface\TerminationService-1\tir-actor-1.xsd

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/common/iru-schemas/%COMMON_IRU_SCHEMAS_VERSION%/iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-carnet.xsd
move /y iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-carnet.xsd IRU.RTS.WS\TerminationService\IRU.RTS.WS.TerminationService.Interface\TerminationService-1\tir-carnet-1.xsd

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/common/iru-schemas/%COMMON_IRU_SCHEMAS_VERSION%/iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-iso-3166-1-alpha-3.xsd
move /y iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-iso-3166-1-alpha-3.xsd IRU.RTS.WS\VoucherService\IRU.RTS.WS.VoucherService.Interface\VoucherService-1\iso-3166-1-alpha-3.xsd

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/common/iru-schemas/%COMMON_IRU_SCHEMAS_VERSION%/iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-actor.xsd
move /y iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-actor.xsd IRU.RTS.WS\VoucherService\IRU.RTS.WS.VoucherService.Interface\VoucherService-1\tir-actor-1.xsd

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/common/iru-schemas/%COMMON_IRU_SCHEMAS_VERSION%/iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-carnet.xsd
move /y iru-schemas-%COMMON_IRU_SCHEMAS_VERSION%-tir-carnet.xsd IRU.RTS.WS\VoucherService\IRU.RTS.WS.VoucherService.Interface\VoucherService-1\tir-carnet-1.xsd

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/tirplus/tirplus-model/%TIRPLUS_MODEL_VERSION%/tirplus-model-%TIRPLUS_MODEL_VERSION%-voucher.xsd
move /y tirplus-model-%TIRPLUS_MODEL_VERSION%-voucher.xsd IRU.RTS.WS\VoucherService\IRU.RTS.WS.VoucherService.Interface\VoucherService-1\tirplus-voucher-1.xsd

REM Copy common schema needed in several solutions

copy IRU.RTS.WS\CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\rts-carnet-1.xsd IRU.RTS.WS\TerminationService\IRU.RTS.WS.TerminationService.Interface\TerminationService-1\rts-carnet-1.xsd
copy IRU.RTS.WS\CarnetService\IRU.RTS.WS.CarnetService.Interface\CarnetService-1\rts-carnet-1.xsd IRU.RTS.WS\VoucherService\IRU.RTS.WS.VoucherService.Interface\VoucherService-1\rts-carnet-1.xsd