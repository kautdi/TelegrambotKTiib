using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using System.Drawing;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using static Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton;
using System.IO;
using static System.IO.Path;
using System.Data.SQLite;

namespace DangerousSausages
{
    
    public partial class MainWindow : Window
    {
        string Token;
        private static Telegram.Bot.TelegramBotClient BOT;
        
        public  MainWindow()
        {
            InitializeComponent();
            if (!File.Exists(@"TestDB.db"))
            {
                SQLiteConnection.CreateFile(@"TestDB.db");
            }
            using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=TestDB.db; Version=3;")) // в строке указывается к какой базе подключаемся
            {
                // строка запроса, который надо будет выполнить
                string commandText = "CREATE TABLE IF NOT EXISTS [dbTableName] ( [id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, [name] NVARCHAR(128), [position] NVARCHAR(128), " +
       "[task] NVARCHAR(128), [bal] INTEGER,[priceOch] INTEGER,[priceZaOch] INTEGER )"; // создать таблицу, если её нет
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open(); // открыть соединение
                Command.ExecuteNonQuery(); // выполнить запрос
                Connect.Close(); // закрыть соединение
            }
            using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=TestDB.db; Version=3;")) // в строке указывается к какой базе подключаемся
            {
                // строка запроса, который надо будет выполнить
                string commandText = "CREATE TABLE IF NOT EXISTS [Specialitet] ( [id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, [name] NVARCHAR(128), [about] NVARCHAR(128), " +
       "[exams] NVARCHAR(128), [priceOch] INTEGER,[priceZaOch] INTEGER)"; // создать таблицу, если её нет
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open(); // открыть соединение
                Command.ExecuteNonQuery(); // выполнить запрос
                Connect.Close(); // закрыть соединение
            }

            using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=TestDB.db; Version=3;")) // в строке указывается к какой базе подключаемся
            {
                // строка запроса, который надо будет выполнить
                string commandText = "CREATE TABLE IF NOT EXISTS [Aspirantura] ( [id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, [name] NVARCHAR(128), [about] NVARCHAR(128), " +
       "[exams] NVARCHAR(128))"; // создать таблицу, если её нет
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open(); // открыть соединение
                Command.ExecuteNonQuery(); // выполнить запрос
                Connect.Close(); // закрыть соединение
            }
        }
        


private void EnterToken(object sender, EventArgs e)
        {
            Token = TokenText.Text;

            if (TokenText != null)
            {
                TokenLabel.Content = "Token received";
            }
        }

        private void StartBot(object sender, RoutedEventArgs e)
        {
            if (TokenLabel.Content.Equals("Token received"))
            {
                TokenLabel.Content = "Bot alive";
            }
            BOT = new Telegram.Bot.TelegramBotClient(Token);
            BOT.OnMessage += BotOnMessageReceived;
            BOT.StartReceiving(new UpdateType[] { UpdateType.Message });
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            

            String Answer = "";

            
            Telegram.Bot.Types.Message msg = messageEventArgs.Message;
            if (msg == null || msg.Type != MessageType.Text) return;
                
            

                switch (msg.Text)
            {
                case "Общежития":
                    Answer = "Адреса общежитий:\n\n1)г. Ростов-на-Дону ул. 2-я Краснодарская, 113/1\n\n2)г.Ростов-на-Дону пер.Гвардейский, 6\n\nПосмотеть на карте:https://clck.ru/V4FDR \n";
                    break;
                case "Факультет КТиИБ":
                    Answer = "Что вам необиходмо узнать?";
                    break;
                case "Геолокация":
                    Answer = "Адрес:\nг.Ростов-на-Дону, Большая Садовая 69\nhttps://inlnk.ru/qVOKz";
                    break;
                case "Специальности":
                    Answer= "В данном разделе мы можете посмотреть напрвления в нашем университете.";
                    break;
                case "/start": Answer = "Привет, дорогой абитуриент! Давайте знакомится!\n\nЯ - чат - бот КТиИБ РИНХа😻\nСоздан я специально для будующих студентов, чтобы они поближе познакомились с нашим университетом.\n Дружба со мной позволит вам:\n\n🔍 Узнать об направлениях на нашем университете.\n\n 🧩 Найти наш факультет.\n\n 🕑 Найти контакты для связи с нами.✔️ Узнать больше о нашем факультете."; break;
   
                case "Скрыть панель":
                    Answer = "Перейдите в /start";
                    await BOT.SendTextMessageAsync(msg.Chat.Id, "Для повторного вызова панели", replyMarkup: new ReplyKeyboardRemove());
                    break;
                case "Студенческая жизнь":
                    Answer = "В РГЭУ(РИНХ) работают 12 спортивных секций, активно функционируют спортивные клубы: боксерский им. Д.Кудряшова, шахматный им. Е.Ковалевской, бильярдный, туристический и студенческий спортивный клуб «БАРС - РГЭУ». Творческие и яркие студенты факультета КТиИБ активно участвуют в деятельности Студенческого культурного центра, достойно представляют родной факультет на просторах необъятного межвузовского пространства.Летом студенты факультета имеют возможность отдохнуть на побережье Азовского моря в СОЛ «Ивушка».";
                    break;
                case "Трудоустройство":
                    Answer = "Факультет активно расширяет список баз практик, по месту прохождения которых студенты в дальнейшим могут трудоустроиться.\nНаши партнеры:\nЮг Руси;\nРоствертол;\nЭлектронная медицина;\nГамма;\nСпецвузавтоматика;\nЦентр - Инвест;\nСбербанк;\nФСТЭК;\nРостсельмаш;\nАдминистрации Ростова и РО.";
                    break;
                case "Профессиональная подготовка":
                    Answer = "По окончании обучения выпускники могут претендовать на следующие вакантные места организаций:\nbackend - и frontend - разработчики;\nweb - программисты;\n1C - программисты;\nсистемные архитекторы;\nбизнес - аналитики;\nIT - менеджеры;\nруководители IT-проектов;\nIT - директора;\nразработчики информационных систем;\nспециалисты по ИБ;\ndata scientist;\nфинансовые аналитики и т.д.";
                    break;
                case "Социальные сети":
                    Answer = "Присоединяйтесь к нам!";
                    break;
                case "Как добраться с автовокзала?":
                    Answer = "Проложить маршрут:\nhttps://clck.ru/V4HBh";
                    break;
                case "Как добраться с Ж/Д?":
                    Answer = "Проложить маршрут:\nhttps://clck.ru/V4HMf";
                    break;
                case "Аспирантура":
                    using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=TestDB.db; Version=3;"))
                    {
                        string commandText = "SELECT * FROM [Aspirantura] WHERE [name] NOT NULL";
                        SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                        Connect.Open(); // открыть соединение
                        SQLiteDataReader sqlReader = Command.ExecuteReader();
                        while (sqlReader.Read()) // считываем и вносим в лист все параметры
                        {
                            object all_task = " ";
                            object position = " ";
                            object name = sqlReader["name"];
                            ;
                            all_task = sqlReader["exams"];
                            position = sqlReader["about"];
                            String All_Bakalavr = name + "\n\nОписание:\n" + position.ToString() + "\n\nЭкзамены(ЕГЭ):\n" + all_task.ToString();
                            await BOT.SendTextMessageAsync(msg.Chat.Id, All_Bakalavr);

                            Answer = "Подробнее:\nhttps://rsue.ru/abitur/aspirantura/programmi/";
                        }
                        Connect.Close();
                    }
                    break;
                case "Бакалавриат":
                    using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=TestDB.db; Version=3;"))
                    {
                        string commandText = "SELECT * FROM [dbTableName] WHERE [name] NOT NULL";
                        SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                        Connect.Open(); // открыть соединение
                        SQLiteDataReader sqlReader = Command.ExecuteReader();
                        while (sqlReader.Read()) // считываем и вносим в лист все параметры
                        {
                            object all_task = " ";
                            object position = " ";
                            object name = sqlReader["name"];
                            object bal = sqlReader["bal"];
                            object priceOch = sqlReader["priceOch"];
                            object priceZaOch = sqlReader["priceZaOch"];
                            all_task = sqlReader["task"];
                            position = sqlReader["position"];
                           
                            String All_Bakalavr = name + "\n\nОписание:\n" + position.ToString() + "\n\nЭкзамены(ЕГЭ):\n" + all_task.ToString()+ "\n\nПроходной балл: " + bal.ToString() + "\n\nСтоимость обучения(очная/заочная): " + priceOch.ToString() + "/" + priceZaOch.ToString();
                                await BOT.SendTextMessageAsync(msg.Chat.Id, All_Bakalavr);

                                Answer = "Подробнее:\nhttp://ktib-rsue.ru/napravleniya-podgotovki.html";
                        }
                        Connect.Close();
                    }
                    break;
                case "Магистратура":
                    using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=TestDB.db; Version=3;"))
                    {
                        string commandText = "SELECT * FROM [Specialitet] WHERE [name] NOT NULL";
                        SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                        Connect.Open(); // открыть соединение
                        SQLiteDataReader sqlReader = Command.ExecuteReader();
                        while (sqlReader.Read()) // считываем и вносим в лист все параметры
                        {
                            object all_task = " ";
                            object position = " ";
                            object name = sqlReader["name"];
                            all_task = sqlReader["exams"];
                            object priceOch = sqlReader["priceOch"];
                            object priceZaOch = sqlReader["priceZaOch"];
                            position = sqlReader["about"];
                            String All_Bakalavr = name + "\n\nОписание:\n" + position.ToString() + "\n\nЭкзамены(ЕГЭ):\n" + all_task.ToString() + "\n\nСтоимость обучения(очная/заочная): " + priceOch.ToString() + "руб/" + priceZaOch.ToString() + "руб";
                            await BOT.SendTextMessageAsync(msg.Chat.Id, All_Bakalavr);

                            Answer = "Подробнее:\nhttp://magistratura.rsue.ru/umd.php";
                        }
                        Connect.Close();
                    }
                    break;
                case "Документы":

                    string mesage = "Список Совета факультета\nhttps://inlnk.ru/WEVZQ ";
                    await BOT.SendTextMessageAsync(msg.Chat.Id, mesage);
                    mesage = "План работы Совета факультета\nhttps://inlnk.ru/qO44o";
                    await BOT.SendTextMessageAsync(msg.Chat.Id, mesage);
                    Answer = "Положение об факультете\nhttps://inlnk.ru/WyGG7";
                    break;
                case "Контакты":
                    Answer = "Приемная комиссия\nТелефон:+7 (863) 237-02-60\nПочта: pk@rsue.ru\n\nПриемная ректора\nТелефон:+7 (863) 263-30-80\nПочта: main@rsue.ru\n\nДеканат\nТелефон:(863) 261-38-64\nПочта: celt@inbox.ru";
                    break;
               

                default:             
                        Answer = "Перейдите в /start";
                    break;
            }
            await BOT.SendTextMessageAsync(msg.Chat.Id, Answer);

            if (msg.Text == "Специальности")
            {
                var keyboard = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
                {
                    Keyboard = new[] {
                                                new[] // row 1
                                                {
                                                    new KeyboardButton("Бакалавриат"),
                                                    new KeyboardButton("Магистратура"),
                                                },
                                                new[] // row 1
                                                {
                                                    new KeyboardButton("Аспирантура"),
                                                 
                                                },
                                                new[] // row 2
                                                {
                                                    new KeyboardButton("Скрыть панель"),
                                                },
                                             },
                    ResizeKeyboard = true
                };
                await BOT.SendTextMessageAsync(msg.Chat.Id, "Что вас интересует?", replyMarkup: keyboard);
            }
            if (msg.Text == "/start")
            {
                var keyboard = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
                {
                    Keyboard = new[] {
                                                new[] // row 1
                                                {
                                                    new KeyboardButton("Специальности"),
                                                },
                                                  new[] // row 1
                                                {
                                                    new KeyboardButton("Общежития"),
                                                    new KeyboardButton("Факультет КТиИБ"),
                                                },
                                                    new[] // row 1
                                                {
                                                    new KeyboardButton("Контакты"),
                                                    new KeyboardButton("Геолокация"),
                                                   
                                                },
                                                    
                                                new[] // row 2
                                                {
                                                    new KeyboardButton("Скрыть панель"),
                                                },
                                             },
                    ResizeKeyboard = true
                };
                await BOT.SendTextMessageAsync(msg.Chat.Id, "Выберете то, что вас интересует! ", replyMarkup: keyboard);
            }
            if (msg.Text == "Социальные сети")
            {
                var keyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(new[]
{
                        new [] // first row
                                        {
                                                      InlineKeyboardButton.WithUrl("Вконтакте","https://vk.com/ktiib"),
                                                      InlineKeyboardButton.WithUrl("Инстаграм","https://www.instagram.com/f.ktiib/"),
                                        },
                        new [] // first row
                                        {
                                                      InlineKeyboardButton.WithUrl("Наш сайт","https://vk.com/away.php?to=http%3A%2F%2Fktib-rsue.ru"),
                                        },


                                                    });
                await BOT.SendTextMessageAsync(msg.Chat.Id, "Наши социальные сети!", replyMarkup: keyboard);
            }
            if (msg.Text == "Факультет КТиИБ") { 
                var keyboardBack = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
                {
                    Keyboard = new[] {
                                                new[] // row 1
                                                {
                                                    new KeyboardButton("Студенческая жизнь"),
                                                    new KeyboardButton("Трудоустройство"),
                                                },
                                                  new[] // row 1
                                                {
                                                    new KeyboardButton("Профессиональная подготовка"),
                                                    new KeyboardButton("Социальные сети"),
                                                },
                                                  new[] // row 1
                                                {
                                                    new KeyboardButton("Скрыть панель"),
                                                    
                                                },

                                             },
                    ResizeKeyboard = true
                };
                await BOT.SendTextMessageAsync(msg.Chat.Id, "Выберете то, что вас интересует! ", replyMarkup: keyboardBack);

            }
            if (msg.Text == "Геолокация")
            {
                var keyboardBack = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
                {
                    Keyboard = new[] {
                                                
                                                  new[] // row 1
                                                {
                                                    new KeyboardButton("Как добраться с Ж/Д?"),
                                                    new KeyboardButton("Как добраться с автовокзала?"),
                                                },
                                                  new[] // row 1
                                                {
                                                    new KeyboardButton("Скрыть панель"),

                                                },

                                             },
                    ResizeKeyboard = true
                };
                await BOT.SendTextMessageAsync(msg.Chat.Id, "Выберете то, что вас интересует! ", replyMarkup: keyboardBack);

            }
        }

        private void TokenText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    
    }
}
