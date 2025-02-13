// This file should eventually be auto-generated from the respective obo files, to have a safe way of updating it from the same source.
// For now, it is manually created and updated. It is not complete, just a collectAFSOn of terms needed for baseline WIP validatAFSOn

namespace ARCTokenization.StructuralOntology


open ControlledVocabulary
open System


module APGSO =

    let FreeText = CvTerm.create(accession = "APGSO:00000022", name = "FreeText", ref = "APGSO")

    module ``Process Graph Header`` =
        
        let key = CvTerm.create(accession = "APGSO:00000001", name = "Process Graph Header", ref = "APGSO")

        let Characteristic      = CvTerm.create(accession = "APGSO:00000002", name = "Characteristic", ref = "APGSO")
        let Factor              = CvTerm.create(accession = "APGSO:00000003", name = "Factor", ref = "APGSO")
        let Parameter           = CvTerm.create(accession = "APGSO:00000004", name = "Parameter", ref = "APGSO")
        let Component           = CvTerm.create(accession = "APGSO:00000005", name = "Component", ref = "APGSO")
        let ProtocolType        = CvTerm.create(accession = "APGSO:00000006", name = "ProtocolType", ref = "APGSO")
        let ProtocolDescription = CvTerm.create(accession = "APGSO:00000007", name = "ProtocolDescription", ref = "APGSO")
        let ProtocolUri         = CvTerm.create(accession = "APGSO:00000008", name = "ProtocolUri", ref = "APGSO")
        let ProtocolVersion     = CvTerm.create(accession = "APGSO:00000009", name = "ProtocolVersion", ref = "APGSO")
        let ProtocolREF         = CvTerm.create(accession = "APGSO:00000010", name = "ProtocolREF", ref = "APGSO")
        let Performer           = CvTerm.create(accession = "APGSO:00000011", name = "Performer", ref = "APGSO")
        let Date                = CvTerm.create(accession = "APGSO:00000012", name = "Date", ref = "APGSO")
        let Input               = CvTerm.create(accession = "APGSO:00000013", name = "Input", ref = "APGSO")
        let Output              = CvTerm.create(accession = "APGSO:00000014", name = "Output", ref = "APGSO")


    module IOType = 

        let key = CvTerm.create(accession = "APGSO:00000016", name = "IOType", ref = "APGSO")
        
        let Source          = CvTerm.create(accession = "APGSO:00000016", name = "Source", ref = "APGSO")
        let Sample          = CvTerm.create(accession = "APGSO:00000017", name = "Sample", ref = "APGSO")
        let Material        = CvTerm.create(accession = "APGSO:00000021", name = "Material", ref = "APGSO")
        let Data            = CvTerm.create(accession = "APGSI:00000023", name = "Data", ref = "APGSO")
        [<Obsolete>]
        let RawDataFile     = CvTerm.create(accession = "APGSO:00000018", name = "RawDataFile", ref = "APGSO")
        [<Obsolete>]
        let DerivedDataFile = CvTerm.create(accession = "APGSO:00000019", name = "DerivedDataFile", ref = "APGSO")
        [<Obsolete>]
        let ImageFile       = CvTerm.create(accession = "APGSO:00000020", name = "ImageFile", ref = "APGSO")