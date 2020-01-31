@echo off
echo Starting deploy package...

set BuildFolder=%1
set ProductName=%2
set BuildVersion=%3
set TargetFolder=%4

set PackageFile=%BuildFolder%\%ProductName%\%ProductName%_%BuildVersion%.zip

if not exist %PackageFile% (
	echo Deploy package not exist: %PackageFile%
	echo Exit!
	exit 1
)

echo Deploy package file: %PackageFile%

if not exist %TargetFolder% (
	echo Target deployment folder not exist: %TargetFolder%
	echo Exit!
	exit 1
)

echo Extracting file from to (excluded word template docs)
"D:\Program Files\7-Zip\7z.exe" x %PackageFile% -y -o%TargetFolder% -xr!docTemplates > nul

echo Extracting word template docs to path %TargetFolder%
"D:\Program Files\7-Zip\7z.exe" e %PackageFile% -y -o%TargetFolder%\docTemplates -i!docTemplates\*.doc > nul

echo Finish deploying package!

:eof