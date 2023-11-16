
namespace music_api.Auxi;

public static class Rand
{
    public static Random rand = new Random();


    public static string GetRandomString(int length)
    {
        string result = "";
        int randValue;
        for (int i = 0; i < length; i++)
        {
            randValue = rand.Next(0, 26);
            result += Convert.ToChar(randValue + 65);
        }
        return result;
    }

}