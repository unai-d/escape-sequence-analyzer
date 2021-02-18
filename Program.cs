using System;
using System.Threading;

namespace EscapeSequenceAnalyzer
{
    class Program
    {
        static bool DecodingEscapeSequence = false;
        
        static ESCAPE_TYPE EscapeType = ESCAPE_TYPE.NONE;
        static OSC_CTRL CurrentOscCtrl = OSC_CTRL.NONE;

        static void Main(string[] args)
        {
            var stdin = Console.OpenStandardInput();
            string[] escargs = new string[8];

            int pos = 0, charcount = 0, argsindex = 0, retries = 0;

            while (retries < 5)
            {
                int b = stdin.ReadByte();
                if (b == -1) { Thread.Sleep(200); retries++; continue; }

                if (!DecodingEscapeSequence) // Text mode
                {
                    if (b != 0x1b) // Not entering escape mode
                    {
                        Console.WriteLine($"0x{pos:X4}  {b:X2}.");
                        charcount++;
                    }
                    else // Entering escape mode
                    {
                        Console.WriteLine($"0x{pos:X4}  1B ESC.");
                        DecodingEscapeSequence = true;
                        charcount = 0;
                    }
                }
                else // Escape mode
                {
                    bool terminate = false;

                    if (EscapeType == ESCAPE_TYPE.NONE) // Esc mode not set.
                    {
                        EscapeType = (ESCAPE_TYPE)b;
                        Console.WriteLine($"0x{pos:X4}  {b:X2} {EscapeType}.");
                    }
                    else if (EscapeType == ESCAPE_TYPE.OPERATING_SYSTEM_COMMAND) // Esc mode is OSC ]
                    {
                        if (b == 0x07)
                        {
                            terminate = true;
                        }
                        else if (Enum.IsDefined(typeof(OSC_CTRL), b))
                        {
                            CurrentOscCtrl = (OSC_CTRL)b;
                            Console.WriteLine($"0x{pos:X4}  {b:X2} {CurrentOscCtrl}.");
                        }
                        else
                        {
                            escargs[argsindex] += (char)b;
                            Console.WriteLine($"0x{pos:X4}  {b:X2} '{(char)b}'.");
                        }
                    }
                    else if (EscapeType == ESCAPE_TYPE.STRING_TERMINATOR) // Esc mode is ST \
                    {
                        terminate = true;
                    }
                    else if (EscapeType == ESCAPE_TYPE.CONTROL_SEQUENCE_INTRODUCER) // Esc mode is CSI [
                    {
                        if (b >= 0x30 & b <= 0x39) // 0-9
                        {
                            escargs[argsindex] += (char)b;
                            Console.WriteLine($"0x{pos:X4}  {b:X2} NUMBER_{(char)b}.");
                        }
                        else if (b == 0x3a) // ':'
                        {
                            argsindex++;
                            Console.WriteLine($"0x{pos:X4}  3A COLON_DELIMITER.");
                        }
                        else if (b == 0x3b) // ';'
                        {
                            argsindex++;
                            Console.WriteLine($"0x{pos:X4}  3B SEMICOLON_DELIMITER.");
                        }
                        else if (b == 0x3f) // '?'
                        {
                            escargs[argsindex] += (char)b;
                            Console.WriteLine($"0x{pos:X4}  3F PRIVATE_CONTROL_SEQUENCE.");
                        }
                        else if (Enum.IsDefined(typeof(CSI_CTRL), b))
                        {
                            Console.WriteLine($"0x{pos:X4}  {b:X2} {(CSI_CTRL)b}.");
                            switch ((CSI_CTRL)b)
                            { 
                                case CSI_CTRL.SELECT_GRAPHIC_RENDITION:
                                    foreach (string s in escargs)
                                    {
                                        if (s != null && s != "")
                                            Console.WriteLine($"           '{s}' = {(CSI_SELECT_GRAPHIC_RENDITION_CODE)Convert.ToInt32(s)}.");
                                    }
                                    break;

                                case CSI_CTRL.ERASE_IN_DISPLAY:
                                    Console.WriteLine($"           {(CSI_ERASE_IN_DISPLAY_CODE)Convert.ToInt32(escargs[0])}.");
                                    break;

                                case CSI_CTRL.ERASE_IN_LINE:
                                    Console.WriteLine($"           {(CSI_ERASE_IN_LINE_CODE)Convert.ToInt32(escargs[0])}.");
                                    break;

                                case CSI_CTRL.CURSOR_POSITION:
                                    Console.WriteLine($"           ROW {escargs[0] ?? "1"}, COLUMN {escargs[1] ?? "1"}.");
                                    break;

                                case CSI_CTRL.SCROLL_UP:
                                case CSI_CTRL.SCROLL_DOWN:
                                    Console.WriteLine($"           {escargs[0] ?? "1"} LINES.");
                                    break;

                                case CSI_CTRL.ENABLE:
                                case CSI_CTRL.DISABLE:
                                    if (escargs[0] == "?25")
                                    {
                                        Console.WriteLine($"           CURSOR.");
                                    }
                                    else if (escargs[0] == "?1049")
									{
                                        Console.WriteLine($"           ALTERNATIVE_SCREEN_BUFFER.");
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"0x{pos:X4}  {b:X2} UND.");
                        }

                        if (b >= 0x40 && b <= 0x7e) // Entering text mode (terminator of CSI)
                        {
                            terminate = true;
                        }
                    }
                    else // Unrecognized Fe mode.
                    {
                        Console.WriteLine($"0x{pos:X4}  {b:X2} UND.");
                    }

                    if (terminate)
                    {
                        Console.WriteLine($"           ESC_END.");
                        DecodingEscapeSequence = false;
                        EscapeType = ESCAPE_TYPE.NONE;
                        escargs = new string[8];
                        argsindex = 0;
                    }
                }

                pos++;
            }
        }
    }
}
