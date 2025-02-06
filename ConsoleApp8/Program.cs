using ConsoleApp8;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{

    static ITelegramBotClient botClient;

    public static List<string> task;

    public Class1 keyboard = new Class1();

    static async Task Main(string[] args)
    {
        Class1.init();
        task = new List<string>();
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
            switch(update.Message.Text)
            {
                case "/help":
                    Console.WriteLine("Получено сообщение: " + update.Message.Text);
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Информация по использованию бота", replyMarkup: Class1.help);
                    break;
                case "/start":
                    Console.WriteLine("Получено сообщение: " + update.Message.Text);
                    await botClient.SendTextMessageAsync(
                        update.Message.Chat.Id, Strings.start,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                        replyMarkup: Class1.start);
                    break;
                case "/add":
                    Console.WriteLine("Получено сообщение: " + update.Message.Text);
                    if (update.Message != null)
                    {
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "напишите задачу в чат!");
                        task.Add(update.Message.Text);
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Задача добавлена!");
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Введите текст!");
                    }
                    break;
                case "/list":
                    
                    break;
                case "/done":
                    break;
                case "/delete":
                    break;
                default:
                    break;
            }
        }

        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            string buttonCommand = update.CallbackQuery.Data;
            switch(buttonCommand)
            {
                case "commands":
                    Console.WriteLine("Получено сообщение: " + update.CallbackQuery.Message.ToString());
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, Strings.help);
                    break;
                default:
                    break;
            }
        }
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            string buttonCommand2 = update.CallbackQuery.Data;
            switch(buttonCommand2)
            {
                case "setzadach":
                    break;
                case "help":
                    Console.WriteLine("Получено сообщение: " + update.CallbackQuery.ToString());
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Информация по использованию бота", replyMarkup: Class1.help);
                    break;
                case "showtask":
                    break;
                case "dedline":
                    break;
                case "otmetka":
                    break;
                case "deletezadacha":
                    break;
                default:
                    break;

            }
        }
    }

    public static void saveTOFile(long chatid)
    {
        File.AppendAllText("chatid.txt", chatid + Environment.NewLine);
    }
}
