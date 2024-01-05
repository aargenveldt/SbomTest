using System;
using System.Collections.Generic;

namespace de.Aargenveldt.SbomTest.TypeInspection
{
    /// <summary>
    /// Interface für Klassen, die Informationen zu Assemblies ermitteln können.
    /// </summary>
    public interface IAssemblyInfoProvider
    {
        /// <summary>
        /// Liefert Informationen zu den Assemblies, die die angegebenen
        /// Typen enthalten.
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        IList<AssemblyInfo> GetAssemblyInfos(IEnumerable<Type> types);
    }
}
