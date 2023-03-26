cd %TMP%
for /f %%i in ('dir /a:d /s /b scoped_dir*') do (rd /s /q %%i)