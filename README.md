# Escape Sequence Analyzer
As its name suggests, this little program reads data from the standard input and shows information about the ANSI escape sequences that the data stream could contain.
This application is written in C# for the .NET Core 3.1 platform.

## Usage
Since this program only reads data from the standard input, you will need to use a pipeline:

`a-program | ./EscapeSequenceAnalyzer`

This will make `EscapeSequenceAnalyzer` take the standard output of `a-program` and search for escape sequences.
If you are running this on a Windows machine, don't forget to specify the `.exe` extension.

### Example 1
There are a bunch of Linux programs that make use of ANSI escape sequences in order to, for example, change the text color, or clear the screen.
The command `clear` makes use of those escape sequences in order to clear the screen by simply printing out the following escape sequences:

```
$ clear | ./EscapeSequenceAnalyzer
0x0000  1B ESC.
0x0001  5B CONTROL_SEQUENCE_INTRODUCER.
0x0002  33 NUMBER_3.
0x0003  3B SEMICOLON_DELIMITER.
0x0004  4A ERASE_IN_DISPLAY.
           ALL_SCREEN_AND_SCROLLBACK_BUFFER.
           ESC_END.
0x0005  1B ESC.
0x0006  5B CONTROL_SEQUENCE_INTRODUCER.
0x0007  48 CURSOR_POSITION.
           ROW 1, COLUMN 1.
           ESC_END.
0x0008  1B ESC.
0x0009  5B CONTROL_SEQUENCE_INTRODUCER.
0x000A  32 NUMBER_2.
0x000B  4A ERASE_IN_DISPLAY.
           ALL_SCREEN.
           ESC_END.
```
First, it erases the entire screen and the scrollback buffer, although the last action is not supported by many terminal emulators.
Then, it changes the cursor position to the top-left corner of the screen (row 1, column 1).
And finally, clears the entire screen again.

### Example 2
```
$ echo -ne "\e[31m RED TEXT \e[0m" | ./EscapeSequenceAnalyzer
0x0000  1B ESC.                        
0x0001  5B CONTROL_SEQUENCE_INTRODUCER.
0x0002  33 NUMBER_3.                   
0x0003  31 NUMBER_1.                   
0x0004  6D SELECT_GRAPHIC_RENDITION.   
           '31' = FG_RED.              
           ESC_END.                    
0x0005  20.                            
0x0006  52.                            
0x0007  45.                            
0x0008  44.                            
0x0009  20.                            
0x000A  54.                            
0x000B  45.                            
0x000C  58.                            
0x000D  54.                            
0x000E  20.                            
0x000F  1B ESC.                        
0x0010  5B CONTROL_SEQUENCE_INTRODUCER.
0x0011  30 NUMBER_0.                   
0x0012  6D SELECT_GRAPHIC_RENDITION.   
           '0' = RESET.                
           ESC_END.                    
```
The first escape sequence sets the foreground color to red, so following characters will be rendered in red.
And the final escape sequence will reset the color back to its original state, among other properties.
