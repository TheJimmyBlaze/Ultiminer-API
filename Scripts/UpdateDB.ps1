
$DB = $PSScriptRoot + "/../Database/";
$API = $PSScriptRoot + "/../API/";

write-host "Applying migrations to database...";
write-host "using Database Project:" $DB;
write-host "using API Project:" $API;

dotnet ef database update --project $DB --startup-project $API