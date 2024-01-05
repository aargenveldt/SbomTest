using System;
using System.Collections.Generic;

namespace de.Aargenveldt.SbomTest.TypeInspection
{
    /// <summary>
    /// Container für Assembly Informationen
    /// </summary>
    public class AssemblyInfo : IEquatable<AssemblyInfo>
    {
        /// <summary>
        /// Name der Assembly
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Pfad zur Assembly
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Version der Assembly
        /// </summary>
        public Version Version { get; set; }

        /// <inheritdoc/>
        public bool Equals(AssemblyInfo other)
        {
            bool retval;

            if (other == null)
                retval = false;
            else
                retval = string.Equals(this.Name, other.Name, StringComparison.Ordinal)
                        && string.Equals(this.Path, other.Path, StringComparison.Ordinal)
                        && EqualityComparer<Version>.Default.Equals(this.Version, other.Version);

            return retval;
        }
    }
}
