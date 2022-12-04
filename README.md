# floppydisk9414.03.flashcards
csThis is the https://www.thecsharpacademy.com/project/14 Flashcards project  
   
 ## Changes to get started!  
 #### Change 1  
 Had to change the framework from .net5.0 to a newer .net version so I chose .net7.0.  
 #### Change 2  
 Pablo, from the www.thecsharpacademy.com originially built this program with VS Code.  Since I was doing a pull request, it included some of that code.  I needed to update the luanchSettings.json file to my \bin folder like this:  
 ```
 "workingDirectory": "C:\\Users\\mjohnse1\\OneDrive - JNJ\\Documents\\Temp\\MJ_Items\\Engineering\\net\\CSharpAcademy\\floppydisk9414.03.flashcards\\Flashcards\\bin"  
 ```
 #### Change 3
 Delete almost everything!  I didn't delete the Packages (NuGet) packages as adding them wasn't too difficult.  Also, I left the program.cs file since it was working and just commented out Pablo's class/method calls.   But...  Every other class was DELETED.  

 ## NOTES, Etc  

 #### #nullable enable
 When setting up the StudySession class, I wasn't sure the best way to do that and wanted to have a property that I might or might not use.  So I wrote it w/ a '?' and rec'd a warning.  The remove the warning I needed the line at the top of the file:  
   
 #nullable enable
 https://stackoverflow.com/questions/55492214/the-annotation-for-nullable-reference-types-should-only-be-used-in-code-within-a  
  
 I had serious issues reading in a null StackName in the StudySession table.  I found a fix in this article:  
 https://stackoverflow.com/questions/1772025/sql-data-reader-handling-null-column-values  
   
 But..  struggling implementing the fix because I don't know how to add the method to the SqlDataReader type.  

