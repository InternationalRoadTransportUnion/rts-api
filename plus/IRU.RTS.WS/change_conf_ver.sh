#!/bin/sh

if [[ ${1+x} ]]; then
  version=$1
else
  asminfos=($(git ls-files */AssemblyInfo.cs ))
  if [[ ${#asminfos[@]} -gt 0 ]]; then
    version=$(grep -h "^\[assembly: AssemblyVersion(" ${asminfos[0]} | awk '{ print gensub(/.*AssemblyVersion\(\"(.*)\"\).*/, "\\1", $0) }')
  else
    version=Unknown
  fi
fi

assemblies=($(git ls-files *.*proj | xargs grep -h "<AssemblyName>" | awk '{ print gensub(/.*<AssemblyName>(.*)<\/AssemblyName>.*/, "\\1", $0) }'))
cfgfiles=($(git ls-files *.config))

for cfgfile in ${cfgfiles[@]}; do
  #echo $cfgfile

  for assembly in ${assemblies[@]}; do
    #echo $assembly
    match="\($assembly, Version=\)\([^,\"]\+\)"
    sed "s/$match/\1$version/g" $cfgfile > tmp.bak
	dos2unix --u2d tmp.bak
	mv tmp.bak $cfgfile
  done
    
done