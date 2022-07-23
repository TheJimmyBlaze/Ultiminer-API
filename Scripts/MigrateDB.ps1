
if ($args.count -eq 0){
    write-host "A migration name must be provided e.g., './MigrateDB.ps1 initial'";
    return;
}

$DB = $PSScriptRoot + "/../Database/";
$API = $PSScriptRoot + "/../API/";

write-host "using Database Project: " + $DB;
write-host "using API Project: " + $API;

dotnet ef migrations add $args[0] --project $DB --startup-project $API