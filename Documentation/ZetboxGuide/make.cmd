@echo off
texify --pdf ZetboxGuide.tex
pause
call makeclean.cmd
start ZetboxGuide.pdf