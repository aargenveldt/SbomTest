using System;
using System.Collections.Generic;
using System.Linq;

namespace de.Aargenveldt.SbomTest.TypeInspection
{
    /// <summary>
    /// Provider für Assembly Informationen. Standardimplementierung
    /// der <see cref="IAssemblyInfoProvider"/> Schnittstelle.
    /// </summary>
    public class AssemblyInfoProvider : IAssemblyInfoProvider
    {
        /// <inheritdoc/>
        public IList<AssemblyInfo> GetAssemblyInfos(IEnumerable<Type> types)
        {
            List<AssemblyInfo> infos = new List<AssemblyInfo>();

            foreach (Type type in types ?? Enumerable.Empty<Type>())
            {
                try
                {
                    if (type != null)
                    {
                        AssemblyInfo assemblyInfo = new AssemblyInfo()
                        {
                            Name = type.Assembly.GetName().Name,
                            Version = type.Assembly.GetName().Version,
                            Path = type.Assembly.Location
                        };
                        infos.Add(assemblyInfo);
                    }
                }
                catch
                {
                    // NOP
                }
            }

            AssemblyInfo[] retval = infos.Distinct()
                                         .OrderBy(item => item.Name)
                                         .ThenBy(item => item.Version)
                                         .ThenBy(item => item.Path)
                                         .ToArray();
            return retval;
        }
    }
}
