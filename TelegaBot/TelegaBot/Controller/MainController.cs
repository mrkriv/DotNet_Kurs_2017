using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using TelegaBot.Data;

namespace TelegaBot.Controller
{
    public class MainController : ControllerBase
    {
        [MessageMask("hello", RegexOptions.IgnoreCase)]
        public void Hello()
        {
            var user = DB.Users.FirstOrDefault(x => x.ID == Update.Message.FromUser.UserID);

            if (user != null)
            {
                var msg = "Hello " + Update.Message.FromUser.FirstName + "!!!";
                API.SendMessage(Update.Message.Chat, msg);
            }
            else
            {
                var msg = "Привет " + Update.Message.FromUser.FirstName + "! Напиши мне сове расписание";
                API.SendMessage(Update.Message.Chat, msg);
            }
        }

        [MessageMask(@"что у меня (\d+)", RegexOptions.IgnoreCase)]
        [Description("Показать пары")]
        public void PrintDay([Description("День")]int Day)
        {
            var user = DB.Users.FirstOrDefault(x => x.ID == Update.Message.FromUser.UserID);

            if (user == null)
            {
                API.SendMessage(Update.Message.Chat, "Я тебя не знаю");
                return;
            }

            if (user.Schedule == null)
            {
                API.SendMessage(Update.Message.Chat, "Я не знаю");
                return;
            }

            if (Day < 1 || Day > 7)
            {
                API.SendMessage(Update.Message.Chat, "В неделе 7 дней!!");
                return;
            }

            if (user.Schedule.Days[Day - 1].Lessons.Any())
            {
                foreach (var lesson in user.Schedule.Days[Day - 1].Lessons)
                {
                    API.SendMessage(Update.Message.Chat, $"{lesson.Value.Name} кабинет {lesson.Value.Cabinet}");
                }
            }
            else
            {
                API.SendMessage(Update.Message.Chat, "Выходной!");
            }
        }

        [MessageMask(@"запомни\s+(\d+)\s+(\d+)\s+(\w+)\s+(\d+)", RegexOptions.IgnoreCase)]
        [Description("Запомнить пару")]
        public void Rememder(
            [Description("День")]int Day,
            [Description("Номер пары")]int Num,
            [Description("Название пары")]string Name,
            [Description("Кабинет")]int Cabinet)
        {
            var user = DB.Users.FirstOrDefault(x => x.ID == Update.Message.FromUser.UserID);

            if (user == null)
            {
                user = new TelegaUser
                {
                    ID = Update.Message.FromUser.UserID,
                    Name = Update.Message.FromUser.FirstName
                };

                DB.Users.Add(user);
            }

            var schedule = user.Schedule;

            if (schedule == null)
            {
                schedule = new Schedule();
                user.Schedule = schedule;

                DB.Schedules.Add(schedule);
            }

            if (Day < 1 || Day > 7)
            {
                API.SendMessage(Update.Message.Chat, "В неделе 7 дней!!");
                return;
            }

            var day = schedule.Days[Day - 1];

            var lesson = new Lesson
            {
                Name = Name,
                Cabinet = Cabinet,
            };

            if (day.Lessons.ContainsKey(Num))
            {
                day.Lessons[Num] = lesson;
            }
            else
            {
                day.Lessons.Add(Num, lesson);
            }

            DB.Save();

            API.SendMessage(Update.Message.Chat, "Запомнил");
        }
    }
}
