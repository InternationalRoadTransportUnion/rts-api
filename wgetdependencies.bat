@echo off

REM Download dependencies on artifactory and rename them accordingly

set REPO_LIBS_RELEASE_LOCAL_URL=http://repo.iruworld.org/libs-release-local

REM Common dependencies


REM classic dependencies

cd classic
call wgetdependencies.bat
cd ..

REM plus dependencies

cd plus
call wgetdependencies.bat
cd ..