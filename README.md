# ArcGraphModel

## Library structure
### CvTokens
```mermaid
classDiagram
    ICvBase <|-- IParam : Inherits
    IParamBase <|-- IParam : Inherits
    ICvBase <|.. CvObject : Implements
    ICvBase <|.. CvContainer : Implements
    IParam <|.. UserParam : Implements
    IParam <|.. CvParam : Implements
    <<Interface>> ICvBase
    <<Interface>> IParamBase
    <<Interface>> IParam
    class ICvBase{
        + CvTerm     
    }
    class IParamBase{
        + CvValue
        + WithValue()
    }
    class IParam{
        + CvTerm 
        + CvValue
        + WithValue()
    }
    class CvObject{
        + Attributes
        + CvTerm
        + Generic Value
    }
    class CvContainer{
        + Attributes
        + CvTerm
        + Children        
        + GetSingle()       
        + SetSingle()
        + GetMany()
        + SetMany()

    }
    class CvParam{
        + Attributes
        + CvTerm
        + CvValue
        + WithValue()
    }
    class UserParam{
        + Attributes
        + Term
        + CvValue
        + WithValue()
    }
```

## Develop

### Prerequisites

- .NET 6 SDK
- nodejs (tested with ~v16)

### Setup

- dotnet tool restore
- npm install

### Build whole project

#### Linux/macOS

- make `build.sh` executable
- run `build.sh`

#### Windows

run `build.cmd`

#### or run the build project directly:

`dotnet run --project ./build/build.fsproj`

### Build ontologies (YAML to OBO)

#### Linux/macOS

- make `build.sh` executable
- run `build.sh buildOntologies`

#### Windows

run `build.cmd buildOntologies`

#### or run the build project directly:

`dotnet run --project ./build/build.fsproj buildOntologies`

### Test

#### Linux/macOS

- run `build.sh runTests`

#### Windows

- run `build.cmd runTests`

#### or run the build project directly:

`dotnet run --project ./build/build.fsproj runTests`
