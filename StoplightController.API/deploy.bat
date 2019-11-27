set /p password= "SSH Password:"
dotnet-sshdeploy push -f netcoreapp2.2 -t "/home/pi/stoplight" -h 192.168.1.107 -u pi -w %password% -c Release