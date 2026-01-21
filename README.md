# This Is A Kata Of A String Calculator exercise provided By Daniele Megna, A Developer Of Qmates Company

## How to run the application
* To test the project you can run task **"test"** or manually run **"dotnet test ${workspaceFolder}/Tests/Tests.csproj"**
* Github project just contains .exe program and Add-to-PATH.bat, if you want to test it directly double click no Add-to-PATH.bat and give permission to add scalc to user's variables' path, then open prompt and type "scalc 1,2,3" or with some other string numbers.
* If you want to republish you can run task **"publish"** and **add scalc** to path manually, if you also want automatically add "scalc" command to cmd run **"publish-and-install-scalc"**. Then reopen prompt and type "scalc 2,2,3" for example.
* If you want to manually publish app you can, with my best wishes, run **"dotnet publish ${workspaceFolder}/Application/Application.csproj -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true /p:EnableCompressionInSingleFile=true /p:DebugType=none /p:DebugSymbols=false -o ${workspaceFolder}/publish"** and then manually add scalc to path
* At the end you can run **"remove-scalc-from-path"** to remove scalc from path variables

## How application was implemented:
## Part 1:
### Step 1: the simplest thing
**Create a simple String calculator with a method int add(String numbers).**

The string argument can contain 0, 1 or 2 numbers, and will return their sum (for an empty string it will return 0) for example "" or "1" or "1,2".
Start with the simplest test case of an empty string and move to 1 and two numbers.
Remember to solve things as simply as possible so that you force yourself to write tests you did not think about.
Remember to refactor after each passing test.

So the points are:
* method int add(String numbers)
* test passing empty string
* test passing "1"
* test passing "1,2"
* Refactor


### Step 2: handle an unknown amount of numbers
Allow the add() method to handle an unknown amount of numbers.

So the points are:
* I just consent Add() method to manage an unknown amount of numbers so I only have to add a case to previous test
* Refactor


### Step 3: handle new lines between numbers
Allow the add() method to handle new lines between numbers (instead of commas).

the following input is ok: "1\n2,3" (will equal 6)
the following input is NOT ok: "1,\n" (not need to prove it - just clarifying)

So the points are:
* Create the simplest test with '\n' instead of ',' and watch it fails
* Modify the split function of Add() method to accept even '\n'
* Retry test to verify it's all ok
* Add a test a little bit complex with '\n' and ','
* Refactor


### Step 4: support different delimiters
Support different delimiters: to change a delimiter, the beginning of the string will contain a separate line that looks like this:

"//[delimiter]\n[numbers...]"

For example "//;\n1;2" should return 3 where the default delimiter is ';'.

The first line is optional. All existing scenarios should still be supported.

So the points are:
* Create a test to test if delimiter is valid and watch it fails
* Create a function to get delimiter and to validate the test
* Create a test to check separator '\n' between delimiter and string numbers and watch it fails
* Create a function to test separator '\n'
* Refactor
* Create a test that pass new delimiter to Add() method and watch build fails
* implement Add() method to accept a delimiter and set [';', '\n'] as default as previous tests require
* Refactor
* retest all
* Refactor

### Step 5: negative numbers
Calling add() with a negative number will throw an exception "negatives not allowed" - and the negative that was passed.
For example add("1,4,-1") should throw an exception with the message "negatives not allowed: -1".
If there are multiple negatives, show all of them in the exception message.

So the points are:
* Create a test that pass negative numbers to Add() function and watch it fails
* readjust Add() function to accept negative numbers throwing exception to fix the test
* refactor

### Step 6: ignore big numbers
Numbers bigger than 1000 should be ignored, so adding 2 + 1001 = 2

So the points are:
* Create a test that pass big numbers to Add() function and watch it fails summing them
* Modify Add() method to ignore big numbers
* Refactor

## Part 2 - Go further
### Step 1: Add logging
Add Logging Abilities to your new String Calculator (to an ILogger.Write()) interface (you will need a mock). Every time you call Add(), the sum result will be logged to the logger.
When calling Add() and the logger throws an exception, the string calculator should notify an IWebservice of some kind that logging has failed with the message from the logger's exception (you will need a mock and a stub).

So the points are:
* Add ILogger to Add method and watch all tests calling it fail
* refactor
* Write a test that creates a Mock logger to pass to AddService just invocated
* Refactor
* Create a function to invocate AddService that create a Mock Logger to centralized it for all tests
* Refactor
* Create an IWebSerice interface and call it into AddService when logger throw an exception
* Create a test to invocate AddService with an IWebService mock and a stub logger, logger musto throw exception and at the end of test function webservice mock must test it is been called 1 time
* Refactor

## Step 2: A More Difficult Variation of the Kata
Create a program (test first) that uses string calculator, which the user can invoke through the terminal / console by calling scalc '1,2,3' and will output the following line before exiting: "The result is 6". Note that scalc is the name of the app executable.

Then, instead of exiting after the first result, the program will ask the user for "another input please" and print the result of the new user input out as well, until the user gives no input and just press enter (i.e. empty string): in that case it will exit.

So the points are:
* Convert the application to a console app creating Program.cs and Main method that printed out simple "Hello world"
* Instantiate AddService into Program.cs and pass to it the args if present or console input if it's not
* Add a while loop that ask for another input until user press "enter" with empty input
* Create a publish task that create a single .exe file
* Create a command to automatically add "scalc" to environment's path