using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace VS2017MSBuildAutoVersioning
{
    public class GetAutoVersion : Task
    {
        [Output]
        public string AutoVersion { get; set; }

        public string BaseVersion { get; set; }

        public override bool Execute()
        {
            var vs = (this.BaseVersion ?? "1.0.0").Split('.');

            if (vs.Length < 4)
                vs = vs.Concat(Enumerable.Range(1, 4 - vs.Length).Select(_ => "*")).ToArray();

            this.AutoVersion = "";

            for (var i = 0; i < 4; i++)
            {
                if(!vs[i].Contains("*"))
                    this.AutoVersion += vs[i];
                else
                    switch (i)
                    {
                        case 0:
                        case 1:
                            Log.LogError($"Invalid wildcard usage: {this.BaseVersion}");
                            return false;
                        case 2:
                            this.AutoVersion += vs[i].Replace("*", GetAutoBuildVersion().ToString());
                            break;
                        default:
                            this.AutoVersion += vs[i].Replace("*", GetAutoRevisionNumber().ToString());
                            break;
                    }

                if (i != 3)
                    this.AutoVersion += ".";
            }

            Log.LogMessage(MessageImportance.High, $"AutoVersion: {this.AutoVersion}");
            return true;
        }

        static int GetAutoBuildVersion()
        {
            return (DateTime.Today - new DateTime(2000, 1, 1)).Days;
        }

        static int GetAutoRevisionNumber()
        {
            var d = DateTime.UtcNow;
            return ((int)new TimeSpan(d.Hour, d.Minute, d.Second).TotalSeconds) / 2;
        }
    }
}
