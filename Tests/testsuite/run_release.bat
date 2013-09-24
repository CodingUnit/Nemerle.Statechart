@del tests\ /Q /F




@copy "..\..\Lib\bin\debug\Nemerle.Statechart.Lib.dll" tests\ 





@copy "c:\Program Files\Nemerle\Net-4.0\Nemerle.dll" tests\






@copy "..\..\bin\debug\Nemerle.Statechart.dll" tests\ 

@copy "..\..\Nemerle.Statechart.Runtime\bin\debug\Nemerle.Statechart.Runtime.dll" tests\ 




@copy "..\fsmtest\bin\debug\fsmtest.exe" tests\ 





"c:\Program Files\Nemerle\Net-4.0\Nemerle.Compiler.Test.exe" -ref:"tests\Nemerle.Statechart.dll" -ref:"tests\Nemerle.Statechart.Lib.dll" -ref:"tests\fsmtest.exe" -ref:"tests\Nemerle.Statechart.Runtime.dll" -output:tests\ *.n
pause


