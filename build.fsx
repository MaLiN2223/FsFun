// include Fake libs
#r "./packages/FAKE/tools/FakeLib.dll"

open Fake.Testing;
open Fake

// Directories
let buildDir  = "./build/"
let deployDir = "./deploy/"
let sourceDir = "./src/"
let testsDir = "./FSharpFunTests/FSharpFunTests/bin/Debug"

// Filesets
let appReferences  =
    !! "/**/*.csproj"
    ++ "/**/*.fsproj"

// version info
let version = "0.1"  // or retrieve from CI server

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; deployDir]
)

Target "Build" (fun _ ->
    // compile all projects below src/app/
    MSBuildDebug buildDir "Build" appReferences
    |> Log "AppBuild-Output: "
)

Target "Deploy" (fun _ ->
    !! (buildDir + "/**/*.*")
    -- "*.zip"
    |> Zip buildDir (deployDir + "ApplicationName." + version + ".zip")
)

Target "BuildTests" (fun _ ->
trace "Building Tests..."
!! "**/*Tests.csproj"
  |> MSBuildDebug testsDir "Build"
  |> Log "TestBuild-Output: "
)

let runTests () =
    tracefn "Running tests..."
    !! (testsDir @@ "*.Tests.dll")
    |> xUnit2 (fun p -> {
                        p with HtmlOutputPath = Some(testsDir @@ "xunit.html");
                               ToolPath = @"../packages/xunit.runner.console.2.0.0/tools/xunit.console.exe"
                        })


Target "Tests" (fun _ ->
    runTests()
)




// Build order
"Clean"
  ==> "Build"
  ==> "BuildTests"
  ==> "Tests"
  ==> "Deploy"

// start build
RunTargetOrDefault "Deploy"
