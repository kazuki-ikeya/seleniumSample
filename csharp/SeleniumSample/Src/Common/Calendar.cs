using System.Text.Json;

namespace Common
{
    class CommonCalendar
    {
        public static Dictionary<string, string> holidays = new Dictionary<string, string>() { };

        public static Dictionary<string, string> LoadHolidays()
        {
            if (CommonCalendar.holidays.Count != 0)
            {
                return CommonCalendar.holidays;
            }
            var targetFilePath = "common/jsons/holidays.json";
            var filePath = CommonFile.CreateAbsPath(targetFilePath);
            var fileData = CommonFile.Read(filePath);
            var jsonData = JsonSerializer.Deserialize(fileData, typeof(Dictionary<string, string>));

            CommonCalendar.holidays = (Dictionary<string, string>)(jsonData ?? new Dictionary<string, string> { });
            return CommonCalendar.holidays;
        }

        public static bool IsHoliday(DateTime? argDate = null)
        {

            // 未指定時は今日で判定
            DateTime date;
            if (argDate == null)
            {
                date = DateTime.Now;
            }
            else
            {
                date = argDate.Value;
            }

            // 土日判定
            var demo = new List<DayOfWeek>() { DayOfWeek.Saturday, DayOfWeek.Sunday };
            if (demo.Contains(date.DayOfWeek))
            {
                return true;
            }

            // 祝日判定
            var strDate = date.ToString("yyyy-MM-dd");
            if (CommonCalendar.LoadHolidays().ContainsKey(strDate))
            {
                return true;
            }

            return false;
        }

    }
}