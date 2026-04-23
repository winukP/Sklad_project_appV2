namespace Sklad_project_app
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // FATAL-03 - Обработка необработанных исключений
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.ThreadException += (sender, e) =>
            {
                Logger.Fatal($"FATAL-03: Необработанное исключение на уровне приложения.\n" +
                             $"Поток: {System.Threading.Thread.CurrentThread.Name}\n" +
                             $"Исключение: {e.Exception}\n" +
                             $"Состояние: приложение будет завершено.", e.Exception);
                MessageBox.Show("Произошла непредвиденная ошибка.\nПриложение будет закрыто.",
                    "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            };
            //FATAL-01 — Невозможно подключиться к базе данных при старте
            try
            {
                using (var db = new SkladContext())
                {
                    db.Database.CanConnect();
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal($"FATAL-01: Невозможно подключиться к базе данных при запуске приложения.\n" +
                             $"Host: localhost | Port: 6767 | Database: Sklad_db\n" +
                             $"Исключение: {ex.GetType()} --- {ex.Message}\n" +
                             $"Приложение будет завершено.", ex);
                MessageBox.Show("Не удалось подключиться к базе данных.\nПриложение будет закрыто.",
                    "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}