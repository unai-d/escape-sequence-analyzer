using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeSequenceAnalyzer
{
    enum ESCAPE_TYPE
    {
        NONE = 0x00,
        // Fe
        PADDING_CHARACTER = 0x80,
        SINGLE_SHIFT_TWO = 0x78,            // 'N'
        SINGLE_SHIFT_THREE = 0x79,          // 'O'
        DEVICE_CONTROL_STRING = 0x7a,       // 'P'
        CONTROL_SEQUENCE_INTRODUCER = 0x5b, // '['
        STRING_TERMINATOR = 0x5c,           // '\'
        OPERATING_SYSTEM_COMMAND = 0x5d,    // ']'
        START_OF_STRING = 0x98,
        PRIVACY_MESSAGE = 0x9e,
        APPLICATION_PROGRAM_COMMAND = 0x9f
    }

    enum OSC_CTRL
    {
        NONE = 0x00,
        SET_TITLE = 0x30,
        HYPERLINK = 0x38,
    }

    enum CSI_CTRL
    {
        NONE = 0x00,
        CURSOR_UP = (byte)'A',
        CURSOR_DOWN = (byte)'B',
        CURSOR_FORWARD = (byte)'C',
        CURSOR_BACK = (byte)'D',
        CURSOR_POSITION = (byte)'H',
        ERASE_IN_DISPLAY = (byte)'J',
        ERASE_IN_LINE = (byte)'K',
        SCROLL_UP = (byte)'S',
        SCROLL_DOWN = (byte)'T',
        SELECT_GRAPHIC_RENDITION = (byte)'m',
        ENABLE = (byte)'h',
        DISABLE = (byte)'l',
    }

    enum CSI_SELECT_GRAPHIC_RENDITION_CODE
    {
        UNKNOWN = -1,

        RESET = 0,
        BOLD = 1,
        FAINT = 2,
        ITALIC = 3,
        UNDERLINED = 4,
        BLINK = 5,
        FAST_BLINK = 6,
        INVERSE = 7,
        INVISIBLE = 8,
        STRIKE = 9,

        DEFAULT_FONT = 10,
        ALTERNATIVE_FONT_1 = 11,
        ALTERNATIVE_FONT_2 = 12,
        ALTERNATIVE_FONT_3 = 13,
        ALTERNATIVE_FONT_4 = 14,
        ALTERNATIVE_FONT_5 = 15,
        ALTERNATIVE_FONT_6 = 16,
        ALTERNATIVE_FONT_7 = 17,
        ALTERNATIVE_FONT_8 = 18,
        ALTERNATIVE_FONT_9 = 19,

        BLACKLETTER_FONT = 20,
        DOUBLE_UNDERLINE = 21,
        DISABLE_BOLD_AND_FAINT = 22,
        DISABLE_ITALIC_AND_BLACKLETTER = 23,
        NO_UNDERLINE = 24,
        NO_BLINK = 25,
        PROPORTIONAL_SPACING = 26,
        NOT_INVERSED = 27,
        NOT_INVISIBLE = 28,
        DISABLE_STRIKE = 29,

        FG_BLACK = 30,
        FG_RED = 31,
        FG_GREEN = 32,
        FG_YELLOW = 33,
        FG_BLUE = 34,
        FG_MAGENTA = 35,
        FG_CYAN = 36,
        FG_WHITE = 37,
        FG_ADVANCED = 38,
        FG_DEFAULT = 39,

        BG_BLACK = 40,
        BG_RED = 41,
        BG_GREEN = 42,
        BG_YELLOW = 43,
        BG_BLUE = 44,
        BG_MAGENTA = 45,
        BG_CYAN = 46,
        BG_WHITE = 47,
        BG_ADVANCED = 48,
        BG_DEFAULT = 49,

        DISABLE_PROPORTIONAL_SPACING = 50,
        FRAMED = 51,
        ENCIRCLED = 52,
        OVERLINED = 53,
        NEITHER_FRAMED_NOR_ENCIRCLED = 54,
        NOT_OVERLINED = 55,
        SET_UNDERLINE_COLOR = 58,
        DEFAULT_UNDERLINE_COLOR = 59,

        SUPERSCRIPT = 73,
        SUBSCRIPT = 74,

        FG_GRAY = 90,
        FG_BRIGHT_RED = 91,
        FG_BRIGHT_GREEN = 92,
        FG_BRIGHT_YELLOW = 93,
        FG_BRIGHT_BLUE = 94,
        FG_BRIGHT_MAGENTA = 95,
        FG_BRIGHT_CYAN = 96,
        FG_BRIGHT_WHITE = 97,

        BG_GRAY = 100,
        BG_BRIGHT_RED = 101,
        BG_BRIGHT_GREEN = 102,
        BG_BRIGHT_YELLOW = 103,
        BG_BRIGHT_BLUE = 104,
        BG_BRIGHT_MAGENTA = 105,
        BG_BRIGHT_CYAN = 106,
        BG_BRIGHT_WHITE = 107,
    }

    enum CSI_ERASE_IN_DISPLAY_CODE
	{
        FROM_CURSOR_TO_END_OF_SCREEN = 0,
        FROM_CURSOR_TO_BEGINNING_OF_SCREEN = 1,
        ALL_SCREEN = 2,
        ALL_SCREEN_AND_SCROLLBACK_BUFFER = 3
	}

    enum CSI_ERASE_IN_LINE_CODE
	{
        FROM_CURSOR_TO_END_OF_LINE = 0,
        FROM_CURSOR_TO_BEGINNING_OF_LINE = 1,
        ALL_LINE = 2,
	}

    enum PRIVATE_SEQUENCE_CODE
	{
        CURSOR = 25,
        ALTERNATIVE_SCREEN_BUFFER = 1049,
        BRACKETED_PASTE_MODE = 2004,
	}

    enum CSI_SGR_COLOR_MODE
	{
        NONE = -1,
        RGB24 = 2,
        PAL8 = 5,
	}
}
