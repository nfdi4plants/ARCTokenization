﻿module ProjectInfo

open Fake.Core

let project = "ARCTokenization"

let testProjects = 
    [
        "tests/ControlledVocabulary.Tests/ControlledVocabulary.Tests.fsproj"
        "tests/ARCTokenization.Tests/ARCTokenization.Tests.fsproj"
    ]

let solutionFile  = $"{project}.sln"

let configuration = "Release"

let gitOwner = "nfdi4plants"

let gitHome = $"https://github.com/{gitOwner}"

let projectRepo = $"https://github.com/{gitOwner}/{project}"

let pkgDir = "pkg"

// Create RELEASE_NOTES.md if not existing. Or "release" would throw an error.
Fake.Extensions.Release.ReleaseNotes.ensure()

let release = ReleaseNotes.load "RELEASE_NOTES.md"

let stableVersion = SemVer.parse release.NugetVersion

let stableVersionTag = (sprintf "%i.%i.%i" stableVersion.Major stableVersion.Minor stableVersion.Patch )

let mutable prereleaseSuffix = ""

let mutable prereleaseTag = ""

let mutable isPrerelease = false