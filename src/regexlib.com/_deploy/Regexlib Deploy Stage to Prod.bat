:LOOP
ECHO Please Type 'yes' to push the RegexLib stage files to production
SET Choice=
SET /P Choice=Type the word and press Enter: 
ECHO.
IF /I '%Choice%'=='yes' GOTO Confirmed
ECHO "%Choice%" is not valid. Please try again.
ECHO.
GOTO Loop
:Confirmed

@ECHO OFF
SETLOCAL

SET _source=d:\domains\stage.regexlib.com

SET _dest=d:\domains\v2.regexlib.com

SET _what=/SEC /E
:: /SEC :: copy files with SECurity
:: /MIR :: MIRror a directory tree - equivalent to /PURGE plus all subfolders (/E)
:: /PURGE : Delete dest files/folders that no longer exist in source.
:: /E : Copy Subfolders, including Empty Subfolders.

SET _excludeFiles=/XF web.config
SET _excludeFolders=/XD 
:: /XD dirs [dirs]... : eXclude Directories matching given names/paths.

SET _options=/R:0 /W:0 /LOG:RegexlibDeployLog.txt
:: /R:n :: number of Retries
:: /W:n :: Wait time between retries
:: /LOG :: Output log file
:: /NFL :: No file logging
:: /NDL :: No dir logging

ROBOCOPY %_source% %_dest% %_what% %_excludeFiles% %_excludeFolders% %_options%

:End
pause