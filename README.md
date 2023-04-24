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

## develop

### prerequisites

- .NET 6 SDK
- nodejs (tested with ~v16)

### setup

- dotnet tool restore
- npm install

### build

#### Linux/macos

- make `build.sh` executable
- run `build.sh`

#### Windows

run `build.cmd`

#### or run the build project directly:

`dotnet run --project ./build/build.fsproj`

### test

#### Linux/macos

- run `build.sh runTests`

#### Windows

- run `build.cmd runTests`

#### or run the build project directly:

`dotnet run --project ./build/build.fsproj runTests`
