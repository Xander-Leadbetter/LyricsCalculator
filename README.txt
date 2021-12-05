
# Dependencies
- .NET 5.0 or greater (SDK Download available here https://dotnet.microsoft.com/download/dotnet/5.0)
- Microsoft.AspNet.WebApi.Client (5.2.7)
- System.Net.Http (4.3.4)

NOTE: The package dependencies should be included upon .NET 5.0 SDK installation on the host system as they are listed within the project file ItemGroup for inclusion.

However, they can be individually installed via the Packages manager or by the cmdlet if opened via another means/IDE and compiling causes an issue.



# Building/Running the Program

Visual Studio (2019+)
- Open the solution LyricsCalculator.sln
- Build the solution with 'Release' parameter (Can be built as Debug too if preferred, however destination folder will change to Debug and not Release)

Visual Studio Code
- Ensure the .vscode folder and two supporting files (launch.json & tasks.json) are included in the top directory - these are typically created automatically when build is attempted but I've included them here.
- Select "Open File..." from the File drop down, select the top directory of where the files and folders were downloaded to.
- Follow prompts for extension installation on Visual Studio Code (C# Extension is needed)
- Either press Ctrl+Shift+B and select "build" from the middle drop down or Terminal > "Run Build Task..."

- Run the generated output LyricsCalculator.exe within the ~bin\Release\net5.0

# Troubleshooting
- If the API's are down or under heavy request, the process should return a response indicating the issue - if so, try again at a later time.

# How To Use
- Type in an artists name and hit return, the output should return the results automatically.

# Known Issues
- The lyrics API sometimes returns incorrect/different lyrics for songs, be this through a dropout or incorrect match to the title, this can yield results that vary at times - generally seen when looking up an artist with a large number of albums/tracks. A way to mitigate this is to increase delay, but at a compromise to usability.
- The lyrics API sometimes switches between stating \n for new line to \r - sometimes it includes the title, sometimes it doesn't - See issue1.jpg and issue2.jpg for an example of this. I can't see any work around for this other than the "work around" I've put in to the lyrics parsing, as such I can't guarantee accurate results.