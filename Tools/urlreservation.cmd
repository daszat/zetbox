@echo off
netsh http add urlacl url=http://+:6666/ user="LAN\Domain Users"
pause