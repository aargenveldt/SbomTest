# SBOM Test

Testprojekt für die Bereitstellung einer SBOM (= Software Bill Of Material).



[TOC]

## Motivation

Die Solution ```SbomTest``` dient als Ausgangspunkt für die Erstellung einer SBOM.

Die Solution verwendet verschiedene Komponenten/Bibliotheken, die via NuGet eingebunden werden. Einige weisen bekannte Sicherheitsprobleme auf und/oder sind veraltet. Details siehe unten.



## Aufbau der Solution

Die Solution besteht aus drei Unterprojekten:

1. TypeInspection (.NET Standard 2.0)

   Implementiert eine Klasse, die eine Menge von Typen entgegennimmt, für jeden die Assembly bestimmt, die die Implementierung liefert, und die Informationen (Name, Version) dieser Assembly liefert.

2. BusinessLayer (.NET Framework 4.8.1)

   Repräsentiert die Implementierung einer Geschäftslogik in einem realen Projekt. Hier werden einige externe Komponenten via NuGet referenziert und eingebunden (Details siehe unten). Das Business Objekt liefert dann eine Auflistung mit Informationen zu diesen Assemblies.

3. **SbomTestCLI** (.NET Framework 4.8.1)

   Ein Konsolenprogramm zur Ausgabe der Ergebnisse des Businessobjekts.
   
   Im Projekt wird ```AutoFac``` als IOC Container verwendet.
   
   Das Konsolenprogramm kann ohne Parameter gestartet werden und zeigt dann im Konsolenfenster ein Banner, die Version - und eine Liste (ausgewählter) Assemblies, die intern Verwendung finden.



## Verwendete Komponenten

Stand: 05.01.2024 

Hinweise zu Versionen, Sicherheitsproblemen etc. beziehen sich auf diesen Stichtag.

* BusinessLayer

  Die referenzierten Komponenten werden **nicht** verwendet... Es werden lediglich Informationen zu den referenzierten Assemblies gesammelt und für eine Ausgabe bereitgestellt.

  * ```Castle.Core``` v5.1.1

  * ```EntityFramework``` v6.4

    ⚠ Diese Version ist veraltet.

  * ```Newtonsoft.Json``` v11.0.1

    ⚠ Diese Version ist veraltet.

    ⛔ Diese Version weist ein Sicherheitsproblem auf: [CVE-2024-21907](https://github.com/advisories/GHSA-5crp-9r3c-p9vr) "Improper Handling of Exceptional Conditions in Newtonsoft.Json"

  * ```Polly``` v8.2.0

  * ```Snowflake.Data```v2.0.1

    ⚠ Diese Version ist veraltet.

    ⛔ Diese Version weist ein Sicherheitsproblem auf: [CVE-2023-34230](https://github.com/advisories/GHSA-223g-8w3x-98wr) "Snowflake Connector .Net Command Injection"
    
    
    
  * ```Serilog```v2.11.0

    ⚠ Diese Version ist veraltet.

    ⛔ Diese Bibliothek ist **direkt** referenziert (aus ```..\lib\log4net.dll```) - **nicht via NuGet**!!!

    Da diese Komponente direkt referenziert wird, wird sie von den SBOM Generatoren (z.B. CycloneDX) im Regelfall ignoriert. D.h., dass diese Komponente **nicht in einer generierten SBOM erscheint!!!**

    Kurioses am Rande: Ursprgl. war ```log4net```v1.2.15.0 direkt referenziert, um diesen Testfall abzudecken... Aber ```Snowflake.Data``` hat eine Referenz auf ```log4net```v2.0.12 eingeschleppt - und die hat das Buildsystem nachhaltig verwirrt: Die Referenz auf ```log4net```v1.2.15.0 konnte nicht mehr aufgelöst und die Solution deshalb nicht mehr erstellt werden... Gleichzeitig konnte keine Referenz auf ```log4net```v1.2.15.0 mehr explizit gesetzt werden, da *das Build System diese Referenz bereits automatisch erstellt hat* (aka: Via NuGet Abhängigkeit hinter den Kulissen aufgelöst hat).

* SbomTestCLI

  * ```Autofac``` v7.1.0

    IOC Container.

  * ```Colorful.Console``` v1.2.15

    Farbige Ausgaben im Konsolenfenster.

  * ```Figgle```v0.5.1

    Ausgabe von ASCII Art Bannern.

Viele der referenzierten Komponenten bringen weitere Abhängigkeiten mit, die wiederum via NuGet aufgelöst werden. ```Snowflake.Data```verwendet als Schmankerl zudem ```Newtonsoft.Json``` in einer anderen Version (v13.x). Aus diesem Grunde kommt es zu einem Downgrade der ```Newtonsoft.Json``` Bibliothek (von v13.x auf v11.0.1), da die Version 11.0.1 direkt referenziert wird... Beim Erstellen der Solution wird eine entsprechende Warnung ausgegeben.

## SBOM

SBOM = Software Bill Of Materials = Software "Materialliste"&rarr; Software Lieferkette

### Formate

Es existieren verschiedene SBOM Formate. Die wichtigsten sind:

* Software Package Data Exchange (SPDX)

  SPDX, ein Projekt der Linux Foundation, wurde mit der Absicht gegründet, ein gemeinsames Datenaustauschformat für Informationen über  Softwarepakete zur gemeinsamen Nutzung und Sammlung zu schaffen.

  SPDX ist das einzige SBOM Format mit ISO Zertifizierung.

* CycloneDX

  CycloneDX entstammt direkt der OWASP Foundation (Open Web Application Security Project) und ist ein *leichtgewichtiger SBOM-Standard, der für den Einsatz in  Anwendungssicherheitskontexten und für die Analyse von Komponenten in  der Lieferkette entwickelt wurde*.

  Das Besondere an CycloneDX ist, dass es von Anfang an als  Stücklistenformat konzipiert wurde und eine Vielzahl von  Anwendungsfällen abdeckt, darunter auch Software-as-a-Service-BOM  (SaaSBOM). CycloneDX unterstützt außerdem eine Vielzahl von  Anwendungsfällen über Software hinaus.

* Software Identification (SWID) Tagging

  SWID ist in erster Linie auf Lizenzierung ausgelegt.

### Tools

Es existieren für alle SBOM Formate verschiedene Tools zur Generierung, Modifikation, Konvertierung und Verwaltung. Microsoft integriert z.B. SPDX in die Toolchain...

Die Generierung einer SBOM ist allerdings nur ein Teil im Rahmen eines sicheren Entwicklungsprozesses. Ebenso wichtig ist die administrative Sicht, die auf Basis von SBOM Monitoring, Auswertungen und die Durchführung von Maßnahmen erlaubt. Es kommt also wieder mal ein ganzer Stapel von Tools und Anwendungen zum Einsatz.

Dieses .NET Projekt wurde aufgesetzt, um die projektorientierte Analyse von Risiken in der Software Lieferkette mit Hilfe der Software [dependency track](https://dependencytrack.org/) zu erproben. ```dependency track``` arbeitet auf Basis von SBOM im CycloneDX Format. - Entsprechend wurde das Tooling für CycloneDX aufgesetzt...

### CycloneDX für .NET installieren

Das CycloneDX Modul für .NET ist für die .NET (ehemals .NET Core) Entwicklungsumgebung ausgelegt. Entsprechend erfolgt der Zugriff über das Schweizer Taschenmesser der .NET Softwareentwicklung: ```dotnet.exe```.

Entsprechend muss vorab eine entsprechende Entwicklungsumgebung (z.B. per Installation eines aktuellen Visual Studio) geschaffen werden...

Installation via NuGet

```cmd
dotnet tool install --global CycloneDX
```

Aktualisierung via NuGet

```cmd
dotnet tool update --global CycloneDX
```

CycloneDX kann, sobald installiert, auch auf .NET Framework Projekte angewendet werden!

### Wie CycloneDX für .NET arbeitet

CycloneDX kann auf eine .NET (auch .NET Framework) Solution - oder ein einzelnes Projekt - angewendet werden. Ausgewertet werden alle **via NuGet** referenzierten Komponenten. Für diese ermittelt das Tool alle notwendigen Informationen - und folgt dabei den jeweiligen Abhängigkeiten rekursiv. Aus den gewonnenen Informationen wird die SBOM in Form einer XML oder JSON Datei erstellt.

**Dafür wird Internet Zugang für den Zugriff auf nuget.org benötigt.**

Werden projekt- oder firmenspezifische NuGet Server verwendet, dann können diese per Parameter mit angegeben werden, so dass sie in die Auswertung von Komponenten mit einbezogen werden.

⚠**Wichtig**⚠

CycloneDX kann (derzeit - Stand 05.01.2024) keine direkten Referenzen auswerten. Wird also eine Komponente via direkter Referenz (z.B. aus einem lokalen lib Ordner) eingebunden, **dann erscheint diese Komponente <u>nicht</u> in der SBOM!!!**

### SBOM für die SbomTest Solution generieren

Aufruf aus einem Konsolenfenster im Pfad der Solution:

```cmd
dotnet CycloneDX sbomtest.sln -o ./ -j -st Application -sv 1.0.0
```

| Parameter             | Beschreibung                                                 |
| --------------------- | ------------------------------------------------------------ |
| ```sbomtest.sln```    | Angabe der Solution, die ausgewertet werden soll; hier Angabe mit relativem (bzw. ohne) Pfad, da das Kommando angenommen im Verzeichnis der Solution ausgeführt wird. |
| ```-o ./```           | Ausgabeverzeichnis/-datei festlegen. Mit ```./```erfolgt die Ausgabe im aktuellen Verzeichnis unter dem Standardnamen ```bom.xml```bzw. ```bom.json```. |
| ```-j```              | Ausgabe im JSON Format (```bom.json```). - Ansonsten erfolgt die Ausgabe im XML Format. |
| ```-st Application``` | Legt den Typ der SBOM fest (hier also SBOM für eine Anwendung). |
| ```-sv 1.0.0```       | Version der Komponente = Anwendung (ohne Angabe wird 0.0.0 verwendet). |

### Beispiel: Generierte SBOM

Die Beispieldatei ist im JSON Format (```bom.json```). Die Beispieldatei ist gekürzt (siehe ```...```):

```json
{
  "bomFormat": "CycloneDX",
  "specVersion": "1.5",
  "serialNumber": "urn:uuid:1d93788e-773d-49bf-9f0d-1996a0f0c264",
  "version": 1,
  "metadata": {
    "timestamp": "2024-01-05T13:32:12Z",
    "tools": [
      {
        "vendor": "CycloneDX",
        "name": "CycloneDX module for .NET",
        "version": "3.0.4.0"
      }
    ],
    "component": {
      "type": "application",
      "bom-ref": "sbomtest@1.0.0",
      "name": "sbomtest",
      "version": "1.0.0"
    }
  },
  "components": [
    {
      "type": "library",
      "bom-ref": "pkg:nuget/AngleSharp@0.15.0",
      "author": "AngleSharp",
      "name": "AngleSharp",
      "version": "0.15.0",
      "description": "AngleSharp is the ultimate angle brackets parser library. It parses HTML5, CSS3, and XML to construct a DOM based on the official W3C specification.",
      "scope": "required",
      "hashes": [
        {
          "alg": "SHA-512",
          "content": "050BAF8F6E357F483EFF82154D6E9CF76DC4C0D5B819AA8C2B9F591D26D2CEA79AABF74365AAB9347DD02F4F13772E1EA994206708C5C8563C88B690CA453897"
        }
      ],
      "licenses": [
        {
          "license": {
            "id": "MIT"
          }
        }
      ],
      "copyright": "Copyright 2013-2021, AngleSharp.",
      "purl": "pkg:nuget/AngleSharp@0.15.0",
      "externalReferences": [
        {
          "url": "https://anglesharp.github.io/",
          "type": "website"
        }
      ]
    },
    ...
  ],
  "dependencies": [
    {
      "ref": "pkg:nuget/AngleSharp@0.15.0",
      "dependsOn": [
        "pkg:nuget/System.Text.Encoding.CodePages@5.0.0"
      ]
    },
    ...
    {
      "ref": "sbomtest@1.0.0",
      "dependsOn": [
        "pkg:nuget/Autofac@7.1.0",
        "pkg:nuget/Castle.Core@5.1.1",
        "pkg:nuget/Colorful.Console@1.2.15",
        "pkg:nuget/EntityFramework@6.4.0",
        "pkg:nuget/Figgle@0.5.1",
        "pkg:nuget/Newtonsoft.Json@11.0.1",
        "pkg:nuget/Polly@8.2.0",
        "pkg:nuget/Snowflake.Data@2.0.1",
        "pkg:nuget/NETStandard.Library@2.0.3"
      ]
    }
  ]
}
```

