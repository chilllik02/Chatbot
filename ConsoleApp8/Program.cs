using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{
    public static InlineKeyboardMarkup help;
    public static InlineKeyboardMarkup start;
    static ITelegramBotClient botClient;
    static async Task Main(string[] args)
    {

        help = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    text: "Список доступных команд",
                    callbackData: "commands"
                    )
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    text: "Что я умею?👹",
                    callbackData: "12345"
                    )
            }
        });
        start = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    text: "TIME⌚",
                    callbackData: "time"
                    )
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    text: "HELP⚠️",
                    callbackData: "help"
                    )
            }
        });





        string token = "7808802453:AAEGMa8kl4n5XDqxvAAFa05cqED1FO_z53U";
        botClient = new TelegramBotClient(token);

        var reciverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }
        };

        botClient.StartReceiving(
             updateHandler: Polychenie,
             receiverOptions: reciverOptions,
             errorHandler: HandleErrorAsync
            );

        Console.WriteLine("Бот запущен! Нажмите Enter для выхода...");
        Console.ReadLine();
    }

    private static async Task HandleErrorAsync(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
    {
        Console.WriteLine($"Произошла ошибка: {exception.Message.ToString()}");
        
    }

    private static async Task Polychenie(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if(update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            saveTOFile(update.Message.Chat.Id);
            if (update.Message != null && update.Message.Text == "/Time" || update.Message.Text == "/time")

            {
                Console.WriteLine("Получено сообщение: " + update.Message.Text);
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Текущее время и дата: " + DateTime.Now.ToString());
            }
            else if (update.Message != null && update.Message.Text == "/Help" || update.Message.Text == "/help")
            {
                Console.WriteLine("Получено сообщение: " + update.Message.Text);

                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Информация по использованию бота", replyMarkup: help);
            }
            else
            {
                Console.WriteLine("Получено сообщение: " + update.Message.Text);

                await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"Привет {update.Message.Chat.FirstName}, я бот помощник! ", replyMarkup: start);
            }
        }

        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            string buttonCommand = update.CallbackQuery.Data;
            if (buttonCommand == "commands")
            {
                Console.WriteLine("Получено сообщение: " + update.CallbackQuery.Message.ToString());

                await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, $"Вы запросили список комманд!");
            }
            if (buttonCommand == "12345")
            {
                Console.WriteLine("Получено сообщение: " + update.CallbackQuery.Message.ToString());

                await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, $"Вы запросили, что я умею!");
            }
        }
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            string buttonCommand2 = update.CallbackQuery.Data;
            if (buttonCommand2 == "time")
            {
                Console.WriteLine("Получено сообщение: " + update.CallbackQuery.Message.ToString());
                await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Текущее время и дата: " + DateTime.Now.ToString());
            }
            if (buttonCommand2 == "help")
            {
                Console.WriteLine("Получено сообщение: " + update.CallbackQuery.ToString());
                await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Информация по использованию бота", replyMarkup: help);
            }
        }




    }

    public static void saveTOFile(long chatid)
    {
        File.AppendAllText("chatid.txt", chatid + Environment.NewLine);
    }
}
