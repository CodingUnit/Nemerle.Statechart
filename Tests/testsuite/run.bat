@del tests\ /Q /F


@copy "..\..\Lib\bin\debug\Nemerle.Statechart.Lib.dll" tests\ 



@copy "c:\Program Files\Nemerle\Net-4.0\Nemerle.dll" tests\




@copy "..\..\Implementation\bin\debug\Nemerle.Statechart.Implementation.dll" tests\ 


@copy "..\fsmtest\bin\debug\fsmtest.exe" tests\ 



"c:\Program Files\Nemerle\Net-4.0\Nemerle.Compiler.Test.exe" -ref:"..\..\bin\debug\Nemerle.Statechart.dll" -ref:"..\..\Lib\bin\Debug\Nemerle.Statechart.Lib.dll" -ref:..\..\Implementation\bin\debug\Nemerle.Statechart.Implementation.dll -ref:"..\fsmtest\bin\debug\fsmtest.exe" -output:tests\ *.n
pause

