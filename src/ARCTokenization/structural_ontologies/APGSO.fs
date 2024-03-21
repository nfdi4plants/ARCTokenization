namespace ARCTokenization.StructuralOntology

    open ControlledVocabulary

    module APGSO =

        let Process_Graph_Header = CvTerm.create("APGSO:00000001", "Process Graph Header", "APGSO")

        let Characteristic = CvTerm.create("APGSO:00000002", "Characteristic", "APGSO")

        let Factor = CvTerm.create("APGSO:00000003", "Factor", "APGSO")

        let Parameter = CvTerm.create("APGSO:00000004", "Parameter", "APGSO")

        let Component = CvTerm.create("APGSO:00000005", "Component", "APGSO")

        let ProtocolType = CvTerm.create("APGSO:00000006", "ProtocolType", "APGSO")

        let ProtocolDescription = CvTerm.create("APGSO:00000007", "ProtocolDescription", "APGSO")

        let ProtocolUri = CvTerm.create("APGSO:00000008", "ProtocolUri", "APGSO")

        let ProtocolVersion = CvTerm.create("APGSO:00000009", "ProtocolVersion", "APGSO")

        let ProtocolREF = CvTerm.create("APGSO:00000010", "ProtocolREF", "APGSO")

        let Performer = CvTerm.create("APGSO:00000011", "Performer", "APGSO")

        let Date = CvTerm.create("APGSO:00000012", "Date", "APGSO")

        let Input = CvTerm.create("APGSO:00000013", "Input", "APGSO")

        let Output = CvTerm.create("APGSO:00000014", "Output", "APGSO")

        let IOType = CvTerm.create("APGSO:00000015", "IOType", "APGSO")

        let Source = CvTerm.create("APGSO:00000016", "Source", "APGSO")

        let Sample = CvTerm.create("APGSO:00000017", "Sample", "APGSO")

        let RawDataFile = CvTerm.create("APGSO:00000018", "RawDataFile", "APGSO")

        let DerivedDataFile = CvTerm.create("APGSO:00000019", "DerivedDataFile", "APGSO")

        let ImageFile = CvTerm.create("APGSO:00000020", "ImageFile", "APGSO")

        let Material = CvTerm.create("APGSO:00000021", "Material", "APGSO")

        let FreeText = CvTerm.create("APGSO:00000022", "FreeText", "APGSO")

