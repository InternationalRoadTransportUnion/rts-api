@echo off

REM Download dependencies on artifactory and rename them accordingly

mkdir Dependencies

wget http://apps.charybdis.iruworld.org/swf/artifactory/libs-release/org/iru/common/crypto/ciphersharp/1.0.0.0/ciphersharp-1.0.0.0-2.0.dll
move ciphersharp-1.0.0.0-2.0.dll Dependencies\IRU.Common.Crypto.Cipher.dll

