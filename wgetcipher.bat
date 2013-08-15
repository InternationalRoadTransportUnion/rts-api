@echo off

REM Download dependencies on artifactory and rename them accordingly

mkdir Dependencies

wget http://repo.iruworld.org/libs-release/org/iru/common/crypto/ciphersharp/1.0.0.0/ciphersharp-1.0.0.0-2.0.dll
move ciphersharp-1.0.0.0-2.0.dll Dependencies\IRU.Common.Crypto.Cipher.dll

