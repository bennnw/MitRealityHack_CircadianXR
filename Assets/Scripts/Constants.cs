using UnityEngine;
using System.IO;

public static class Constants
{
    public const string LIGHT_SOURCE = "DirectionalLightSource";
    public const string START_PICK_TIME = "StartPickTime";
    public const string NO_SELECTION = "NoSelection";
    public const string IMG_0_1 = "Img_1";
    public const string IMG_1_2 = "Img_2";
    public const string IMG_2_3 = "Img_3";
    public const string IMG_3_4 = "Img_4";
    public const string IMG_4_5 = "Img_5";
    public const string IMG_5_6 = "Img_6";
    public const string IMG_6_7 = "Img_7";
    public const string IMG_7_8 = "Img_8";
    public const string IMG_8_9 = "Img_9";
    public const string IMG_9_10 = "Img_10";
    public const string IMG_10_11 = "Img_11";
    public const string IMG_11_12 = "Img_12";
    public const string START_BUTTON = "StartButton";
    public const string CONFIRM_BUTTON = "ConfirmButton";

    public const string BTN_0_1 = "Btn_0_1";
    public const string BTN_1_2 = "Btn_1_2";
    public const string BTN_2_3 = "Btn_2_3";
    public const string BTN_3_4 = "Btn_3_4";
    public const string BTN_4_5 = "Btn_4_5";
    public const string BTN_5_6 = "Btn_5_6";
    public const string BTN_6_7 = "Btn_6_7";
    public const string BTN_7_8 = "Btn_7_8";
    public const string BTN_8_9 = "Btn_8_9";
    public const string BTN_9_10 = "Btn_9_10";
    public const string BTN_10_11 = "Btn_10_11";
    public const string BTN_11_12 = "Btn_11_12";

    public static Color SLEEP_COLOR = Color.red;
    public static Color WAKEUP_COLOR = Color.blue;

    public static string USER_FILE = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Resources", "user.txt");
    public static string FLIGHT_FILE = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Resources", "flight.txt");
}
