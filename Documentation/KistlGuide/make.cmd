@echo off
texify --pdf KistlGuide.tex
pause
call makeclean.cmd
start KistlGuide.pdf