### 6.1.0 – (Released 2025-02-12)

- Changes:
  - [Dependency updates for FsSpreadsheet & ARCtrl](https://github.com/nfdi4plants/ARCTokenization/pull/70)

### 6.0.0 – (Released 2024-04-30)

- Changes:
  - Dependency changes

### 5.0.0 - (Released 2024-04-19)

- Additions:
  - [Fix various term names and expand ARCMock in preparation for supporting ARC-specification draft 2.0.0 and isa-light](https://github.com/nfdi4plants/ARCTokenization/pull/57)

### 4.0.0 - (Released 2024-03-02)

- Additions:
  - [Add enhanced Tokenization for Specific Folders and Files](https://github.com/nfdi4plants/ARCTokenization/pull/53)

### 3.0.0 - (Released 2024-01-10)

- Additions:
    - [Add basic process graph tokenization](https://github.com/nfdi4plants/ARCTokenization/pull/48/commits/e6cd1775575aaac5aca3d2a48ff26fd31b136038):
        - Based on ARCtrl ARCTable
        - add ARC Process Graph Structural ontology (APGSO)
    - [Add filesystem token-based metadata parsing](https://github.com/nfdi4plants/ARCTokenization/pull/49)
    - Most API endpoints now have a slightly different signature, meaning this is a major release

- Bugfixes:
    - Fix Option.defaultValue usage throwing errors although sheet names were present ([#35](https://github.com/nfdi4plants/ARCTokenization/issues/35) and [#36](https://github.com/nfdi4plants/ARCTokenization/issues/36)) via  [this commit](https://github.com/nfdi4plants/ARCTokenization/commit/560d46bfb11df5587f06a6da990bf49dad88334b) inspired by this [PR](https://github.com/nfdi4plants/ARCTokenization/pull/37)

### 2.0.0 - (Released 2024-01-08)

Changes in existing ontology terms will now ways result in a new major version of the package.

- BugFixes:
    - [Fix Assay Technology Type TAN being marked obsolete](https://github.com/nfdi4plants/ARCTokenization/issues/47)

### 1.4.0 - (Released 2024-01-05)
- Additions: [Offer more different sets of CvTerms and OboTerms for structural ontologies](https://github.com/nfdi4plants/ARCTokenization/commit/f44ab3835725b1e86657f778045474438f51f24c)
    - includes: all, non-root, non-obsolete, non-root + non-obsolete, obsolete

### 1.3.0 - (Released 2024-01-03)
- Additions: [Add metadata token Mock API](https://github.com/nfdi4plants/ARCTokenization/commit/7ba554848542f46a60bdb82c54d84d186d6e685e)

- Bugfixes:
    - Fix multiple structural term relations [#42](https://github.com/nfdi4plants/ARCTokenization/issues/42)

### 1.2.0 - (Released 2023-10-26)
- Additions:
    - [Add file system tokenization](https://github.com/nfdi4plants/ARCTokenization/commit/57de162d50c918f1e245f1aa34db0a6b1660ba3b)

### 1.1.0 - (Released 2023-10-25)
- Additions:
    - [Add some temporary static modules representing structural ontologies](https://github.com/nfdi4plants/ARCTokenization/commit/7d37cfc8a52accbc37df63e95a39d2684e66535f)

### 1.0.3 - (Released 2023-8-21)
- Additions:
    - Update FsOboParser dependency to 0.3.0

### 1.0.2 - (Released 2023-8-21)
- Additions:
    - Include structural ontologies in the nuget package
- Bugfixes:
    - Add missing follows relationships for [STUY](https://github.com/nfdi4plants/ARCTokenization/issues/31) and [orcid](https://github.com/nfdi4plants/ARCTokenization/issues/30) terms

### 1.0.1 - (Released 2023-8-20)

- Bugfixes:
    - [Fix typos in structural ontologies](https://github.com/nfdi4plants/ARCTokenization/commit/2dfd46f6884a61853baa76971620e868b66b4987), thanks [@omaus](https://github.com/omaus)

### 1.0.0+

Starting from 1.0.0, Versions of the packages in this project are decoupled, meaning this single release notes page does not work anymore.

For the individual package release notes, please refer to these files:
- [ControlledVocabulary](./src/ControlledVocabulary/RELEASE_NOTES.md)
- [ARCTokenization](./src/ARCTokenization/RELEASE_NOTES.md)

### 1.0.0+e7faa34 (Released 2023-8-17)
* Additions:
    * [[#e7faa34](https://github.com/nfdi4plants/ArcGraphModel/commit/e7faa348c1d8128205a40e2186914380c466fed6)] Add ConvertTokens tests
    * [[#1254645](https://github.com/nfdi4plants/ArcGraphModel/commit/1254645f3a7b4013d4e683b2fd44946e90596658)] Add tests for parseKeyWithTerms
    * [[#ccb95fd](https://github.com/nfdi4plants/ArcGraphModel/commit/ccb95fd918a7cd4e032dd42577333745d71582c3)] Add conversion convenience functions for CvParam and UserParam
    * [[#8ecd66b](https://github.com/nfdi4plants/ArcGraphModel/commit/8ecd66be81caf58af01c555c084642268170e588)] #29 add orcid terms, add a list of obsolete terms as convenience functions, add and adapt tests
    * [[#04f5158](https://github.com/nfdi4plants/ArcGraphModel/commit/04f5158b5c5a490f2800a1666780bb5e1be0f953)] Merge pull request #28 from nfdi4plants/rework-cv
    * [[#16926d5](https://github.com/nfdi4plants/ArcGraphModel/commit/16926d5972ce740255f70ea6521b602796cc7b40)] Add more static member tests for CvParam
    * [[#9f393ea](https://github.com/nfdi4plants/ArcGraphModel/commit/9f393ea78e5406e2ccc32263aff91c8f57fd8190)] Add some tests for CvParam
    * [[#b2aac3b](https://github.com/nfdi4plants/ArcGraphModel/commit/b2aac3b0f57b5b647f7216d6acdbc209a6060975)] implement Param convenience type static members on CvParam and UserParam
    * [[#d2ae48c](https://github.com/nfdi4plants/ArcGraphModel/commit/d2ae48c6397ae3c0a882dbea0cf91c6d9e6644d3)] implement all static members from ParamBase and CvBase convenience types on Param convenience type
    * [[#f4bfe52](https://github.com/nfdi4plants/ArcGraphModel/commit/f4bfe52793dccfe7a77a9da48a6c13c415fc87a0)] Add explicit interface implementations for CvParam and UserParam
    * [[#ec8e312](https://github.com/nfdi4plants/ArcGraphModel/commit/ec8e312e20ad1e92f73528a3834c5dc912454c6f)] move some convenience functions into type extensions to better reflect naming based on input type
    * [[#b3ddf3c](https://github.com/nfdi4plants/ArcGraphModel/commit/b3ddf3cc7332500b0e4869cb847b01daf20e4eca)] rename CvTern Value field -> Name
    * [[#2293e3f](https://github.com/nfdi4plants/ArcGraphModel/commit/2293e3f66aafcb668f4e31d38adbb05f44d36dbc)] Rework CvTerm and CvUnit as record, use unified naming for their fields (Accession, Value, RefUri)
* Deletions:
    * [[#28599f7](https://github.com/nfdi4plants/ArcGraphModel/commit/28599f726526e307f5f7dec825228dee10a45522)] remove fable tool dependency
    * [[#8767c8d](https://github.com/nfdi4plants/ArcGraphModel/commit/8767c8d4580eceac05e3d20d4bb2a3389e45eb0d)] remove fgl dependency
* Bugfixes:
    * [[#3decc9c](https://github.com/nfdi4plants/ArcGraphModel/commit/3decc9ca1a4d40d3a752192b12931759915a345b)] fix some occurences of Param.tryParam
    * [[#c6e2072](https://github.com/nfdi4plants/ArcGraphModel/commit/c6e2072d529ede21912f46d7059a145f0aff8db3)] Update to FsSpreadsheet 3.4 (soon 4.0), fix nbackwards incompatible stuff, use dense rows
    * [[#eebb4f4](https://github.com/nfdi4plants/ArcGraphModel/commit/eebb4f4c0f6a482fdb48512c88c615d43d67b017)] update ontologies with structural `follows` relationships, fix some numbering errors

### 0.2.0+c841135 (Released 2023-8-4)
* Additions:
    * [[#196b63e](https://github.com/nfdi4plants/ArcGraphModel/commit/196b63ec4229ef3535c8556ed12e8820b5e42510)] release 0.1.0-preview.2
    * [[#417129d](https://github.com/nfdi4plants/ArcGraphModel/commit/417129df0c694a1c0df35947d04227d9d57a4320)] target netstandard2.0, use nuget lockfiles
    * [[#b081d48](https://github.com/nfdi4plants/ArcGraphModel/commit/b081d483a767452d0965ca98b5525ca3e1bfccd1)] Merge pull request #12 from nfdi4plants/patch-1
    * [[#74c57e6](https://github.com/nfdi4plants/ArcGraphModel/commit/74c57e61aa5cd798cf0702f9daab053b14af32b2)] Merge pull request #13 from nfdi4plants/isaDataModel
    * [[#9c3fe89](https://github.com/nfdi4plants/ArcGraphModel/commit/9c3fe899908d85b434712c7b5b97a355b354e467)] Update worksheet tokenization functions
    * [[#6ccf979](https://github.com/nfdi4plants/ArcGraphModel/commit/6ccf979cf38507f93897442a4d4dc4d7dee1a859)] Merge pull request #17 from nfdi4plants/feature-newIOParser-#16
    * [[#2a49bfd](https://github.com/nfdi4plants/ArcGraphModel/commit/2a49bfd8845bbf4ea5f1e3f53e267d38d12eb452)] Update NuGet versions of external libraries
    * [[#4623a46](https://github.com/nfdi4plants/ArcGraphModel/commit/4623a466a6495c9cf8ed590d4aa4c48b50b39863)] Update key parser and worksheet parsers
    * [[#96e9fa5](https://github.com/nfdi4plants/ArcGraphModel/commit/96e9fa5b67aac2643a116d37dfdd85ca7cf2ca59)] update ci
    * [[#89a58cb](https://github.com/nfdi4plants/ArcGraphModel/commit/89a58cbbdba5faf8453649c8a89a90484f5b42c4)] re-add TableTransform xd
    * [[#570a7d2](https://github.com/nfdi4plants/ArcGraphModel/commit/570a7d27b6388d1ac475d8943bba5d3319e20661)] Finalize parsing of FsWorkbook into CvParams
    * [[#09f8887](https://github.com/nfdi4plants/ArcGraphModel/commit/09f88879b247c88b46025f697c7778ae508f8953)] Add wip obo structure for arc metadata sections
    * [[#1319a6a](https://github.com/nfdi4plants/ArcGraphModel/commit/1319a6a4e2976dd2ac69fe6185719bed22493e80)] Rename libraries
    * [[#69e53ec](https://github.com/nfdi4plants/ArcGraphModel/commit/69e53ec450d6754d2c48bebfde1cc225dcc8ae5f)] Add some test helpers
    * [[#5cd5589](https://github.com/nfdi4plants/ArcGraphModel/commit/5cd5589bf0187c174958801b85d532bb7df17a05)] Add top level parsing function
    * [[#e9e8bc2](https://github.com/nfdi4plants/ArcGraphModel/commit/e9e8bc2e3eb2510760b1dff05e8890d574b68b82)] Add first simple integration test for annotation table parsing
    * [[#c6192db](https://github.com/nfdi4plants/ArcGraphModel/commit/c6192db7453d3fe869012415820eace18d34c777)] Add characteristics integration test
    * [[#e79cbd1](https://github.com/nfdi4plants/ArcGraphModel/commit/e79cbd1c676f42451983989799259e1e86cdbbf2)] update investigation structural ontology
    * [[#ba26b43](https://github.com/nfdi4plants/ArcGraphModel/commit/ba26b4311523c504fb8482c7946730a1d915eb47)] add study structural ontology
    * [[#3a0f1a7](https://github.com/nfdi4plants/ArcGraphModel/commit/3a0f1a7511a472dd227059441c072d803230c26f)] Merge pull request #24 from nfdi4plants/feature-updateTokenization-#20
    * [[#ebe468d](https://github.com/nfdi4plants/ArcGraphModel/commit/ebe468d31774e87f73e7f923795af712915a6eaa)] add assay metadata structural ontology
    * [[#bd06096](https://github.com/nfdi4plants/ArcGraphModel/commit/bd06096970062d009b403d3d10705314f0d63ba9)] Add structural ontology parsing logic
    * [[#f062e66](https://github.com/nfdi4plants/ArcGraphModel/commit/f062e664c329b7b678da907f6610f9453307389c)] wip structural tests
    * [[#59f52e4](https://github.com/nfdi4plants/ArcGraphModel/commit/59f52e4a2e8a5d06a1575c3c98226c1dd5b9f64a)] working empty investigation metadata test
    * [[#cd4f08b](https://github.com/nfdi4plants/ArcGraphModel/commit/cd4f08bb982906685246e6110e09753edec1c877)] wip more restructuring
    * [[#ae6c20a](https://github.com/nfdi4plants/ArcGraphModel/commit/ae6c20a44d7758693ef051296a5a36e91d8208dc)] Add location attributes to all metadata section cv params, annotate keys with special term
    * [[#9a2c5d8](https://github.com/nfdi4plants/ArcGraphModel/commit/9a2c5d86738c4a13eef82c13c446d9d704bc9d60)] finish simple investigation metadata test
    * [[#fc515d1](https://github.com/nfdi4plants/ArcGraphModel/commit/fc515d175051155b2b2127c006ade333bacd2675)] pin obo parser dependency
    * [[#78973ee](https://github.com/nfdi4plants/ArcGraphModel/commit/78973eefacd7984be8e8eb976f9c58e35963d670)] rename library to ARCTokenization, update package metadata
    * [[#c621cdc](https://github.com/nfdi4plants/ArcGraphModel/commit/c621cdc2969843252c368f2fe70f2cd9a02a8ebe)] add requested changes from review
    * [[#c841135](https://github.com/nfdi4plants/ArcGraphModel/commit/c841135875974f64bc677500022731dc5e0b996a)] Merge pull request #27 from nfdi4plants/structural-ontology
* Deletions:
    * [[#69a1f28](https://github.com/nfdi4plants/ArcGraphModel/commit/69a1f2828c1c07d78848477173b1bf76ade12bc6)] Delete some unit tests and add some integration tests
    * [[#3f35750](https://github.com/nfdi4plants/ArcGraphModel/commit/3f35750713143d9517e40f362adb7df9462e452e)] Cleanup: - remove js stuff - use Xunit for tests - various dependency adjustments
    * [[#76fc7b4](https://github.com/nfdi4plants/ArcGraphModel/commit/76fc7b47ee1660a0b98193bcdcab1aef202ac184)] remove old term parser match cases
* Bugfixes:
    * [[#e8cde7b](https://github.com/nfdi4plants/ArcGraphModel/commit/e8cde7b42a292cf48c49794b8185f86e03b31f0a)] Fix interchanged names
    * [[#32ecc07](https://github.com/nfdi4plants/ArcGraphModel/commit/32ecc0751b719af30b1f8176ccb1f6031525147a)] Delete print-debugging
    * [[#14e049f](https://github.com/nfdi4plants/ArcGraphModel/commit/14e049f365919a0e53a71cc3c6b961c4a4fd61ee)] Merge pull request #15 from nfdi4plants/bug-namesInterchanged-#14
    * [[#9c450d9](https://github.com/nfdi4plants/ArcGraphModel/commit/9c450d9a79ca1a1ccfadf43c5754db0f2e552793)] Fix display bug when printing
    * [[#3ba183b](https://github.com/nfdi4plants/ArcGraphModel/commit/3ba183b83b71440bf9aaa7a5c98e0ece86ea8c1a)] Play around in playground, update fixture Investigation file
    * [[#d2a28b7](https://github.com/nfdi4plants/ArcGraphModel/commit/d2a28b7b3ebd04bfa65413b2fd0e411a51a68176)] Fix bug resulting from dependency upgrade
    * [[#8006f21](https://github.com/nfdi4plants/ArcGraphModel/commit/8006f214444ef203b9f6f870a51a2cb96fd2592a)] fix test paths

### 0.1.0+b3b6f0a (Released 2023-4-27)
* Additions:
    * [[#ac5ab8c](https://github.com/nfdi4plants/ArcGraphModel/commit/ac5ab8c1836bb0c6fa718cf5ada67cf9bcf60d80)] add attributes to tokenized containers
    * [[#bb20906](https://github.com/nfdi4plants/ArcGraphModel/commit/bb20906c735c1be21b25a0c2b5ac3d942f5c3b23)] add helper functions to CvAttribute Collection
    * [[#b3b6f0a](https://github.com/nfdi4plants/ArcGraphModel/commit/b3b6f0a4c393a17bf373458d1b81ead53ab44480)] add some IO tests
    * [[#3dc5d11](https://github.com/nfdi4plants/ArcGraphModel/commit/3dc5d11017f7d01c5c83ae57026dc509bb11fc87)] update test script
* Bugfixes:
    * [[#6357040](https://github.com/nfdi4plants/ArcGraphModel/commit/635704005f5a2f788b67b3c981ee7d51b2ce1205)] fix functions for js usage
    * [[#35c9d64](https://github.com/nfdi4plants/ArcGraphModel/commit/35c9d64757b10d783ccad856467fc2bbf448171c)] fix duplicate test naming

### 0.0.0 (Released 2023-3-15)
* Additions:
    * Initial set up for RELEASE_Notes.md

