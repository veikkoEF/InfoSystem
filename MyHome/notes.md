
# Funktionen
- Wetter: Auswahl des Ortes, 3 Tages Vorhersage, Tages-Ansicht
- News: allgemeiner Nachrichtenfeed
- Message: vom Smartphone direkt an die App
- Bilder: vom Smartphone direkt an die App (via Cloud)
- Geoposition: Anzeige in Karte
- Nachrichten aus der Region
- Abfahrten Bus/ Bahn
- TV-Programm
- ToDo Liste
- Kalender
- Steuerung über Smartphone
- Uhrzeit/ Datum
- Interaktion (InkCanvas)

# Controls
## Wetter
- API: https://openweathermap.org/api
- URI: http://api.openweathermap.org/data/2.5/weather?q=Erfurt&appid=7506e28af4bfa0e5e79d9a402320302a&units=metric
- Client, C#-Lib, https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netcore-1.1
- Examples:  
	- https://github.com/swiftyspiffy/OpenWeatherMap-API-CSharp
	- https://gist.github.com/shuhey/fc1903079ec74d132736f2c5b71cb6dc
	- https://channel9.msdn.com/Series/Windows-10-development-for-absolute-beginners/UWP-058-UWP-Weather-Setup-and-Working-with-the-Weather-API
	- https://stackoverflow.com/questions/47589249/how-to-get-5day-weather-forecast-from-openweathermap-to-my-uwp-app
- Assets: https://www.wallpaper-gratis.eu/natur/wolke/wolken.php
- Konvertierung Sunset/ Sunrise: 
  DateTime sunrise = DateTimeOffset.FromUnixTimeSeconds(long.Parse(myWeather.city.sunrise)).DateTime.ToLocalTime();

## News
 - https://newsapi.org/register/success

## Messaging
 - Übermittlung von kurzen Nachrichten direkt vom Smartphone
 - 


## Bilder
- Dropbox: 
    - https://www.dropbox.com/developers/documentation/dotnet
    - https://github.com/dropbox/dropbox-sdk-dotnet/blob/b8f5c751f9df6765f4e2bbb98b39bc6bde5cfadc/dropbox-sdk-dotnet/Examples/SimpleTest/Program.cs#L320
- FlipView: https://stackoverflow.com/questions/26325297/how-to-make-auto-flip-view-xaml
- Animation: https://help.syncfusion.com/uwp/carousel/animating-carousel-items

# Code

## Navigation
 - https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.navigationviewitem
 - https://docs.microsoft.com/de-de/windows/communitytoolkit/getting-started

## Load Images from File
 - https://docs.microsoft.com/en-us/uwp/api/windows.storage.storagefile.getfilefrompathasync
 - https://codedocu.de/Details?d=1145&a=9&f=181&l=0

## Image Resize
  - https://stackoverflow.com/questions/59235739/how-to-compress-the-image-size-using-c-sharp-dot-net-core
  - https://www.grapecity.com/blogs/comparing-dot-net-imaging-apis
  - https://github.com/SixLabors/ImageSharp

## Azure
 - https://medium.com/@shaha/microsoft-azure-storage-with-universal-windows-platform-6524d0189651
 - https://docs.microsoft.com/de-de/dotnet/api/overview/azure/storage?view=azure-dotnet
 - https://docs.microsoft.com/de-de/azure/cosmos-db/tutorial-develop-table-dotnet

## Web-Server (Background)
- https://social.msdn.microsoft.com/Forums/en-US/d34e58ba-0495-4846-ac07-7b59ee1edb19/build-and-deploy-web-services-on-windows-10-iot-for-raspberry-pi-3?forum=WindowsIoT
- https://sandervandevelde.wordpress.com/2016/04/08/building-a-windows-10-iot-core-background-webserver/
- https://www.dotnetpro.de/workout/hardware/mach-schlau-haus-1480303.html
- https://github.com/tomkuijsten/restup/wiki/One-pager
- https://marketplace.visualstudio.com/items?itemName=MicrosoftIoT.WindowsIoTCoreProjectTemplatesforVS15

## UWP-Examples
 - https://comentsys.wordpress.com/

# Raspberry
- https://docs.microsoft.com/en-us/windows/iot-core/tutorials/quickstarter/PrototypeBoard

# Inspiration
- https://dakboard.com/site
- https://github.com/XamlBrewer/UWP-Composition-API-Clock
- https://stackoverflow.com/questions/38562704/make-clock-uwp-c

# Tools
- https://dummyimage.com/

# Installation
- Pfad (lokal): App-Path: C:\Users\...\AppData\Local\Packages\...\LocalState
- 

# Code-Beispiele
- Shutdown: https://stackoverflow.com/questions/57366396/how-to-shutdown-restart-my-uwp-application

# Komponente Bildershow

## Ziel

Das Ziel dieser Komponente ist das Anzeigen von Bildern auf dem Bildschirm in Form einer Diashow. Als Vorbild dient ein digitaler Bilderrahmen. Dabei sollen mehrere Möglichkeiten der Darstellung existieren, z.B.:

- jeweils ein Bild in Vollbilddarstellung und nacheinander durchlaufend
- mehrere Bilder in Form einer Collage angeordnet

## Bildquellen

Die Bilder stammen aus bekannten Online-Speicherdiensten in der Cloud, zum Beispiel:

- Dropbox
- OneDrive
- GoogleDrive

Die Bilder werden lokal auf das Device heruntergeladen, damit die Anzeige der Bilder im laufenden Betrieb schneller erfolgen kann. Ebenso ist eine Funktion der Komponente nach einem erstmaligen Download der Bilder auch möglich, wenn keine aktive Internetverbindung besteht. Die Komponente soll in der Lage sein, eine große Anzahl von Bildern zu verwalten und aus der Menge der Bilder eine Auswahl anzuzeigen.

## Bildanzeige

Es stehe die folgenden Optionen für eine Bildanzeige zur Verfügung:

- Diashow: Die Bilder werden im Vollbildmodus nacheinander automatisch eingeblendet. Auf Touch-Bildschirmen kann zusätzlich via Touch zwischen den Bildern gewechselt werden. Das Zeitintervall für den Bildwechsel kann eingestellt werden.

- Collage: Mehrere Bilder werden auf der Oberfläche einblendet. Nach einer bestimmten Zeit wird der Bildschirm gelöscht und eine neue Collage wird gestartet. Es können die Anzahl der Bilder pro Collage und die Geschwindigkeit des Neueinblendendes eines des nächsten Bildes eingestellt werden.

## Upload der Bilder in den Online-Speicher
- Die Bilder können direkt in den Online-Speicher hochgeldaden werden
- über die App MyHome 