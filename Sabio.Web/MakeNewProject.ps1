# Remove-Item c:\scripts\* -include .wav,.mp3


# copy-item c:\src\* c:\dst -force -recurse -verbose

Param
(
    [Parameter(Mandatory=$true)]
    [string] $ProjectName
)
$originalPath = $MyInvocation.MyCommand.Definition

$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition
$destPath = '../../' + $ProjectName
$slnName =  $ProjectName + ".sln"
$deletePath = '.\packages\*'


Write-Output  $scriptPath 
Write-Output  $destPath

Copy-Item -Path $scriptPath -Destination $destPath -Exclude *.dll, *.ps1 -Force -Recurse

CD $destPath
Remove-Item $deletePath -exclude *.config -Recurse -Force
Remove-Item .\.vs -Recurse -Force


#Get-Childitem  *.* -File -Recurse -Filter *.suo  | Foreach-Object {Write-Output  $_.FullName}# Foreach-Object {Remove-Item $_.FullName}

#Get-ChildItem *.suo* -recurse | Where { ! $_.PSIsContainer } -Verbose

#Remove-Item  -Recurse -Force -Verbose -Filter *.suo -Path .\*
#Remove-Item  -Recurse -Force -Verbose .\*.vsp*

#Remove-Item  -Recurse -Force -Verbose .\*.vss*

Rename-Item -NewName $slnName -Path Sabio.Starter.Template.sln -Force

Remove-Item .\*.ps1 -Force

CD $scriptPath

Write-Warning "You still have to delete all the *.vsp and *.vss files, the .vs folder, this *.suo files as well as removing the solution from Source countrol bindings."







