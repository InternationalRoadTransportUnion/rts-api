#! /bin/sh

set -e

ARTIFACTS_DIR=$1
VERSION_FILE=$2
ARTIFACTORY_URL=$3

version=`cat $VERSION_FILE`

old_pwd=`pwd`
zipfile=services.zip
cd $ARTIFACTS_DIR
zip -r $old_pwd/$zipfile .
cd $old_pwd

mvn deploy:deploy-file -Dfile=$zipfile -DrepositoryId=artifactory -Durl=$ARTIFACTORY_URL -DgroupId=org.iru.rtsplus -DartifactId=rtsplus-services -Dversion=$version -Dpackaging=zip -DgeneratePom=false