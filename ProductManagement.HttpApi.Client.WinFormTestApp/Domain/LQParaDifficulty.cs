using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.HttpApi.Client.WinFormTestApp.Domain
{
    public static class Difficulty
    {
        public static Dictionary<DifficultyCode, string> Levels = new()
        {
            {DifficultyCode.Easy, "易"},
            {DifficultyCode.Medium, "中"},
            {DifficultyCode.Hard, "難"},
        };

        public static DifficultyCode? GetKeyByValue(string value)
        {
            return Levels.FirstOrDefault(x => x.Value == value).Key;
        }
    }

    public enum DifficultyCode
    {
        Easy = 1,
        Medium,
        Hard
    }
}
