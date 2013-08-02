﻿module CsvtringUnitTests 

open System
open NUnit.Framework
open FsUnit

open Combinator
open StringCombinator
open StringMatchers.CsvSample


[<Test>]
let testEmptyWhiteSpace() = 
    let csv = new StringStreamP("")

    let result = test csv ws

    result |> should equal ""

[<Test>]
let testWhiteSpace() = 
    let csv = new StringStreamP(" ")

    let result = test csv ws

    result |> should equal " "

[<Test>]
let testElement() = 
    let csv = new StringStreamP("some text")

    let result = test csv csvElement

    result |> should equal "some text"
    
[<Test>]
let testElements() = 
    let csv = new StringStreamP("some text,")

    let result = test csv element

    result |> should equal "some text"

[<Test>]
let testElement2() = 
    let csv = new StringStreamP("some text")

    let result = test csv element

    result |> should equal "some text"

[<Test>]
let testTwoElement() = 
    let csv = new StringStreamP("some text, text two")

    let result = test csv elements

    result |> should equal ["some text";"text two"]

[<Test>]
let testTwoLines() = 
    let t = @"a, b
c, d"

    let csv = new StringStreamP(t)

    let result = test csv lines

    result |> should equal [["a";"b"];["c";"d"]]

[<Test>]
let testEscaped() = 
    let t = @"\,"

    let csv = new StringStreamP(t)

    let result = test csv escapedChar

    result |> should equal ","

[<Test>]
let testLiteral() = 
    let t = "\"foo\""

    let csv = new StringStreamP(t)

    let result = test csv literal

    result |> should equal "foo"

[<Test>]
let testLiteral2() = 
    let t = "a\,b"

    let csv = new StringStreamP(t)

    let result = test csv normalAndEscaped

    result |> should equal "a,b"

[<Test>]
let testUnEscaped1() = 
    let t = "a,b"

    let csv = new StringStreamP(t)

    let result = test csv normalAndEscaped

    result |> should equal "a"

[<Test>]
let testCsvWithQuotes1() = 
    let t = "\"cd,fg\""

    let csv = new StringStreamP(t)

    let result = test csv lines

    result |> should equal [["cd,fg"]]

[<Test>]
let testCsvWithQuotes2() = 
    let t = "a,\"b 1.\\\",\"cd,fg\"
a,b,\\\", \"cd,fg\""

    let csv = new StringStreamP(t)

    let result = test csv lines |> List.toArray

    result.[0] |> should equal ["a";"b 1.\\";"cd,fg"]
    result.[1] |> should equal ["a";"b";"\"";"cd,fg"]


[<Test>]
let testAll() = 
    let t = @"This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words""
This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words""
This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words""
This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words""
This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words""
This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words""
This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words""
This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words""
This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words""
This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words""
This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"", This is some text! whoo ha, ""words"""

    let csv = new StringStreamP(t)

    let result = test csv lines

    List.length result |> should equal 11
