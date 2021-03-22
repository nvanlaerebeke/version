using System;

namespace version
{
    public class VersionModification
    {      
        public Version Verify(string version) {
            if(Version.TryParse(version, out var obj)) {
                return obj;
            } 
            
            Console.Error.WriteLine("Validation Error");
            Environment.Exit(1);

            return null;
        }

        public Version Up(string version, string versiontype) {
            var objVersion = Verify(version);
            var major = objVersion.Major;
            var minor = objVersion.Minor;
            var build = objVersion.Build;
            var revision = objVersion.Revision;

            switch(versiontype.ToLower()) {
                case "major":
                    return GetNewVersion(++major, minor, build, revision);
                case "minor":
                    return GetNewVersion(major, ++minor, build, revision);
                case "build":
                    return GetNewVersion(major, minor, ++build, revision);
                case "revision":
                    return GetNewVersion(major, minor, build, ++revision);
            }
            return objVersion;
        }

        public Version Down(string version, string versiontype) {
            var objVersion = Verify(version);
            var major = objVersion.Major;
            var minor = objVersion.Minor;
            var build = objVersion.Build;
            var revision = objVersion.Revision;

            switch(versiontype.ToLower()) {
                case "major":
                    return GetNewVersion(--major, minor, build, revision);
                case "minor":
                    return GetNewVersion(major, --minor, build, revision);
                case "build":
                    return GetNewVersion(major, minor, --build, revision);
                case "revision":
                    return GetNewVersion(major, minor, build, --revision);
            }
            return objVersion;
        }

        public Version Reset(string version, string versiontype) {
            var objVersion = Verify(version);
            var major = objVersion.Major;
            var minor = objVersion.Minor;
            var build = objVersion.Build;
            var revision = objVersion.Revision;

            switch(versiontype.ToLower()) {
                case "major":
                    return GetNewVersion(0, minor, build, revision);
                case "minor":
                    return GetNewVersion(major, 0, build, revision);
                case "build":
                    return GetNewVersion(major, minor, 0, revision);
                case "revision":
                    return GetNewVersion(major, minor, build, 0);
            }
            return objVersion;
        }

        private Version GetNewVersion(int major, int minor, int build, int revision) {
            if(major != -1 && minor != -1 && build != -1 && revision != -1) {
                return new Version(major, minor, build, revision);
            } else if(major != -1 && minor != -1 && build != -1) {
                return new Version(major, minor, build);
            } else if(major != -1 && minor != -1) {
                return new Version(major, minor);
            } else if(major != -1) {
                return new Version(major.ToString());
            } else {
                Console.Error.WriteLine("Invalid Version Number");
                Environment.Exit(2);
            }
            return null;
        }
    }
}