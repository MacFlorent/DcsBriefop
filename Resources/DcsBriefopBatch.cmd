@echo off

set mizPath=%~dp0
set mizFile=%1
set dcsBriefopExe=%2

::extract path for DcsBriefop
for %%i in ("%dcsBriefopExe%") do (
set dcsBriefopPath=%%~pi
set dcsBriefopExe=%%~ni
)

::set execution directory to the DcsBriefop execution path
pushd %dcsBriefopPath%

@echo on
start "" %dcsBriefopExe% batch --mizFile %mizPath%%mizFile% --loglevel info
@echo off

::reset execution directory
popd

::pause