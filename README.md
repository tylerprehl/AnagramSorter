# Anagram Sorter
The program created (AnagramSorter) is a .NET C# console app, designed to be run on Windows Command Line or PowerShell.

### Build and Run the AnagramSorter
You can build and run the Anagram Sorter by doing the following:
- ensure that dotnet is installed (version 8.0.202 was used during development/testing)
- navigate to the project in Command Prompt or PowerShell
- place input.txt in a directory labeled "InputOutput" within the project directory
- run ```dotnet build```
- run ```dotnet run "InputOutput\input.txt"```
- view the output.txt file created in the InputOutput directory

### input.txt Testing Samples
The following is a list of input lines used to test the project (and what each specifically tests)
- [empty file]
	- an empty input.txt file tests how the program acts when there is no input at all
	- expected: There is no content in the input file to analyze
- ""
	- test how the program handles an empty line (in a file with at least one other valid line)
	- expected: []
- "thing"
	- test how the program handles a single phrase input
	- expected: [thing]
- "some thing"
	- test how the program handles a phrase with a space mid-phrase
	- expected: [some thing]
- "   some thing   "
	- test how the program handles spaces before/after phrases (with space mid-phrase)
	- expected: [some thing]
- "to,two,eat,too,ate,tea,tow"
	- test how the program handles sorting the anagram groups (by phrase count)
	- expected: [to],[too],[two,tow],[eat,ate,tea]
- "ele gant man,a gentleman"
	- test how the program handles difference in number of spaces mid-phrase
	- expected: [ele gant man,a gentleman]
- "abc, cba, abc efg, gef cab"
	- test how the program handles specifically non-English text
	- expected: [abc,cba],[abc efg,gef cab]
- "some1"
	- test how the program handles invalid input (digit)
	- expected: Inavlid characters were found in the input file. Please ensure only a-z (lowercase), spaces, commas, and carriage returns are used
- "thing!"
	- test how the program handles invalid input (exclamation point - special character)
	- expected: Inavlid characters were found in the input file. Please ensure only a-z (lowercase), spaces, commas, and carriage returns are used