#!/usr/bin/env bash

set -ex

pushd src/Diff.Cli

echo "HELLO" > a.txt
echo "AHELLO-" > b.txt
dotnet run -- a.txt b.txt

rm a.txt b.txt

popd