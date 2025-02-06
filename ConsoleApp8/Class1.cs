using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleApp8
{
    internal  class Class1
    {

        public static InlineKeyboardMarkup help;
        public static InlineKeyboardMarkup start;
        

        public static void init()
        {
            help = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    text: "Список доступных команд",
                    callbackData: "commands"
                    )
            }
        });
            start = new InlineKeyboardMarkup(new[]
            {
                new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    text: "Добавить новую задачу",
                    callbackData: "setzadach"
                    )
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    text: "Показать список текущих задач",
                    callbackData: "showtask"
                    )
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    text: "Напомнить о дедлайнах",
                    callbackData: "dedline"
                    )
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    text: "Отметить выполненные задачи",
                    callbackData: "otmetka"
                    )
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    text: "Удаление ненужных задач",
                    callbackData: "deletezadacha"
                    )
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    text: "Помощь",
                    callbackData: "help"
                    )
            },
            

        });
        }



    }
}
