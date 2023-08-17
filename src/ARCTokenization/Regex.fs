/// <summary>
/// This module contains unified regex patterns and matching functions to parse isa tab column headers to BuildingBlock information.
/// </summary>
namespace ARCTokenization

open System

module Regex =

    module Pattern =

        let [<Literal>] CommentPattern = @"(?s)Comment\[.*\]"

    module ActivePatterns =
    
        open System.Text.RegularExpressions
    
        /// Matches, if the input string matches the given regex pattern.
        let (|Regex|_|) pattern (input : string) =
            let m = Regex.Match(input.Trim(), pattern)
            if m.Success then Some(m)
            else None

        /// Matches any Comment[...] pattern.
        let (|Comment|_|) input = 
            match input with
            | Regex Pattern.CommentPattern r -> Some r.Value
            | _ -> None
