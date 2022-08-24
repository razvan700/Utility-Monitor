// See https://aka.ms/new-console-template for more information
using System;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
class program
{
    static void Main(string[] args)
    { 
        if(args.Length != 3) 
        {
            Console.WriteLine("Please give exactly three arguments in the command line");
            return;
        }
        // TODO Make sure variables timeBetweenEvaluations and timeMaximumLifeSpan are numbers
        int minToMillisecond = 60000;
        string processName = args[0];
        int timeBetweenEvaluations = Int32.Parse(args[2]);
        int timeMaximumLifeSpan = Int32.Parse(args[1]);
        
        // considerates the processe that has been passed from the console
        Process[] allProcesses = Process.GetProcessesByName(processName); 
    
        while(true)
        {
            Console.WriteLine("Number of processes:{0}", allProcesses.Length);
            foreach(Process proc in allProcesses) 
            {
                TimeSpan runtime;
                try {
                    runtime = DateTime.Now - proc.StartTime;
                }
                catch (Win32Exception ex) {
                    if (ex.NativeErrorCode == 5)
                        continue;   
                    throw;
                }
                Console.WriteLine(runtime.Minutes);
                if( runtime.Minutes > timeMaximumLifeSpan) 
                {
                    proc.Kill();
                }
                
            }   
            Thread.Sleep(timeBetweenEvaluations * minToMillisecond);
        }
    }
}