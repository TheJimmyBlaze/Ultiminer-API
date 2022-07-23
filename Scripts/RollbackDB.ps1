
$DB = $PSScriptRoot + "/../Database/";
$API = $PSScriptRoot + "/../API/";

write-host "Rolling back previous migration...";
write-host "using Database Project:" $DB;
write-host "using API Project:" $API;

dotnet ef migrations remove --project $DB --startup-project $API