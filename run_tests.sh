#!/bin/sh

set -eu

dotnet run --project ./test/FizzBuzzWhizz.Tests/ -- "${@}"
