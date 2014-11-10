@echo off

REM Download EPD messages schemas on artifactory and rename them accordingly

set REPO_LIBS_RELEASE_LOCAL_URL=http://repo.iruworld.org/libs-release-local
set EPD_MESSAGES_VERSION=1.0.6
set WGET_PARAMS=--no-proxy

SETLOCAL ENABLEEXTENSIONS ENABLEDELAYEDEXPANSION
for %%s in (004 005 009 013 014 015 016 025 028 029 045 051 055 060 917 928) do (  
  set RFILE=epd-messages-%EPD_MESSAGES_VERSION%-epd%%s.xsd 
  wget %WGET_PARAMS% %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/epd/epd-messages/%EPD_MESSAGES_VERSION%/!RFILE!
  move !RFILE! TIREDPG2BRecvrHostService\Schemas\TIREPD_IE%%s.xsd
)

set RFILE=epd-messages-%EPD_MESSAGES_VERSION%-types.xsd
wget %WGET_PARAMS% %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/epd/epd-messages/%EPD_MESSAGES_VERSION%/!RFILE!
move !RFILE! TIREDPG2BRecvrHostService\Schemas\TIREPD_Types.xsd

set RFILE=epd-messages-%EPD_MESSAGES_VERSION%-set.xsd
wget %WGET_PARAMS% %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/epd/epd-messages/%EPD_MESSAGES_VERSION%/!RFILE!
move !RFILE! TIREDPG2BRecvrHostService\Schemas\TIREPD_Set.xsd