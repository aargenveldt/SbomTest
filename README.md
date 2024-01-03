# SBOM Test

Testprojekt für die Bereitstellung einer SBOM (= Software Bill Of Material).



[TOC]

## Motivation

Die Solution ```SbomTest``` dient als Ausgangspunkt für die Erstellung einer SBOM. Die Solution besteht aus drei Unterprojekten:

1. TypeInspection (.NET Standard 2.0)

   Implementiert eine Klasse, die eine Menge von Typen entgegennimmt, für jeden die Assembly bestimmt, die die Implementierung liefert, und die Informationen (Name, Version) dieser Assembly liefert.

2. BusinessLayer (.NET Framework 4.8.1)

   Repräsentiert die Implementierung einer Geschäftslogik in einem realen Projekt. Hier werden einige externe Komponenten via NuGet referenziert und eingebunden. Das Business Objekt liefert dann eine Auflistung mit Informationen zu diesen Assemblies.

3. SbomTestCLI (.NET Framework 4.8.1)

   Ein Konsolenprogramm zur Ausgabe der Ergebnisse des Businessobjekts.

