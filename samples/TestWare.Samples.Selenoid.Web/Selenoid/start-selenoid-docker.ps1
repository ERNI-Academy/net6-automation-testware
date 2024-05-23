Write-Host "Stopping selenoid and selenoid-ui containers" -ForegroundColor DarkGreen
docker-compose down

Write-Host "Downloading browser images...." -ForegroundColor DarkGreen
$browsers = Get-Content .\browsers.json | ConvertFrom-Json
foreach($browser in $browsers.PsObject.Properties.Value) {
	foreach($version in $browser.versions.PsObject.Properties.Value) {
		docker pull $version.image
	}
}

Write-Host "Starting selenoid and selenoid-ui containers" -ForegroundColor DarkGreen
docker-compose up -d