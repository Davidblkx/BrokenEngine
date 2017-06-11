function removeCoreDll ($folder, $file) {

    $files = Get-ChildItem $folder -Filter "BrokenEngine.*.dll" -Recurse
    for($i=0; $i -lt $files.Count; $i++){
        "Checking " + $files[$i].FullName
        if ($files[$i].Name -ne $file) {
            Remove-Item -Force $files[$i].FullName
        }

    }

}

function createPlugin ($path, $dest, $name) {
    $fullPath = "build/Plugins/" + $dest
    mkdir $fullPath
    $relative = "../" + $fullPath
    dotnet publish $path -o $relative
    removeCoreDll $fullPath $name
}

function emptyBuildFolder () {
    Remove-Item -Recurse -Force build
    mkdir .\build
    mkdir .\build\Plugins    
}

function runApp () {
    #Run app
    Set-Location .\build
    dotnet BrokenEngine.dll
    Set-Location ..
}

#Change location to project root
function goToRoot () {
    Set-Location "D:\Projects\BrokenEngine\"
}

function init ($runtime) {
    goToRoot

    #Remove all files from 'build' folder
    emptyBuildFolder

    #Publish main project
    dotnet restore -r $runtime
    dotnet build -r $runtime
    dotnet publish BrokenEngine/BrokenEngine.csproj -o "../build" -r $runtime

    #Plugin ISettings
    createPlugin "BrokenEngine.Settings/BrokenEngine.Settings.csproj" "DeafaultSettingsPlugin" "BrokenEngine.Settings.dll"

    #Plugin IConsole
    createPlugin "BrokenEngine.Command/BrokenEngine.Command.csproj" "DeafaultCommandPlugin" "BrokenEngine.Command.dll"

    #Plugin IStorage
    createPlugin "BrokenEngine.Storage/BrokenEngine.Storage.csproj" "DeafaultStoragePlugin" "BrokenEngine.Storage.dll"

    #Plugin ILogging
    createPlugin "BrokenEngine.Loggin/BrokenEngine.Loggin.csproj" "DeafaultLogginPlugin" "BrokenEngine.Loggin.dll"

    #Plugin BassAudioPlayer
    createPlugin "BrokenEngine.BassAudioPlayer/BrokenEngine.BassAudioPlayer.csproj" "BassAudioPlayer" "BrokenEngine.BassAudioPlayer.dll"
    Copy-Item -Path "BrokenEngine.BassAudioPlayer/DEPENDENCIES/*" -Destination "build" -Recurse

    #Clear-Host

    #uncomment to run app after build
    #runApp

    "PROCESS COMPLETED"
}

init "win10-x64"