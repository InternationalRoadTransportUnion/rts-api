@echo off

REM Download dependencies on artifactory and rename them accordingly

mkdir Dependencies

set REPO_LIBS_RELEASE_LOCAL_URL=http://repo.iruworld.org/libs-release-local
set COMMON_CRYPTO_VERSION=1.1.0.0

wget %REPO_LIBS_RELEASE_LOCAL_URL%/org/iru/common/crypto/ciphersharp/%COMMON_CRYPTO_VERSION%/ciphersharp-%COMMON_CRYPTO_VERSION%-3.5.dll
move ciphersharp-%COMMON_CRYPTO_VERSION%-3.5.dll Dependencies\IRU.Common.Crypto.Cipher.dll