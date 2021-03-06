#!/usr/bin/env bash

set -e
set -o allexport
eval $(cat .env | tr -d '\r' | sed 's_%\([^%]*\)%_$\1_g')
set +o allexport

set -eu
set -o pipefail

# liberated from https://stackoverflow.com/a/18443300/433393
realpath() {
  OURPWD=$PWD
  cd "$(dirname "$1")"
  LINK=$(readlink "$(basename "$1")")
  while [ "$LINK" ]; do
    cd "$(dirname "$LINK")"
    LINK=$(readlink "$(basename "$1")")
  done
  REALPATH="$PWD/$(basename "$1")"
  cd "$OURPWD"
  echo "$REALPATH"
}

TOOL_PATH=$(realpath .fake)
FAKE="$TOOL_PATH"/fake

if ! [ -e "$FAKE" ]
then
  dotnet tool install fake-cli --tool-path "$TOOL_PATH"
fi
"$FAKE" run scripts/build.fsx $@
