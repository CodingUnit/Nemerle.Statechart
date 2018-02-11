@echo off

mkdir tests\

del tests\ /Q /F
del *.txt


copy "c:\Program Files\Nemerle\Net-4.0\Nemerle.dll" tests\

copy "..\..\Nemerle.Statechart.Runtime\bin\debug\*.dll" tests\ 

copy "..\..\CommonLib.Macros\bin\debug\CommonLib.Macros.dll" tests\ 

copy "..\fsmtest\bin\debug\fsmtest.exe" tests\ 

"c:\Program Files\Nemerle\Net-4.0\Nemerle.Compiler.Test.exe" -ref:"..\..\bin\debug\Nemerle.Statechart.dll" -ref:"tests\CommonLib.Macros.dll" -ref:"tests\System.Reactive.Linq.dll" -ref:"tests\System.Reactive.Core.dll" -ref:"tests\System.Reactive.Interfaces.dll" -ref:"tests\Nemerle.Statechart.Runtime.dll" -ref:"..\fsmtest\bin\debug\fsmtest.exe" -output:tests\ positive\*.n negative\*.n
pause

