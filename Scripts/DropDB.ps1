
$DB = $PSScriptRoot + "/../Database/";
$API = $PSScriptRoot + "/../API/";

$Confirm = read-host -prompt "Are you sure you want to drop the Ultiminer Database (y/n)?";
if ($Confirm -eq "y") {

    write-host "Dropping existing database...";
    write-host "using Database Project:" $DB;
    write-host "using API Project:" $API;

    dotnet ef database drop --project $DB --startup-project $API
    return;
}

write-host "Confirmation not successful, exiting";