#! /bin/sh
mvn -o -N validate|grep 'Building rtsplus-services'|awk '{print $4}' > version.txt